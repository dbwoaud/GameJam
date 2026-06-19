using UnityEngine;

public class LeavingState : GhostState
{
    bool _isSuccess = false;
    public bool IsSuccess => _isSuccess;

    public LeavingState(bool isSuccess)
    {
        _isSuccess = isSuccess;
    }

    public override void Enter()
    {
        _ghost.ShowFace(_isSuccess);
        if (IsSuccess) 
        {
            _movement.ToHeaven(); 
        }
        else
        {
            //  실패시 뒤엎고, 밖으로 걸어나가기
            _data.table.TableFlip(false);
            _movement.Leave();
        }

        _data.table.FreeTable();

    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}
