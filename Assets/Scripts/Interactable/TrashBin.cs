using UnityEngine;

public class TrashBin : MonoBehaviour, IInteractable
{
    public void OnGrab(PlayerInput player)
    {
        if (!player.IsHolding) 
            return;
        
        Carryable held = player.HeldItem;
        //  재료 버리는거
        if (held is CookingTool cookingTool1 && cookingTool1.IngredientCount > 0)
        {
            PlayDiscardSound();
            cookingTool1.ClearContents();
            return;
        }

        //  완성된 요리 버리는거
        if (held is CookingTool cookingTool2 && cookingTool2.ResultObject != null)
        {
            PlayDiscardSound();
            cookingTool2.ResetCookware();
            return;
        }

        if (held is Plate plate && !plate.IsEmpty)
        {
            PlayDiscardSound();
            plate.ClearDish();
            return;
        }

        if (held is Ingredient ingredient)
        {
            PlayDiscardSound();
            Carryable item = player.TakeFromHands();
            Destroy(item.gameObject);
        }
    }

    public void OnInteract(PlayerInput player)
    {

    }

    private void PlayDiscardSound()
    {
        SoundManager.Instance.PlayOneShot(ResourceManager.Instance.Load<AudioClip>("PutIngredient"));
    }
}
