using UnityEngine;

public class Table : MonoBehaviour, IInteractable
{
    public enum TableState { Ready, Toppled, Restoring }

    [Header("되돌리기")]
    [SerializeField] private int restoreSteps = 6;
    [SerializeField] private float abandonResetDelay = 0.6f;

    [Header("모델")]
    [SerializeField] private GameObject readyModel;
    [SerializeField] private GameObject toppledModel;

    [Header("초기 상태")]
    [SerializeField] private bool startToppled = false;

    private int restoreCount;
    private float lastInteractTime;

    public TableState State { get; private set; } = TableState.Ready;
    public bool IsReady => State == TableState.Ready;
    public bool IsToppled => State == TableState.Toppled || State == TableState.Restoring;
    public float RestoreProgress01 =>
        restoreSteps <= 0 ? 1f : Mathf.Clamp01((float)restoreCount / restoreSteps);

    private void Awake()
    {
        ApplyState(startToppled ? TableState.Toppled : TableState.Ready);
    }

    private void Update()
    {
        if (State == TableState.Restoring && Time.time - lastInteractTime > abandonResetDelay)
            FlipBack();
    }

    public void OnGrab(PlayerInput player) { }

    public void OnInteract(PlayerInput player)
    {
        if (State == TableState.Ready)
            return;

        if (State == TableState.Toppled)
            State = TableState.Restoring;

        restoreCount++;
        lastInteractTime = Time.time;

        // 진행도 바 UI 업데이트

        if (restoreCount >= restoreSteps)
            Restore();
    }

    public void Topple()
    {
        restoreCount = 0;
        ApplyState(TableState.Toppled);
    }

    public void CancelRestore()
    {
        if (State == TableState.Restoring)
            FlipBack();
    }

    private void Restore()
    {
        restoreCount = restoreSteps;
        ApplyState(TableState.Ready);
    }

    private void FlipBack()
    {
        restoreCount = 0;
        ApplyState(TableState.Toppled);
    }

    private void ApplyState(TableState next)
    {
        State = next;

        bool ready = next == TableState.Ready;
        if (readyModel != null) 
            readyModel.SetActive(ready);
        if (toppledModel != null) 
            toppledModel.SetActive(!ready);
    }
}