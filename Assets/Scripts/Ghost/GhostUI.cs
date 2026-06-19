using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GhostUI : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] TextMeshProUGUI foodString;
    [SerializeField] Image patienceBar;
    [SerializeField] Image face;
    [SerializeField] GameObject waitingPanel;
    [SerializeField] GameObject leavingPanel;

    [SerializeField] Sprite happyFace;
    [SerializeField] Sprite angryFace;

    private void Start()
    {
        mainCamera = Camera.main;
        transform.rotation = mainCamera.transform.rotation;
    }

    public void ShowFood(string foodName)
    {
        waitingPanel.SetActive(true);
        leavingPanel.SetActive(false);
        foodString.text = foodName;
    }

    public void ShowPatience(float fillAmount)
    {
        patienceBar.fillAmount = fillAmount;
    }

    public void ShowFace(bool isHappy)
    {
        waitingPanel.SetActive(false);
        leavingPanel.SetActive(true);

        if (isHappy) face.sprite = happyFace;
        else face.sprite = angryFace;
    }
}
