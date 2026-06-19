using DG.Tweening;
using UnityEngine;

public class EnteringState : GhostState
{
    public override void Enter()
    {
        Sequence s = _movement.MoveTo(_data.tablePoint);

        s.AppendCallback(() => _ghost.StateMachine.ChangeState(new WaitingForFoodState()));
    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}
