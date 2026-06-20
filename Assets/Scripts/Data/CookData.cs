using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class CookData
{
    public string cookName;
    public List<int> itemIndexs; //해당 음식을 만들기 위해 필요한 재료 인덱스
    public float cookingTime; //조리 시간
    public CookType type; //조리 방법
    public GameObject result; //조리 결과물
}