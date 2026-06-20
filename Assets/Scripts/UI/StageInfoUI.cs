using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageInfoUI : BaseUI
{
    [SerializeField] private TMP_Text targetCountText;
    [SerializeField] private TMP_Text limitTimeText;
    [SerializeField] private List<TMP_Text> cookMenuText;


    public void Show(StageDataSO dataSO)
    {
        targetCountText.text = $"{dataSO.targetCount} 귀신 성불";
        limitTimeText.text = $"{dataSO.limitTime}초";

        var cookList = dataSO.GetCookList();

        for(int i = 0; i < cookMenuText.Count; i++)
        {
            if(i < cookList.Count)
            {
                cookMenuText[i].text = cookList[i].cookData.cookName;
                cookMenuText[i].gameObject.SetActive(true);
            }
            else
            {
                cookMenuText[i].gameObject.SetActive(false);
            }
        }
    }
}
