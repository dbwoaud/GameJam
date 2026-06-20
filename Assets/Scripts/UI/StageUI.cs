using UnityEngine;

public class StageUI : MonoBehaviour
{
    [SerializeField] private StageDataSO stageData;
    [SerializeField] private SceneType sceneType;

    public void OnClickGameScene()
    {
        // SceneControl.Instance.LoadScene(SceneType.GameScene);

        SceneControl.Instance.OnComplete += OnComplete;

        // SceneControl.Instance.LoadSceneAsync((int)SceneType.GameScene);
        SceneControl.Instance.LoadSceneAsync((int)sceneType);
    }

    private void OnComplete()
    {
        StageManager.Instance.SetStage(stageData);

        SceneControl.Instance.OnComplete -= OnComplete;
    }
}
