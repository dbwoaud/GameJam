using System;
using System.Collections.Generic;

[Serializable]
public class ResourceData
{
    public string key;
    public string path;

    public ResourceData(string key, string path)
    {
        this.key = key;
        this.path = path;
    }
}

[Serializable]
public class ResourceWrapperData
{
    public List<ResourceData> resourceDatas = new List<ResourceData>();

    public void Add(ResourceData data)
    {
        resourceDatas.Add(data);
    }

    public void AddRange(List<ResourceData> dataList)
    {
        resourceDatas.AddRange(dataList);
    }
}
