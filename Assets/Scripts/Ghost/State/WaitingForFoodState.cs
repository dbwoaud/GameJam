using UnityEngine;

public class WaitingForFoodState : GhostState
{
    const float patience = 60f;
    float remainingPatience;

    public override void Enter()
    {
        remainingPatience = patience;

        _ghost.GetRandomFoodData();
        _ghost.ShowFoodUI();
    }

    public override void Exit()
    {
    }

    public override void Update()
    {
        // remainingPatience -= Time.deltaTime;
        remainingPatience -= TimeManager.Instance.deltaTime;

        float fillAmount = remainingPatience / patience;

        _ghost.ShowPatience(fillAmount);
        if (remainingPatience < 0) 
        {
            _ghost.StateMachine.ChangeState(new LeavingState(isSuccess: false));
        }
    }
}
