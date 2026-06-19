using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class GhostSpawnManager : MonoBehaviour
{
    #region Singleton
    private static GhostSpawnManager instance;
    public static GhostSpawnManager Instance => instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] Ghost ghostPrefab;

    //  이거 List로 하지 말고 그냥 특정 범위안에서 랜덤으로 해도 될 듯?? 일단 냅두자.
    [SerializeField] List<Transform> ghostSpawnPositions;

    [Button]
    public void SpawnGhost()
    {
        Table assignedTable = TableManager.Instance.GetTable();
        if (assignedTable == null)
        {
            Debug.Log("[GhostSpawnManager] 귀신 생성 요청중에 빈 테이블이 없어 소환할 수 없습니다.");
            return;
        }

        Ghost ghost = Instantiate(ghostPrefab, GetRandomSpawnPosition(), Quaternion.identity);

        ghost.SetInfo(assignedTable.GhostWaitingPosition);

        assignedTable.SetOccupied();
    }

    private Vector3 GetRandomSpawnPosition()
    {
        int index = Random.Range(0, ghostSpawnPositions.Count);

        return ghostSpawnPositions[index].position;
    }



}
