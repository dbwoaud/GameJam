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

        //  접시를 들고 있고, 스토브에 음식이 있을 때
        if (held is Plate plate && onStove != null && onStove.IsDone)
        {
            onStove.TryServeTo(plate);
            return;
        }

        //  손에 든 조리도구를 스토브에 올려놓음
        if (held is CookingTool && onStove == null)
        {
            onStove = (CookingTool)player.TakeFromHands();
            onStove.AttachTo(socket != null ? socket : transform);
            return;
        }

        //  손에 든 재료를 스토브의 조리도구 안에 넣음
        if (held is Ingredient ingredient && onStove != null)
        {
            if (onStove.TryPush(ingredient)) 
                player.TakeFromHands();
            return;
        }

        //  스토브에 올려진 조리도구를 집음.
        if (!player.IsHolding && onStove != null)
        {
            player.Hold(onStove);
            onStove = null;
        }
    }

    public void OnInteract(PlayerInput player) { }
}
