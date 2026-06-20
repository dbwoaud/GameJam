using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageDataSO", menuName = "GamePlay/StageDataSO", order = 0)]
public class StageDataSO : ScriptableObject
{
    [SerializeField] private List<CookDataSO> targetCookList;
    [field:SerializeField] public int targetCount {get; private set;} //성불시킬 혼 숫자
    [field:SerializeField] public float  limitTime {get; private set;} //시간제한

    public List<CookDataSO> GetCookList()
    {
        return targetCookList;
    }

    public CookDataSO GetRandomFood()
    {
        int index = Random.Range(0, targetCookList.Count);
        return targetCookList[index];
    }
}
