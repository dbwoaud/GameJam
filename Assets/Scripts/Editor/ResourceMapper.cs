using UnityEngine;
using UnityEditor;
using System.IO;

public class ResourceMapper : Editor
{
    [MenuItem("Tools/ResourceMapping")]
    public static void Mapping()
    {
        string path = Path.Combine(Application.dataPath,"Resources"); //Resources 폴더 경로

        var wrapperData = GetWrapperData(path); // 매핑 데이터 내용

        var jsonData = JsonUtility.ToJson(wrapperData);

        string savePath = Path.Combine(path,"MappingData.json"); //저장경로

        File.WriteAllText(savePath,jsonData);

        Debug.Log("매핑데이터 생성 완료");
        Debug.Log($"아이템 수 : {wrapperData.resourceDatas.Count}");
        Debug.Log($"결과 : {jsonData}");
    }

    private static ResourceWrapperData GetWrapperData(string folderPath)
    {
        ResourceWrapperData wrapper = new ResourceWrapperData();
        var directories = Directory.GetDirectories(folderPath);

        foreach(var d in directories)
        {
            var wrapper2 = GetWrapperData(d);
            wrapper.AddRange(wrapper2.resourceDatas);
        }

        var files = Directory.GetFiles(folderPath);

        foreach(var f in files)
        {
            var fileName = Path.GetFileName(f);
            var filePath = f.Replace(Application.dataPath,"Assets").Replace("\\","/");

            ResourceData data = new ResourceData(fileName,filePath);

            wrapper.Add(data);
        }

        return wrapper;
    }
}
