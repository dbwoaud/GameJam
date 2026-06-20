using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class MapEditorWindow : EditorWindow
{
    // --- 그리드 설정 ---
    private int gridWidth = 10;
    private int gridHeight = 10;
    private float cellSize = 1f;

    // --- 배치(팔레트) 설정 ---
    private bool isPaintMode = false;
    private List<GameObject> prefabsToPlace = new List<GameObject>();
    private int selectedPrefabIndex = 0;

    // --- 구조 정리 설정 ---
    private GameObject mapRoot; // 모든 사물이 들어갈 부모 오브젝트

    [MenuItem("Tools/Map Editor")]
    public static void ShowWindow()
    {
        GetWindow<MapEditorWindow>("Map Editor");
    }

    private void OnEnable()
    {
        SceneView.duringSceneGui += OnSceneGUI;
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }

    private void OnGUI()
    {
        // 1. 그리드 설정부
        GUILayout.Label("그리드 설정 (Grid Settings)", EditorStyles.boldLabel);
        gridWidth = EditorGUILayout.IntField("가로 칸 수 (N)", gridWidth);
        gridHeight = EditorGUILayout.IntField("세로 칸 수 (M)", gridHeight);
        cellSize = EditorGUILayout.FloatField("셀 크기 (Size)", cellSize);
        
        EditorGUILayout.Space();

        // 2. 구조 정리 설정부 (부모 오브젝트)
        GUILayout.Label("구조 설정 (Structure Settings)", EditorStyles.boldLabel);
        mapRoot = (GameObject)EditorGUILayout.ObjectField("부모 오브젝트 (Root)", mapRoot, typeof(GameObject), true);
        if (mapRoot == null)
        {
            EditorGUILayout.HelpBox("부모 오브젝트가 비어있으면 자동으로 'Map_Root'를 생성합니다.", MessageType.Info);
        }

        EditorGUILayout.Space();

        // 3. 프리팹 팔레트 설정부
        GUILayout.Label("사물 팔레트 (Palette)", EditorStyles.boldLabel);
        
        GUI.backgroundColor = isPaintMode ? Color.green : Color.white;
        string buttonText = isPaintMode ? "배치 모드 켜짐 (Paint Mode ON)" : "배치 모드 꺼짐 (Paint Mode OFF)";
        if (GUILayout.Button(buttonText, GUILayout.Height(30)))
        {
            isPaintMode = !isPaintMode;
        }
        GUI.backgroundColor = Color.white;

        if (isPaintMode)
        {
            EditorGUILayout.HelpBox("하단 사물 선택 후 씬 뷰 클릭 시 배치\n[Shift + 클릭] 시 해당 칸의 사물 삭제", MessageType.None);
        }

        EditorGUILayout.Space();

        int newSize = EditorGUILayout.IntField("등록할 사물 개수", prefabsToPlace.Count);
        if (newSize != prefabsToPlace.Count)
        {
            while (newSize > prefabsToPlace.Count) prefabsToPlace.Add(null);
            while (newSize < prefabsToPlace.Count) prefabsToPlace.RemoveAt(prefabsToPlace.Count - 1);
        }

        for (int i = 0; i < prefabsToPlace.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            if (EditorGUILayout.Toggle(selectedPrefabIndex == i, GUILayout.Width(20)))
            {
                selectedPrefabIndex = i;
            }
            prefabsToPlace[i] = (GameObject)EditorGUILayout.ObjectField($"사물 {i + 1}", prefabsToPlace[i], typeof(GameObject), false);
            EditorGUILayout.EndHorizontal();
        }

        if (GUI.changed)
        {
            SceneView.RepaintAll();
        }
    }

    private void OnSceneGUI(SceneView sceneView)
    {
        DrawGrid();

        if (isPaintMode)
        {
            HandleMouseInput();
        }
    }

    private void DrawGrid()
    {
        Handles.color = new Color(0f, 1f, 1f, 0.3f);
        for (int x = 0; x <= gridWidth; x++)
        {
            Vector3 start = new Vector3(x * cellSize, 0, 0);
            Vector3 end = new Vector3(x * cellSize, 0, gridHeight * cellSize);
            Handles.DrawLine(start, end);
        }
        for (int z = 0; z <= gridHeight; z++)
        {
            Vector3 start = new Vector3(0, 0, z * cellSize);
            Vector3 end = new Vector3(gridWidth * cellSize, 0, z * cellSize);
            Handles.DrawLine(start, end);
        }
    }

    private void HandleMouseInput()
    {
        Event e = Event.current;
        int controlID = GUIUtility.GetControlID(FocusType.Passive);
        HandleUtility.AddDefaultControl(controlID);

        // 마우스 왼쪽 버튼 클릭 또는 드래그 시 작동
        if ((e.type == EventType.MouseDown || e.type == EventType.MouseDrag) && e.button == 0)
        {
            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            if (groundPlane.Raycast(ray, out float rayDistance))
            {
                Vector3 hitPoint = ray.GetPoint(rayDistance);

                if (hitPoint.x >= 0 && hitPoint.x <= gridWidth * cellSize &&
                    hitPoint.z >= 0 && hitPoint.z <= gridHeight * cellSize)
                {
                    // Shift 키가 눌려있으면 삭제 모드, 아니면 배치/덮어쓰기 모드
                    if (e.shift)
                    {
                        EraseObjectOnGrid(hitPoint);
                    }
                    else
                    {
                        if (prefabsToPlace.Count > 0 && prefabsToPlace[selectedPrefabIndex] != null)
                        {
                            PlaceObjectOnGrid(hitPoint);
                        }
                    }
                    e.Use();
                }
            }
        }
    }

    private void ProcessContainer()
    {
        // 부모 오브젝트가 명시되어 있지 않다면 자동으로 Scene에서 찾거나 생성
        if (mapRoot == null)
        {
            mapRoot = GameObject.Find("Map_Root");
            if (mapRoot == null)
            {
                mapRoot = new GameObject("Map_Root");
                Undo.RegisterCreatedObjectUndo(mapRoot, "Create Map Root");
            }
        }
    }

    private Vector3 GetSnappedPosition(Vector3 hitPoint)
    {
        int gridX = Mathf.FloorToInt(hitPoint.x / cellSize);
        int gridZ = Mathf.FloorToInt(hitPoint.z / cellSize);

        return new Vector3(
            (gridX * cellSize) + (cellSize / 2f),
            0,
            (gridZ * cellSize) + (cellSize / 2f)
        );
    }

    // 해당 좌표에 이미 오브젝트가 있는지 검사하는 함수
    private GameObject FindObjectAtPosition(Vector3 position)
    {
        if (mapRoot == null) return null;

        foreach (Transform child in mapRoot.transform)
        {
            // 부동 소수점 오차를 고려하여 거리가 매우 가까우면 같은 위치로 판단
            if (Vector3.Distance(child.position, position) < 0.01f)
            {
                return child.gameObject;
            }
        }
        return null;
    }

    private void PlaceObjectOnGrid(Vector3 hitPoint)
    {
        ProcessContainer();
        Vector3 snappedPosition = GetSnappedPosition(hitPoint);
        GameObject existingObject = FindObjectAtPosition(snappedPosition);

        // 동일한 프리팹이 이미 정밀하게 그 자리에 있다면 중복 생성 스킵
        if (existingObject != null)
        {
            // 다른 오브젝트가 있다면 기존 것을 삭제하고 덮어쓰기
            Undo.DestroyObjectImmediate(existingObject);
        }

        // 사물 생성 및 부모 설정
        GameObject prefabToPlace = prefabsToPlace[selectedPrefabIndex];
        GameObject spawnedObject = (GameObject)PrefabUtility.InstantiatePrefab(prefabToPlace);
        spawnedObject.transform.position = snappedPosition;
        spawnedObject.transform.SetParent(mapRoot.transform); // 부모 오브젝트 하위로 구성

        Undo.RegisterCreatedObjectUndo(spawnedObject, "Place Map Object");
    }

    private void EraseObjectOnGrid(Vector3 hitPoint)
    {
        ProcessContainer();
        Vector3 snappedPosition = GetSnappedPosition(hitPoint);
        GameObject existingObject = FindObjectAtPosition(snappedPosition);

        // 해당 칸에 물체가 있다면 삭제
        if (existingObject != null)
        {
            Undo.DestroyObjectImmediate(existingObject);
        }
    }
}