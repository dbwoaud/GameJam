using System.Collections.Generic;
using UnityEngine;

public class TempFoodManager : MonoBehaviour
{
    #region Singleton
    private static TempFoodManager instance;
    public static TempFoodManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] int currentStage = 0;

    //  임시니깐 진짜 막 쓰겠음.

    List<string> tutorialFoods = new List<string>() { "두부부침", "소고기국" };
    public List<string> TutorialFoods => tutorialFoods;

    List<string> stage1Foods = new List<string>() { "소고기국", "동그랑땡", "꼬치전", "장조림" };
    public List<string> Stage1Foods => stage1Foods;

    List<string> stage2Foods = new List<string>() { "생선전", "생선찜", "파전", "떡" };
    public List<string> Stage2Foods => stage2Foods;


    public string GetRandomFood()
    {
        List<string> currentStageFoodList;
        switch (currentStage)
        {
            case 0:
                currentStageFoodList = tutorialFoods;
                break;
            case 1:
                currentStageFoodList = stage1Foods;
                break;
            case 2:
                currentStageFoodList = stage2Foods;
                break;
            default:
                currentStageFoodList = tutorialFoods;
                break;
        }

        if (currentStageFoodList.Count < 1) Debug.LogError("[TempFoodManager] 현재 스테이지 음식 string의 List가 비어있습니다.");

        int rndIndex = Random.Range(0, currentStageFoodList.Count);
        return currentStageFoodList[rndIndex];
    }
}
