using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private Canvas canvas;

    private Dictionary<Type,BaseUI> uiDict= new Dictionary<Type, BaseUI>(); 
    private Stack<BaseUI> uiStack = new Stack<BaseUI>();

    void Start()
    {
        SceneControl.Instance.OnComplete += ResetDict;
    }

    private void ResetDict()
    {
        uiDict.Clear();
        canvas = null;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(uiStack.Count > 0)
            {
                uiStack.Peek().Hide();
            }
            else
            {
                var ui = Show<InfoUI>();
                ui.SetUI("게임을 종료하시겠습니까?", () => {
                    #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
                    #else
                        Application.Quit();
                    #endif                    
                });
            }
        }
    }

    public T Show<T>(string uiName = "") where T : BaseUI
    {
        if(canvas == null)
        {
            CraeteCanvas();
        }

        if(string.IsNullOrEmpty(uiName))
        {
            uiName = typeof(T).Name;
        }

        if(uiDict.ContainsKey(typeof(T)) == false)
        {
            var asset = ResourceManager.Instance.Load<T>(uiName);
            
            var ui = Instantiate(asset,canvas.transform);
            uiDict.Add(typeof(T),ui);
        }

        var result = uiDict[typeof(T)] as T;
        result.gameObject.SetActive(true);

        uiStack.Push(result);

        return result;
    }

    [ContextMenu("CraeteCanvas")]
    private void CraeteCanvas()
    {
        var g = new GameObject("Canvas");

        canvas =  g.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        var scaler =  g.AddComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
        scaler.referenceResolution = new Vector2(1920,1080);

        g.AddComponent<GraphicRaycaster>();
    }

    public void PopStack()
    {
        uiStack.Pop();
    }
}
