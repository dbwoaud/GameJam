using DG.Tweening;
using UnityEngine;

public class PlayerVisualInteraction : MonoBehaviour
{
    [SerializeField] GameObject knifePivot;
    [SerializeField] GameObject sponge;
    Tween interactionTween;

    public void ShowSlicing()
    {
        if (interactionTween != null)
        {
            interactionTween.Complete();
        }
        knifePivot.SetActive(true);
        knifePivot.transform.localRotation = Quaternion.identity;

        interactionTween = knifePivot.transform.DOLocalRotate(new Vector3(90f, 0, 0), 0.3f);
        interactionTween.onComplete += HideKnife;
    }

    private void HideKnife()
    {
        knifePivot.SetActive(false);
    }
}
