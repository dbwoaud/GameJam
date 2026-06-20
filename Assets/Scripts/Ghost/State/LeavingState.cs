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
            SoundManager.Instance.PlayOneShot(ResourceManager.Instance.Load<AudioClip>("Holy"));
            _movement.ToHeaven(); 
            StageManager.Instance.CountingTarget();
        }
        else
        {
            //  ���н� �ھ���, ������ �ɾ����
            _data.table.TableFlip(TableState.Reversed);
            _movement.Leave();
        }

        _data.table.FreeTable();

    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }
}
