using System;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [field:SerializeField] public StageDataSO stageData { get; private set;}

    private int targetCount = 0; //성불 수
    private float playTime = 0f; //스테이지 플레이 시간

    public Action<int,int> OnUpdateCount;
    public Action<float,float> OnTimer;


    protected override void Awake()
    {
        isDestroyable = true;
        base.Awake();
    }

    public void SetStageData(StageDataSO stageData)
    {
        this.stageData = stageData;
    }

    void Update()
    {
        if(Time.timeScale >= 0f)
        {
            playTime += Time.deltaTime;
            OnTimer.Invoke(playTime,stageData.limitTime);
        }
    }

    private void CountingTarget()
    {
        targetCount += 1;
        OnUpdateCount?.Invoke(targetCount,stageData.targetCount);
    }
}
