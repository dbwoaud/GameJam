using UnityEngine;

public class TitleUI : MonoBehaviour
{
    public void GameStart()
    {
        SceneControl.Instance.LoadScene(SceneType.LobbyScene);
    }
}
