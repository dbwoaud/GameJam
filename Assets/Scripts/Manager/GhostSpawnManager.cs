using Sirenix.OdinInspector;
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

    [Button]
    public void SpawnGhost(Vector3 spawnPoint, Vector3 tablePoint)
    {
        Ghost ghost = Instantiate(ghostPrefab, spawnPoint, Quaternion.identity);

        ghost.SetInfo(tablePoint);
    }





}
