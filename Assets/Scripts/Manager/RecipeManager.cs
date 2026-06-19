using System.Collections.Generic;

public class RecipeManager : Singleton<RecipeManager>
{
    private bool isInit = false;
    private List<CookDataSO> recipePool = new List<CookDataSO>();

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
            recipePool.Add(c);
        }

        isInit = true;

        Logger.Log($"레시피 초기화 완료");
    }

    public CookData GetRecipe(int itemIndex)
    {
        CookData data = null;

        foreach(var r in recipePool)
        {
            bool isContain = r.cookData.itemIndexs.Contains(itemIndex); //해당 음식에 매개변수의 아이템이 포함되는지 여부

            if(data == null && isContain)
            {
                data = r.cookData;
            }
            else if(data != null && isContain)
            {
                data = null;
                break;
            }
        }

        return data;
    }
} 
