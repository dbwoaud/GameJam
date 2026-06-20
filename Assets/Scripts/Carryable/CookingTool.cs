using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class CookingTool : Carryable
{
    public abstract CookType Type { get; }

    [Header("재료 스택 표시")]
    [SerializeField] private Transform contentsRoot;
    [SerializeField] private float stackHeight = 0.2f;

    [Header("조리 시간")]
    [SerializeField] private float doneTime = 1f;   
    [SerializeField] private float burnTime = 15f;

    [Header("래퍼런스")]
    [SerializeField] CookingToolUI ui;

    private readonly Stack<Ingredient> contents = new Stack<Ingredient>();
    private float timer;

    public CookState State { get; private set; } = CookState.Idle;

    //  재료와 조리도구가 일치할 때 레시피.
    CookData recipe;
    GameObject resultObject;

    public int IngredientCount => contents.Count;
    public bool IsDone => State == CookState.Done;
    public bool IsBurnt => State == CookState.Burnt;
    public bool ShowWarning => State == CookState.Done;             
    public float CookProgress01 => State == CookState.Cooking ? Mathf.Clamp01(timer / doneTime) : State == CookState.Done ? 1f : 0f;

    public bool TryPush(Ingredient ingredient)
    {
        if (State != CookState.Idle || ingredient == null) 
            return false;

        contents.Push(ingredient);

        Transform root = contentsRoot != null ? contentsRoot : transform;
        ingredient.AttachTo(root);
        ingredient.transform.localPosition = new Vector3(0f, stackHeight * (contents.Count - 1), 0f);

        TryStart();

        return true;
    }

   
    public Ingredient TryPopTop()
    {
        if (State != CookState.Idle || contents.Count == 0) 
            return null;

        return contents.Pop();
    }

    public void TickCooking(float dt)
    {
        switch (State)
        {
            case CookState.Idle: 
                break;
            case CookState.Cooking:
                ui.ShowPatience(timer / burnTime);

                timer += dt; 
                if (timer >= doneTime) 
                    Complete(); 
                break;
            case CookState.Done:
                ui.ShowPatience(timer / burnTime);

                timer += dt; 
                if (timer >= burnTime) 
                    Burn(); 
                break;
        }
    }

    private void TryStart()
    {
        if (contents.Count == 0) 
            return;

        List<int> itemIndexs = new();
        foreach (Ingredient i in contents)
        {
            itemIndexs.Add(i.IngredientIndex);
        }

        StringBuilder sb = new();
        foreach (Ingredient i in contents)
        {
            sb.AppendLine(i.IngredientIndex.ToString());
        }

        recipe = RecipeManager.Instance.GetRecipe(itemIndexs);

        if (recipe == null)
        {
            return;
        }

        if (recipe.type != Type) 
        { 
            Debug.Log("요리 타입이 달라 시작되지 않습니다.");
            recipe = null; 
        }

        if (recipe != null)
        {
            State = CookState.Cooking;
            timer = 0f;

            //  바 초록색으로 시작
            ui.gameObject.SetActive(true);
            ui.BarTurnGreen();
        }
    }

    private void Complete()
    {
        State = CookState.Done;
        foreach (var i in contents)
        {
            if (i != null)
                Destroy(i.gameObject);
        }
        contents.Clear();
        // 완성 음식 표시 
        resultObject = Instantiate(recipe.result, contentsRoot);

        //  바 빨간색으로 변경
        ui.BarTurnRed();
    }

    private void Burn()
    {
        State = CookState.Burnt;
        // 탄 음식 표시
        Destroy(resultObject);
        //resultObject = Instantiate(탄 오브젝트);

        //  탔으면 바 없앰.
        ui.gameObject.SetActive(false);
    }

    public bool TryServeTo(Plate plate)
    {
        if (State != CookState.Done || recipe == null) 
            return false;

        if (plate.TryReceiveDish(resultObject))
        {
            ResetCookware();
            return true;
        }

        return false;
    }


    public void ClearContents()
    {
        foreach (var i in contents)
        {
            if (i != null)
                Destroy(i.gameObject);
        }
        contents.Clear();
        ResetCookware();
    }

    private void ResetCookware()
    {
        State = CookState.Idle;
        resultObject = null;
        recipe = null;
        timer = 0f;
        ui.gameObject.SetActive(false);
    }
}