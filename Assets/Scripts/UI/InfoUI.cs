using System;
using TMPro;
using UnityEngine;

public class InfoUI : BaseUI
{
    [SerializeField] private TMP_Text infoText;
    private Action confirmAction;

    public void SetUI(string msg,Action action)
    {
        base.Show();
        infoText.text= msg;
        confirmAction = action;
        TimeManager.Instance.SetTimeScale(0f);
    }

    public void OnClickYes()
    {
        confirmAction?.Invoke();
    }

    public void OnClickNo()
    {
        Hide();
    }

    public override void Hide()
    {
        base.Hide();
        TimeManager.Instance.SetTimeScale(1);
    }
}
