using UnityEngine;

public class EatingState : GhostState
{
    Plate _plate;

    public EatingState(Plate plate)
    {
        _plate = plate;
    }

    public override void Enter()
    {
        //  음식 비우기
        _plate.TryEat();

        _ghost.StateMachine.ChangeState(new LeavingState(isSuccess: true));
    }

    public override void Exit()
    {
    }
}
