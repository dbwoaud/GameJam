public abstract class GhostState
{
    protected Ghost _ghost;
    protected GhostData _data;
    protected GhostMovement _movement;
    protected GhostUI _ui;


    public abstract void Enter();

    public abstract void Exit();

    public virtual void Update() { }

    public virtual void InitializeStateInfo(Ghost ghost, GhostData data, GhostMovement movement, GhostUI ui)
    {
        _ghost = ghost;
        _data = data;
        _movement = movement;
        _ui = ui;
    }

}
