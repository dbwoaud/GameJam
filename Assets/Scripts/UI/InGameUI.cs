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

        Logger.Log($"제한시간 {limitTime}");
        Logger.Log($"플레이타임 {playTime}");
        Logger.Log($"남은시간 {remainTime}");

        string minute = (remainTime / 60f).ToString("00");
        string second = (remainTime % 60f).ToString("00");

        this.timerText.text = $"{minute}:{second}";
    }    

    public void SetTargetText(int count,int target)
    {
        this.targetText.text = $"{count} / {target}";
    }
}