using UnityEngine;

public class IngredientSpawner : MonoBehaviour, IInteractable
{
    [SerializeField] private Ingredient ingredientPrefab;
    [SerializeField] private Transform spawnPoint;

    public void OnGrab(PlayerInput player)
    {
        if (player.IsHolding) 
            return;               
        if (ingredientPrefab == null) 
            return;

        SoundManager.Instance.PlayOneShot(ResourceManager.Instance.Load<AudioClip>("TakeIngredient"));

        Vector3 pos = spawnPoint != null ? spawnPoint.position : transform.position;
        Ingredient ingredient = Instantiate(ingredientPrefab, pos, Quaternion.identity);
        player.Hold(ingredient);
    }

    public void OnInteract(PlayerInput player)
    {
        
    }
}
