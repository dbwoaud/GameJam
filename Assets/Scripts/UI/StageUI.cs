using UnityEngine;

public class StageUI : MonoBehaviour
{
    [SerializeField] private StageDataSO stageData;

    public void OnClickGameScene()
    {
        // SceneControl.Instance.LoadScene(SceneType.GameScene);

        SceneControl.Instance.OnComplete += OnComplete;

        SceneControl.Instance.LoadSceneAsync((int)SceneType.GameScene);
    }

    private void OnComplete()
    {
        StageManager.Instance.SetStage(stageData);

        SceneControl.Instance.OnComplete -= OnComplete;
    }
}
