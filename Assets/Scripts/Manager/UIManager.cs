using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private Canvas canvas;

    private Dictionary<Type,BaseUI> uiDict= new Dictionary<Type, BaseUI>();

    public BaseUI Show<T>(string uiName = "") where T : BaseUI
    {
        if(canvas == null)
        {
            CraeteCanvas();
        }

        T ui = null;

        if(string.IsNullOrEmpty(uiName))
        {
            uiName = typeof(T).Name;
        }

        if(uiDict.ContainsKey(typeof(T)) == false)
        {
            var asset = ResourceManager.Instance.Load<T>(uiName);
            
            var prefab = Instantiate(asset,canvas.transform);
            uiDict.Add(typeof(T),prefab);
        }

        return ui;
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
