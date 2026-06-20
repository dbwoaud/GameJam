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

        int minute = (int)remainTime / 60;
        int second = (int)remainTime % 60;

        string minuteStr = minute.ToString("00");
        string secondStr = second.ToString("00");

        timerText.text = $"{minuteStr}:{secondStr}";
    }    

    public void SetTargetText(int count,int target)
    {
        targetText.text = $"{count} / {target}";
    }
}