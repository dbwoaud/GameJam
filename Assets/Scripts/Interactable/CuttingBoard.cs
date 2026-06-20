using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform socket;
    [SerializeField] List<CookDataSO> slicableIngredients;
    [SerializeField] ParticleSystem chopVFX;
    List<int> slicableIngredientIndexes;

    private Ingredient onBoard;

    private void Start()
    {
        slicableIngredientIndexes = new();
        foreach (CookDataSO c in slicableIngredients)
        {
            if (c.cookData.itemIndexs.Count > 1) { Debug.LogError("당신은 실수를 저질렀씁니다."); continue; }
            slicableIngredientIndexes.Add(c.cookData.itemIndexs[0]);
        }
    }

    public void OnGrab(PlayerInput player)
    {
        //  든 재료 도마에 놓기
        if (onBoard == null && player.HeldItem is Ingredient ingredient)
        {
            if (!slicableIngredientIndexes.Contains(ingredient.IngredientIndex)) return;

            ingredient.SetCuttingBoard(this);

            onBoard = (Ingredient)player.TakeFromHands();
            onBoard.AttachTo(socket != null ? socket : transform);
            return;
        }

        //  도마에 있는 재료 들기
        if (onBoard != null && !player.IsHolding)
        {
            onBoard.SetCuttingBoard(null);
            player.Hold(onBoard);
            onBoard = null;
        }
    }

    public void OnInteract(PlayerInput player)
    {
        if (onBoard == null) return;

        //  이미 썰린 재료가 됐을 때 썰 수 없는 것일 가능성 높아서 return
        if (!slicableIngredientIndexes.Contains(onBoard.IngredientIndex)) return;

        Debug.Log("인터렉트");
        ParticleSystem ps = Instantiate(chopVFX, socket.transform.position, socket.transform.rotation);
        ps.Play();
        Destroy(ps.gameObject, 1.5f);
        onBoard.Chop();
    }

    public void OnChopComplete()
    {
        foreach (CookDataSO c in slicableIngredients)
        {
            if (c.cookData.itemIndexs.Count > 1) { Debug.LogError("당신은 실수를 저질렀씁니다."); continue; }
            if (c.cookData.itemIndexs[0] == onBoard.IngredientIndex)
            {
                Destroy(socket.GetChild(0).gameObject);
                GameObject slicedIngredient = Instantiate(c.cookData.result, socket);
                onBoard = slicedIngredient.GetComponent<Ingredient>();
            }
        }
    }

    [Button]
    public void Test()
    {
        Debug.Log($"[CuttingBoard] {onBoard.name}");
    }
}