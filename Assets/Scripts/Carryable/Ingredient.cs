using UnityEngine;

public class Ingredient : Carryable
{
    [Header("재료")]
    [SerializeField] private string ingredientName;
    [SerializeField] private int chopStepsRequired = 6;   // 6번 = 100%

    [Header("모델 교체")]
    [SerializeField] private GameObject rawModel;
    [SerializeField] private GameObject choppedModel;

    private int chopCount;

    public string IngredientName => ingredientName;
    public bool IsChopped => chopCount >= chopStepsRequired;
    public float ChopProgress01 => Mathf.Clamp01((float)chopCount / chopStepsRequired);

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
        if (rawModel != null) 
            rawModel.SetActive(false);
        if (choppedModel != null) 
            choppedModel.SetActive(true);
    }
}