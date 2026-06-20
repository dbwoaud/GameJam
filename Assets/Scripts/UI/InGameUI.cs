using UnityEngine;
using TMPro;

public class InGameUI : BaseUI
{
    [SerializeField] private TMP_Text targetText;
    [SerializeField] private TMP_Text timerText;

    void Start()
    {
        StageManager.Instance.OnUpdateCount += SetTargetText;
        StageManager.Instance.OnTimer += SetTimerText;
    }

    public void SetTimerText(float playTime,float limitTime)
    {
        float remainTime = limitTime - playTime;

        string minute = (remainTime / 60f).ToString("N2");
        string second = (remainTime % 60f).ToString("N2");

        this.timerText.text = $"{minute:second}";
    }    

    public void SetTargetText(int count,int target)
    {
        this.targetText.text = $"{count} / {target}";
    }
}