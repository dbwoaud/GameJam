using UnityEngine;

[CreateAssetMenu(fileName = "CookDataSO", menuName = "Recipe/CookDataSO", order = 0)]
public class CookDataSO : ScriptableObject
{
    public int itemIndex;
    public CookData cookData;
}
