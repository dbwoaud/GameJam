using Sirenix.OdinInspector;
using UnityEngine;

public class CuttingBoard : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform socket;

    private Ingredient onBoard;

    public void OnGrab(PlayerInput player)
    {
        if (onBoard == null && player.HeldItem is Ingredient)
        {
            onBoard = (Ingredient)player.TakeFromHands();
            onBoard.AttachTo(socket != null ? socket : transform);
            return;
        }
        if (onBoard != null && !player.IsHolding)
        {
            player.Hold(onBoard);
            onBoard = null;
        }
    }

    public void OnInteract(PlayerInput player)
    {
        Debug.Log("인터렉트");
        if(onBoard != null) 
            onBoard.Chop();
    }

    [Button]
    public void Test()
    {
        Debug.Log($"[CuttingBoard] {onBoard.name}");
    }
}