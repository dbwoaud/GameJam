using UnityEngine;

public class Sink : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform socket;
    [SerializeField] private int washStepsRequired = 6;

    private Plate plates;
    private Plate washing;
    private int washCount;

    public float WashProgress01 =>
        washing == null ? 0f : Mathf.Clamp01((float)washCount / washStepsRequired);

    public void OnGrab(PlayerInput player)
    {
        if (player.HeldItem is Plate held)
        {
            if (held.Dish != DishType.None)
                return;

            player.TakeFromHands();

            if (plates == null)
            {
                plates = held;
                held.AttachTo(socket != null ? socket : transform);
            }
            else
            {
                plates.TryStack(held);
            }
            return;
        }

        if (!player.IsHolding && plates != null)
        {
            washing = null;
            washCount = 0;

            Plate toGive = plates;
            plates = null;
            player.Hold(toGive);
        }
    }

    public void OnInteract(PlayerInput player)
    {
        if (plates == null)
            return;

        if (washing == null || !washing.IsDirty)
        {
            washing = plates.FindDirty();
            washCount = 0;
        }

        if (washing == null)
            return;

        washCount++;
        // UI 프로그래스바 업데이트

        if (washCount >= washStepsRequired)
        {
            washing.Wash();
            washing = null;
            washCount = 0;
        }
    }
}