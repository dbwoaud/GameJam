using UnityEngine;


public class Plate : Carryable
{
    [Header("접시")]
    [SerializeField] private GameObject cleanPlate;  // 깨끗한 접시 프리팹
    [SerializeField] private GameObject dirtyPlate; // 더러운 접시 프리팹
   
    public DishType Dish { get; private set; } = DishType.None;
    public bool IsDirty { get; private set; }
    public bool IsEmpty => Dish == DishType.None && !IsDirty;

    public bool TryReceiveDish(DishType dish)
    {
        if (IsDirty || Dish != DishType.None || dish == DishType.None) 
            return false;
        Dish = dish;
        // 접시에 담긴 음식 표시
        return true;
    }

    public void ClearDish() => Dish = DishType.None;

    public void MakeDirty()
    {
        Dish = DishType.None;
        IsDirty = true;
        if (cleanPlate != null)
            cleanPlate.SetActive(false);
        if (dirtyPlate != null) 
            dirtyPlate.SetActive(true);
    }


    public void Wash()
    {
        IsDirty = false;
        if (dirtyPlate != null) 
            dirtyPlate.SetActive(false);
        if (cleanPlate != null)
            cleanPlate.SetActive(true);
    }
}