using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    #region Singleton
    private static TableManager instance;
    public static TableManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] List<Table> tables;

    #region Public Methods

    public Table GetTable()
    {
        List<Table> availableTables = new();
        foreach (Table t in tables)
        {
            if (t.IsOccupied) continue;
            availableTables.Add(t);
        }

        if (availableTables.Count < 1) return null;

        //  비어있는 테이블중 하나 반환.
        //  테이블 차지 판정은 귀신 소환할 때 해주겠음(이걸 부르는 쪽)
        int returnTableIndex = Random.Range(0, availableTables.Count);
        return availableTables[returnTableIndex];
    }
    #endregion

    #region Private Methods


    #endregion
}
