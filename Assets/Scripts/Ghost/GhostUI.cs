using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GhostUI : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] TextMeshProUGUI foodString;
    [SerializeField] private Image foodImg;
    [SerializeField] Image patienceBar;
    [SerializeField] Image face;
    [SerializeField] GameObject waitingPanel;
    [SerializeField] GameObject leavingPanel;

    [SerializeField] Sprite happyFace;
    [SerializeField] Sprite angryFace;

    private void Start()
    {
        foodImg.gameObject.SetActive(false);
        mainCamera = Camera.main;
        transform.rotation = mainCamera.transform.rotation;
    }

    public void ShowFood(CookDataSO dataSO)
    {
        foodImg.gameObject.SetActive(true);
        waitingPanel.SetActive(true);
        leavingPanel.SetActive(false);
        // foodString.text = foodName;
        foodImg.sprite = dataSO.cookSprite;
        transform.rotation = mainCamera.transform.rotation;
    }

    public void ShowPatience(float fillAmount)
    {
        patienceBar.fillAmount = fillAmount;
    }

    public void ShowFace(bool isHappy)
    {
        waitingPanel.SetActive(false);
        leavingPanel.SetActive(true);
        foodImg.gameObject.SetActive(false);

        if (isHappy) face.sprite = happyFace;
        else face.sprite = angryFace;
    }
}
