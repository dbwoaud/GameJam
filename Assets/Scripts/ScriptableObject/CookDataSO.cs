using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CookDataSO", menuName = "Recipe/CookDataSO", order = 0)]
public class CookDataSO : ScriptableObject
{
    public int itemIndex;
    public CookData cookData;

    public Sprite cookSprite; //완성품 이미지

    public List<Sprite> partsSprites; //재료 이미지
}
