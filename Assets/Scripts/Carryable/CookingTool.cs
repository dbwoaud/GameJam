using System.Collections.Generic;
using UnityEngine;

public abstract class CookingTool : Carryable
{
    public abstract CookType Type { get; }

    [Header("재료 스택 표시")]
    [SerializeField] private Transform contentsRoot;
    [SerializeField] private float stackHeight = 0.2f;

    [Header("조리 시간")]
    [SerializeField] private float doneTime = 10f;   
    [SerializeField] private float burnTime = 15f;

    private readonly Stack<Ingredient> contents = new Stack<Ingredient>();
    private float timer;

    public CookState State { get; private set; } = CookState.Idle;
    public DishType Result { get; private set; } = DishType.None;

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
                TryStart(); 
                break;
            case CookState.Cooking: 
                timer += dt; 
                if (timer >= doneTime) 
                    Complete(); 
                break;
            case CookState.Done: 
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

        var types = new List<string>();
        foreach (var i in contents) 
            types.Add(i.IngredientName);

        /* 레시피에 존재하면 요리 시작
        if (RecipeBook.TryMatch(types, Method, out DishType dish))
        {
            Result = dish;
            State = CookState.Cooking;
            timer = 0f;
        }
        */
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
    }

    private void Burn()
    {
        State = CookState.Burnt;
        Result = DishType.None;
        // 탄 음식 표시
    }

    public bool TryServeTo(Plate plate)
    {
        if (State != CookState.Done || Result == DishType.None) 
            return false;
        if (!plate.TryReceiveDish(Result)) 
            return false;
        ResetCookware();
        return true;
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
        Result = DishType.None;
        timer = 0f;
    }
}