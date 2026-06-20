using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class InGameUI : BaseUI
{
    [SerializeField] private TMP_Text targetText;
    [SerializeField] private TMP_Text timerText;

    [SerializeField] private Transform recipeTR;

    private List<RecipeInfoUI> recipeUIList = new List<RecipeInfoUI>();
    void Start()
    {
        StageManager.Instance.OnUpdateCount += SetTargetText;
        StageManager.Instance.OnTimer += SetTimerText;
    }

    public void SetTimerText(float playTime,float limitTime)
    {
        float remainTime = limitTime - playTime;

        int minute = (int)remainTime / 60;
        int second = (int)remainTime % 60;

        string minuteStr = minute.ToString("00");
        string secondStr = second.ToString("00");

        timerText.text = $"{minuteStr}:{secondStr}";
    }    

    public void SetTargetText(int count,int target)
    {
        targetText.text = $"{count} / {target}";
    }

    public void OnClickGameStop()
    {
        var ui = UIManager.Instance.Show<InfoUI>();
        ui.SetUI("로비로 돌아가시겠습니까?",ReturnLobby);
    }

    public void OnClickSetting()
    {
        TimeManager.Instance.SetTimeScale(0f);
        UIManager.Instance.Show<SettingUI>();
    }

    private void ReturnLobby()
    {
        SceneControl.Instance.LoadScene(SceneType.LobbyScene);
        TimeManager.Instance.SetTimeScale(1);
    }

    public void OnClickTutorial()
    {
        var ui = UIManager.Instance.Show<TutorialUI>();
        ui.Show();
    }

    public RecipeInfoUI AddOrder(CookDataSO dataSO)
    {
        var prefab = ResourceManager.Instance.Load<RecipeInfoUI>("RecipeInfoUI");
        var ui = Instantiate(prefab,recipeTR);

        // var ui = g.GetComponent<RecipeInfoUI>();

        ui.Show(dataSO);

        recipeUIList.Add(ui);

        return ui;
    }
}