using UnityEngine;

public class TrashBin : MonoBehaviour, IInteractable
{
    public void OnGrab(PlayerInput player)
    {
        if (!player.IsHolding) 
            return;
        
        Carryable held = player.HeldItem;
        if (held is CookingTool cookingTool)
        {
            cookingTool.ClearContents();
            return;
        }

        if (held is Plate plate && !plate.IsEmpty)
        {
            plate.ClearDish();
            return;
        }

        Carryable item = player.TakeFromHands();
        Destroy(item.gameObject);
    }

    public void OnInteract(PlayerInput player)
    {

    }
}
