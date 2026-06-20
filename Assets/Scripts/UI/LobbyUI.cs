using System;
using UnityEngine;

public class LobbyUI : BaseUI
{
    public StageDataSO testData;

    public void OnClickGameScene()
    {
        // SceneControl.Instance.LoadScene(SceneType.GameScene);

        SceneControl.Instance.OnComplete += OnComplete;

        SceneControl.Instance.LoadSceneAsync((int)SceneType.GameScene);
    }

    private void SetStageData()
    {
        
    }

    private void OnComplete()
    {
        StageManager.Instance.SetStage(testData);

        SceneControl.Instance.OnComplete -= OnComplete;
    }
}
