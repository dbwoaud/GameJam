using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeInfoUI : MonoBehaviour
{
    [SerializeField] private Image cookImg;
    [SerializeField] private Image cookingImg;

    [SerializeField] private List<Image> partsImg;
    [SerializeField] private List<Image> partsBackgroundImg;


    public void Show(CookDataSO dataSO)
    {
        cookImg.sprite = dataSO.cookSprite;

        for(int i = 0; i < partsImg.Count; i++)
        {
            if(i < dataSO.partsSprites.Count)
            {
                partsImg[i].sprite = dataSO.partsSprites[i];
                // partsBackgroundImg[i].gameObject.SetActive(true);
                partsImg[i].gameObject.SetActive(true);
            }
            else
            {
                // partsBackgroundImg[i].gameObject.SetActive(false);
                partsImg[i].gameObject.SetActive(false);
            }
        }

        switch(dataSO.cookData.type)
        {
            case CookType.Fry :
                cookingImg.sprite = ResourceManager.Instance.Load<Sprite>("cook_pan");
                break;

            case CookType.Boil :
                cookingImg.sprite = ResourceManager.Instance.Load<Sprite>("cook_pot");
                break;
        }
    }
}
