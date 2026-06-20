public class TutorialUI : BaseUI
{
    public override void Show()
    {
        base.Show();
        TimeManager.Instance.SetTimeScale(1);
    }

    public override void Hide()
    {
        base.Hide();
        TimeManager.Instance.SetTimeScale(1);
    }
}
