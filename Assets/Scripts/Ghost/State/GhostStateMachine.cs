using UnityEngine;

public class GhostStateMachine
{
    Ghost _ghost;
    GhostMovement _movement;
    GhostData _data;

    GhostState currentState;
    public GhostState CurrentState => currentState;


    public void Initialize(Ghost ghost, GhostData data, GhostMovement movement)
    {
        _ghost = ghost;
        _data = data;
        _movement = movement;
    }

    public void SetInitialState(GhostState initialState)
    {
        currentState = initialState;
        currentState.InitializeStateInfo(_ghost, _data, _movement);
        currentState.Enter();
    }

    public void ChangeState(GhostState stateToChange)
    {
        if (currentState == null) Debug.LogError("[GhostStateMachine] 현재 State가 null이어서 Exit 및 State변경에 문제가 생겼습니다.");

        currentState.Exit();

        currentState = stateToChange;
        currentState.InitializeStateInfo(_ghost, _data, _movement);
        currentState.Enter();
    }
}
