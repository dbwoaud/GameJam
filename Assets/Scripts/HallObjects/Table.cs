using UnityEngine;

public class Table : MonoBehaviour
{
    [SerializeField] Transform GhostWaitingPositionTransform;
    public Vector3 GhostWaitingPosition => GhostWaitingPositionTransform.position;





    private bool isOccupied = false;
    public bool IsOccupied => isOccupied;

    public void SetOccupied()
    {
        isOccupied = true;
    }
}
