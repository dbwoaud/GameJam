using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private Canvas canvas;

    private Dictionary<Type,BaseUI> uiDict= new Dictionary<Type, BaseUI>();

    void Start()
    {
        SceneControl.Instance.OnComplete += () => uiDict.Clear();
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
}
