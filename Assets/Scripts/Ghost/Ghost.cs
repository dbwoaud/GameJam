using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] GhostMovement movement;
    GhostStateMachine stateMachine;
    GhostData data;

    public GhostStateMachine StateMachine => stateMachine;

    private void Awake()
    {
        stateMachine = new GhostStateMachine();
        data = new GhostData();
        stateMachine.Initialize(this, data, movement);
    }

    private void Start()
    {
    }

    public void SetInfo(Vector3 movePoint)
    {
        data.tablePoint = movePoint;

        stateMachine.SetInitialState(new EnteringState());
    }



}
