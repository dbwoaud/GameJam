using System.Collections.Generic;
using UnityEngine;

public class TableManager : MonoBehaviour
{
    List<Table> tables;
    //  이걸 얘도 관리 해야겠찌??
    List<bool> tableOccupied;

    #region Public Methods

    public Table GetTable()
    {
        List<Table> availableTables = new();
        for (int i = 0; i < tables.Count; ++i)
        {
            if (tableOccupied[i]) continue;

            availableTables.Add(tables[i]);
        }

        if (availableTables.Count < 1) return null;

        int returnTableIndex = Random.Range(0, tables.Count);

        tableOccupied[returnTableIndex] = true;

        return tables[returnTableIndex];
    }
    #endregion

    #region Private Methods


    #endregion
}
