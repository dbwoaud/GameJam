using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class Plate : Carryable
{
    [Header("Á¢½Ã")]
    [SerializeField] private GameObject cleanPlate;  // ±ú²ýÇÑ Á¢½Ã ÇÁ¸®ÆÕ
    [SerializeField] private GameObject dirtyPlate; // ´õ·¯¿î Á¢½Ã ÇÁ¸®ÆÕ


    [Header("°ãÃÄ ½×±â")]
    [SerializeField] private float stackHeight = 0.05f;
    private readonly List<Plate> plateStack = new List<Plate>();

    public DishType Dish { get; private set; } = DishType.None;
    public bool IsDirty { get; private set; }
    public bool IsEmpty => Dish == DishType.None && !IsDirty;

    public int StackCount => plateStack.Count + 1;
    public bool HasStack => plateStack.Count > 0;
    public Plate TopPlate => plateStack.Count > 0 ? plateStack[plateStack.Count - 1] : this;

    public bool TryReceiveDish(DishType dish)
    {
        if (IsDirty || Dish != DishType.None || dish == DishType.None || HasStack) 
            return false;
        Dish = dish;
        // Á¢½Ã¿¡ ´ã±ä À½½Ä Ç¥½Ã
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

    public bool TryStack(Plate other)
    {
        if (other == null || other == this) 
            return false;
        if (Dish != DishType.None || other.Dish != DishType.None) 
            return false;
        if (plateStack.Contains(other)) 
            return false;

        List<Plate> moving = new List<Plate>(other.plateStack.Count + 1) { other };
        moving.AddRange(other.plateStack);
        other.plateStack.Clear();

        foreach (var p in moving)
        {
            plateStack.Add(p);
            p.SetStacked(this, plateStack.Count);
        }
        return true;
    }

    public Plate PopTop()
    {
        if (plateStack.Count == 0) return null;
        int last = plateStack.Count - 1;
        Plate top = plateStack[last];
        plateStack.RemoveAt(last);
        top.ClearStacked();
        return top;
    }

    public Plate FindDirty()
    {
        for (int i = plateStack.Count - 1; i >= 0; i--)
        {
            if (plateStack[i].IsDirty) return plateStack[i];
        }
        return IsDirty ? this : null;
    }

    private void SetStacked(Plate bottom, int indexFromBottom)
    {
        transform.SetParent(bottom.transform);
        transform.localPosition = new Vector3(0f, stackHeight * indexFromBottom, 0f);
        transform.localRotation = Quaternion.identity;

        var col = GetComponent<Collider>();
        if (col != null) col.enabled = false;
        var rb = GetComponent<Rigidbody>();
        if (rb != null) rb.isKinematic = true;
    }

    private void ClearStacked()
    {
        transform.SetParent(null);
        var col = GetComponent<Collider>();
        if (col != null) col.enabled = true;
    }
}