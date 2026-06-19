using DG.Tweening;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [SerializeField] float moveDuration = 10f;

    public Sequence MoveTo(Vector3 destination)
    {
        Sequence s = DOTween.Sequence();
        transform.DOMove(destination, moveDuration);

        return s;
    }











}
