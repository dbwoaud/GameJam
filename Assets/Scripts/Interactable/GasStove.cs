using UnityEngine;

public class GasStove : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform socket;
    private CookingTool onStove;

    void Update()
    {
        if (onStove != null) 
            onStove.TickCooking(Time.deltaTime);
    }

    public void OnGrab(PlayerInput player)
    {
        Carryable held = player.HeldItem;
        if (held is Plate plate && onStove != null && onStove.IsDone)
        {
            onStove.TryServeTo(plate);
            return;
        }

        if (held is CookingTool && onStove == null)
        {
            onStove = (CookingTool)player.TakeFromHands();
            onStove.AttachTo(socket != null ? socket : transform);
            return;
        }

        if (held is Ingredient ingredient && onStove != null)
        {
            if (onStove.TryPush(ingredient)) 
                player.TakeFromHands();
            return;
        }

        if (!player.IsHolding && onStove != null)
        {
            player.Hold(onStove);
            onStove = null;
        }
    }

    public void OnInteract(PlayerInput player) { }
}
