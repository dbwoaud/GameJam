using UnityEngine;
using UnityEngine.EventSystems;

public class StageUI : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] private StageDataSO stageData;
    [SerializeField] private SceneType sceneType;

    public void OnClickGameScene()
    {
        // SceneControl.Instance.LoadScene(SceneType.GameScene);

        SceneControl.Instance.OnComplete += OnComplete;

        if(stageData.sequenceData != null)
        {
            SceneControl.Instance.OnComplete += stageData.sequenceData.StartSequence;
        }

        // SceneControl.Instance.LoadSceneAsync((int)SceneType.GameScene);
        SceneControl.Instance.LoadSceneAsync((int)sceneType);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        var ui = UIManager.Instance.Show<StageInfoUI>();
        ui.Show(stageData);

        ((RectTransform)ui.transform).position = ((RectTransform)transform).position + Vector3.up * 50;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.Instance.Show<StageInfoUI>().Hide();
    }

    private void OnComplete()
    {
        StageManager.Instance.SetStage(stageData);

        SceneControl.Instance.OnComplete -= OnComplete;
    }
}
