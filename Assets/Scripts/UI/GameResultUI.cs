using TMPro;
using UnityEngine;

public class GameResultUI : BaseUI
{
    [SerializeField] private TMP_Text successCountText;
    [SerializeField] private TMP_Text failCountText;

    public void Show(int successCount,int failCount)
    {
        successCountText.text = $"승천한 영혼 : {successCount}";
        failCountText.text = $"실패한 영혼 : {failCount}";
    }

    public void ReturnLobby()
    {
        TimeManager.Instance.SetTimeScale(1);
        SceneControl.Instance.LoadSceneAsync((int)SceneType.LobbyScene);
    }
}