using DG.Tweening;

public class EnteringState : GhostState
{
    public override void Enter()
    {
        Sequence s = _movement.MoveTo(_data.table.GhostWaitingPosition);

        s.AppendCallback(() => _ghost.StateMachine.ChangeState(new WaitingForFoodState()));
    }

    public override void Exit()
    {
    }
}
