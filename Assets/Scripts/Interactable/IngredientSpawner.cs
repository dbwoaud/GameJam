using UnityEngine;
using DG.Tweening;

public class IngredientSpawner : MonoBehaviour, IInteractable
{
    [SerializeField] private Ingredient ingredientPrefab;
    [SerializeField] Transform coverPivot;

    Sequence seq;

    public void OnGrab(PlayerInput player)
    {
        if (player.IsHolding) 
            return;               
        if (ingredientPrefab == null) 
            return;

        SoundManager.Instance.PlayOneShot(ResourceManager.Instance.Load<AudioClip>("TakeIngredient"));

        
        if (seq.IsActive()) seq.Kill();
        seq = DOTween.Sequence();
        seq.Append(coverPivot.DOLocalRotate(new Vector3(-60f, 0, 0), 0.2f));
        seq.Append(coverPivot.DOLocalRotate(Vector3.zero, 0.2f));

        Ingredient ingredient = Instantiate(ingredientPrefab, Vector3.zero, Quaternion.identity);
        player.Hold(ingredient);
    }

    public void OnInteract(PlayerInput player)
    {
        
    }
}
