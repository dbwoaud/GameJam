using DG.Tweening;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    [SerializeField] float moveDuration = 10f;

    public Sequence MoveTo(Vector3 destination)
    {
        transform.LookAt(destination);

        Sequence s = DOTween.Sequence();
        s.Append(transform.DOMove(destination, moveDuration));

        return s;
    }

    public void ToHeaven()
    {
        Sequence s = DOTween.Sequence();
        s.Append(transform.DOMoveY(10f, moveDuration));

        s.AppendCallback(() => Destroy(gameObject));
    }

    public void Leave()
    {
        Vector3 destination = GhostSpawnManager.Instance.GetRandomSpawnPosition();

        Sequence s = DOTween.Sequence();
        s.Append(transform.DOMove(destination, moveDuration));

        s.AppendCallback(() => Destroy(gameObject));
    }
}
