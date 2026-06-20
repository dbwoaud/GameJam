using UnityEngine;

public class IngredientSpawner : MonoBehaviour, IInteractable
{
    [SerializeField] private Ingredient ingredientPrefab;
    [SerializeField] Transform coverPivot;

    public void OnGrab(PlayerInput player)
    {
        if (player.IsHolding) 
            return;               
        if (ingredientPrefab == null) 
            return;

        SoundManager.Instance.PlayOneShot(ResourceManager.Instance.Load<AudioClip>("TakeIngredient"));

        Ingredient ingredient = Instantiate(ingredientPrefab, Vector3.zero, Quaternion.identity);
        player.Hold(ingredient);
    }

    public void OnInteract(PlayerInput player)
    {
        
    }
}
