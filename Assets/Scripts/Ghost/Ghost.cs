using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] GhostMovement movement;
    [SerializeField] GhostUI ui;
    GhostStateMachine stateMachine;
    GhostData data;

    public GhostStateMachine StateMachine => stateMachine;

    private void Awake()
    {
        stateMachine = new GhostStateMachine();
        data = new GhostData();
        stateMachine.Initialize(this, data, movement, ui);
    }

    private void Start()
    {
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void SetTableInfo(Table1 table)
    {
        data.table = table;

        stateMachine.SetInitialState(new EnteringState());
    }

    public void GetRandomFoodData()
    {
        data.orderingFood = TempFoodManager.Instance.GetRandomFood();
    }

    #region CallUI ui 콜 경유가 많아지면 서로 참조하도록 하겠음.

    public void ShowFoodUI()
    {
        ui.ShowFood(data.orderingFood);
    }

    public void ShowPatience(float fillAmount)
    {
        ui.ShowPatience(fillAmount);
    }

    public void ShowFace(bool isHappy)
    {
        ui.ShowFace(isHappy);
    }
    #endregion
}
