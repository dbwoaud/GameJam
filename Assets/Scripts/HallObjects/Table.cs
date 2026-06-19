using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Table1 : MonoBehaviour
{
    [SerializeField] Transform GhostWaitingPositionTransform;
    public Vector3 GhostWaitingPosition => GhostWaitingPositionTransform.position;

    [SerializeField] Transform tableModel;

    Ghost assignedGhost;

    private bool isOccupied = false;
    public bool IsOccupied => isOccupied;

    private bool isRightPosition = false;
    public bool IsRightPosition => isRightPosition;

    public void SetOccupied(Ghost ghost)
    {
        isOccupied = true;
        assignedGhost = ghost;
    }

    public void FreeTable()
    {
        isOccupied = false;
        assignedGhost = null;
    }

    [SerializeField] float jumpPower;
    [SerializeField] float jumpDuration;
    [Button]
    public void TableFlip(bool toRightPosition)
    {
        isRightPosition = toRightPosition;
        Vector3 rot = toRightPosition ? new Vector3(0, 0, 0) : new Vector3(-180, 0, 0);

        Sequence s = DOTween.Sequence();
        s.Append(tableModel.DOLocalJump(Vector3.zero, jumpPower, 1, jumpDuration));
        s.Join(tableModel.DORotate(rot, jumpDuration));
    }



    #region Test
    [Button, FoldoutGroup("Test")]
    public void FoodServeSuccess()
    {
        if (assignedGhost == null)
        {
            Debug.Log("할당된 귀신이 없습니다.");
            return;
        }
        assignedGhost.StateMachine.ChangeState(new LeavingState(isSuccess: true));
    }

    [Button, FoldoutGroup("Test")]
    public void FoodServeFail()
    {
        if (assignedGhost == null)
        {
            Debug.Log("할당된 귀신이 없습니다.");
            return;
        }
        assignedGhost.StateMachine.ChangeState(new LeavingState(isSuccess: false));
    }
    #endregion
}
