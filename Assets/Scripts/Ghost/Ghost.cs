using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField] GhostMovement movement;
    [SerializeField] GhostUI ui;
    GhostStateMachine stateMachine;
    GhostData data;
    public GhostData Data => data;

    public GhostStateMachine StateMachine => stateMachine;

    [SerializeField] CookDataSO temp;

    private RecipeInfoUI myOrder;

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

    public void SetTableInfo(Table table)
    {
        data.table = table;

        stateMachine.SetInitialState(new EnteringState());
    }

    public void GetRandomFoodData()
    {
        //data.orderFoodSO = StageManager.Instance.stageData.GetRandomFood();
        data.orderFoodSO = temp;

        var ui = UIManager.Instance.Show<InGameUI>();
        myOrder = ui.AddOrder(data.orderFoodSO);
    }

    public void DestroyMyOrder()
    {
        Destroy(myOrder);
    }

    #region CallUI ui �� ������ �������� ���� �����ϵ��� �ϰ���.

    public void ShowFoodUI()
    {
        ui.ShowFood(data.orderFoodSO);
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
