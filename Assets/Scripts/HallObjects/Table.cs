using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public enum TableState { Ready, Reversed, Restoring }

public class Table : MonoBehaviour, IInteractable
{
    [SerializeField] Transform GhostWaitingPositionTransform;
    public Vector3 GhostWaitingPosition => GhostWaitingPositionTransform.position;

    [SerializeField] Transform tableModel;

    [SerializeField] Transform socket;
    Plate onPlate = null;

    Ghost assignedGhost;

    private bool isOccupied = false;
    public bool IsOccupied => isOccupied;

    TableState state = TableState.Ready;
    public TableState State => state;

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
    public void TableFlip(TableState toState)
    {
        if (toState == state) return;
        
        Vector3 rot = toState == TableState.Ready ? new Vector3(0, 0, 0) : new Vector3(-180, 0, 0);

        if (toState == TableState.Reversed && onPlate != null) ThrowPlate();

        Sequence s = DOTween.Sequence();
        s.Append(tableModel.DOLocalJump(Vector3.zero, jumpPower, 1, jumpDuration));
        s.Join(tableModel.DORotate(rot, jumpDuration));

        s.AppendCallback(() => { state = toState; });
    }

    private void ThrowPlate()
    {
        Collider c = onPlate.GetComponent<Collider>();
        c.enabled = false;

        Vector3 endPoint = new Vector3(transform.position.x, 0, transform.position.z) + new Vector3(2 * Mathf.Cos(Time.time), 0, 2 * Mathf.Sin(Time.time));

        Sequence s = DOTween.Sequence();
        s.Append(onPlate.transform.DOJump(endPoint, jumpPower, 1, 1f));
        s.AppendCallback(() => { c.enabled = true; });
    }

    #region IInteractable

    public void OnGrab(PlayerInput player)
    {
        Carryable held = player.HeldItem;

        if (held is Plate plate)
        {
            if (assignedGhost == null) return;
            if (onPlate != null) return;
            onPlate = plate;

            //  빈접시인 경우
            if (plate.FoodGO == null)
            {
                assignedGhost.StateMachine.ChangeState(new LeavingState(isSuccess: false));
                plate.AttachTo(socket);
                player.TakeFromHands();
                return;
            }

            //  접시가 있고, 음식이 있고, 정답
            if (plate.FoodGO.GetComponent<Ingredient>().IngredientIndex == assignedGhost.Data.orderFoodSO.itemIndex)
            {
                assignedGhost.StateMachine.ChangeState(new EatingState(plate));
                plate.AttachTo(socket);
                player.TakeFromHands();
                return;
            }

            //  접시가 있고, 음식이 있고, 오답.
            if (plate.FoodGO.GetComponent<Ingredient>().IngredientIndex != assignedGhost.Data.orderFoodSO.itemIndex)
            {
                assignedGhost.StateMachine.ChangeState(new LeavingState(isSuccess: false));
                plate.AttachTo(socket);
                player.TakeFromHands();
                return;
            }

        }

        if (!player.IsHolding && onPlate != null)
        {
            Debug.Log("여기");
            player.Hold(onPlate);
            onPlate = null;
            return;
        }
    }

    public void OnInteract(PlayerInput player)
    {
        if (state == TableState.Restoring) return;

        if (state == TableState.Reversed)
        {
            TableFlip(TableState.Ready);
        }
    }
    #endregion

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

    [Button]
    public void PlateTest(PlayerInput player)
    {
        Debug.Log($"플레이어 손이 비어있는가: {!player.IsHolding}");
        Debug.Log($"테이블 위에 접시가 있는가: {onPlate != null}");
    }
    #endregion
}
