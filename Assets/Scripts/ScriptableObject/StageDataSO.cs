using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StageDataSO", menuName = "GamePlay/StageDataSO", order = 0)]
public class StageDataSO : ScriptableObject
{
    [SerializeField] private List<CookDataSO> targetCookList;

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
