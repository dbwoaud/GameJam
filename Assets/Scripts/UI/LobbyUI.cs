using System;
using UnityEngine;

public class LobbyUI : BaseUI
{
    void Start()
    {
        UIManager.Instance.Show<ControlInfoUI>();
    }

    public void OnClickSetting()
    {
        UIManager.Instance.Show<SettingUI>();
    }

    public void OnClickCotronInfo()
    {
        UIManager.Instance.Show<ControlInfoUI>();
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
