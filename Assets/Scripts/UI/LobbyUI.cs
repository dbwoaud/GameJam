using System;
using UnityEngine;

public class LobbyUI : BaseUI
{
    public void OnClickSetting()
    {
        UIManager.Instance.Show<SettingUI>();
    }

    // public StageDataSO testData;

    // public void OnClickGameScene()
    // {
    //     // SceneControl.Instance.LoadScene(SceneType.GameScene);

    //     SceneControl.Instance.OnComplete += OnComplete;

    //     SceneControl.Instance.LoadSceneAsync((int)SceneType.GameScene);
    // }

    // private void OnComplete()
    // {
    //     StageManager.Instance.SetStageData(testData);

    //     SceneControl.Instance.OnComplete -= OnComplete;
    // }
}
