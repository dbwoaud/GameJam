using System;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [field:SerializeField] public StageDataSO stageData { get; private set;}

    private int targetCount = 0; //성불 수, 성공 수
    private int failCount = 0;
    private float playTime = 0f; //스테이지 플레이 시간

    public Action<int,int> OnUpdateCount;
    public Action<float,float> OnTimer;


    protected override void Awake()
    {
        isDestroyable = true;
        base.Awake();
    }

    public void SetStage(StageDataSO stageData)
    {
        this.stageData = stageData;
        UIManager.Instance.Show<InGameUI>();
    }

    void Update()
    {
        if(TimeManager.Instance.timeScale >= 0f)
        {
            playTime += TimeManager.Instance.deltaTime;
            OnTimer.Invoke(playTime,stageData.limitTime);

            if(playTime >= stageData.limitTime)
            {
                var ui = UIManager.Instance.Show<GameResultUI>();
                ui.Show(targetCount,failCount);
                
                TimeManager.Instance.SetTimeScale(0f);
            }
        }
    }

    public void CountingTarget()
    {
        targetCount += 1;
        OnUpdateCount?.Invoke(targetCount,stageData.targetCount);
    }

    public void CountingFail()
    {
        failCount += 1;
    }
}
