using UnityEngine;
using UnityEngine.UI;

public class CookingToolUI : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] Image patienceBar;

    private void Start()
    {
        mainCamera = Camera.main;
        transform.rotation = mainCamera.transform.rotation;
    }
    public void ShowPatience(float fillAmount)
    {
        patienceBar.fillAmount = fillAmount;
    }

    public void BarTurnGreen()
    {
        patienceBar.color = Color.green;
    }

    public void BarTurnRed()
    {
        patienceBar.color = Color.red;
    }
}
