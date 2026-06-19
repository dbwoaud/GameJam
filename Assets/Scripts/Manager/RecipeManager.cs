using System.Collections.Generic;

public class RecipeManager : Singleton<RecipeManager>
{
    private bool isInit = false;
    private Dictionary<CookData,HashSet<int>> recipePool = new Dictionary<CookData, HashSet<int>>();

    public void Init()
    {
        if(isInit || ResourceManager.Instance.isInit == false)
        {
            if(ResourceManager.Instance.isInit == false)
            {
                Logger.LogError("ResourceManager 매니저 초기화 이전 호출 에러");
            }

            return;
        }

        var list = ResourceManager.Instance.LoadAll<CookDataSO>("ScriptableObject");

        foreach(var c in list)
        {
            Logger.Log($"레시피 이름 : {c.name}");

            HashSet<int> hashSet = new HashSet<int>(c.cookData.itemIndexs);
            recipePool.Add(c.cookData,hashSet);
        }

        isInit = true;

        Logger.Log($"레시피 초기화 완료");
    }

    public CookData GetRecipe(List<int> itemIndexs)
    {
        CookData data = null;

        foreach(var r in recipePool)
        {
            var recipe = r.Key;
            var hashSet = r.Value;

            if(hashSet.SetEquals(itemIndexs))
            {
                if(data == null)
                {
                    data = recipe;
                }
                else
                {
                    return null;
                }
            }
        }

        return data;
    }
} 
