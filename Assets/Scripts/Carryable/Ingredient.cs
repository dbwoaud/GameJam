using System.Collections.Generic;
using UnityEngine;

public class Ingredient : Carryable
{
    [Header("재료")]
    //[SerializeField] private string ingredientName;
    [SerializeField] private int ingredientIndex;
    [SerializeField] private int chopStepsRequired = 6;   // 6번 = 100%

    [Header("모델 교체")]
    [SerializeField] private GameObject rawModel;
    [SerializeField] private GameObject choppedModel;

    [SerializeField] CookDataSO cookDataSO;
    CuttingBoard _cuttingBoard;

    private int chopCount;

    public int IngredientIndex => cookDataSO.itemIndex;
    public bool IsChopped => chopCount >= chopStepsRequired;
    public float ChopProgress01 => Mathf.Clamp01((float)chopCount / chopStepsRequired);

    public void SetCuttingBoard(CuttingBoard cuttingBoard)
    {
        _cuttingBoard = cuttingBoard;
    }

    public void Chop()
    {
        if (IsChopped) 
            return;
        chopCount++;

        //UI 업데이트
        
        if (IsChopped) 
            OnChopCompleted();
    }

    private void OnChopCompleted()
    {
        Debug.Log("자르기 완료");

        if (rawModel != null) 
            rawModel.SetActive(false);
        if (choppedModel != null) 
            choppedModel.SetActive(true);

        _cuttingBoard.OnChopComplete();
    }
}