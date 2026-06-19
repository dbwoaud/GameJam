using System;
using UnityEngine;

public class StageManager : Singleton<StageManager>
{
    [field:SerializeField] public StageDataSO stageData { get; private set;}

    protected override void Awake()
    {
        isDestroyable = true;
        base.Awake();
    }

    public void SetStageData(StageDataSO stageData)
    {
        this.stageData = stageData;
    }
}
