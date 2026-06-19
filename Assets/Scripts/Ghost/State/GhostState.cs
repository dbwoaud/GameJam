public abstract class GhostState
{
    protected Ghost _ghost;
    protected GhostData _data;
    protected GhostMovement _movement;


    public abstract void Enter();

    public abstract void Exit();

    public virtual void InitializeStateInfo(Ghost ghost, GhostData data, GhostMovement movement)
    {
        _ghost = ghost;
        _data = data;
        _movement = movement;
    }

}
