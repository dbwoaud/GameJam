using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private bool isInit = false;

    private Dictionary<string,string> assetBinding = new Dictionary<string, string>();
    private Dictionary<string,UnityEngine.Object> assetPool = new Dictionary<string, UnityEngine.Object>();

    public void Init()
    {
        if(isInit)
        {
            return;
        }

        var textAsset = Resources.Load<TextAsset>("MappingData");

        if(textAsset == null)
        {
            Logger.LogError($"{GetType()} : 초기화 에러");
            return;
        }

        var mappingData = JsonUtility.FromJson<ResourceWrapperData>(textAsset.text);

        foreach(var asset in mappingData.resourceDatas)
        {
            if(assetBinding.ContainsKey(asset.key))
            {
                continue;
            }

            assetBinding.Add(asset.key,asset.path);
        }

        isInit = true;

        Logger.Log($"초기화 완료");
    }

    public T Load<T>(string assetName) where T : UnityEngine.Object
    {
        if(assetPool.ContainsKey(assetName))
        {
            return (T)assetPool[assetName];
        }

        var assetPath = assetBinding[assetName];
        var asset = Resources.Load<T>(assetPath);        

        if(asset == null)
        {
            Logger.LogError("해당 에셋이 존재하지 않습니다");
            return null;
        }

        assetPool[assetName] = asset;
        return asset;
    }
}