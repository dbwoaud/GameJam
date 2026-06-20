using UnityEngine;

public class BaseUI : MonoBehaviour
{
    public bool isStackable = true;
    public virtual void Show() { }

    public virtual void Hide()
    {
        UIManager.Instance.PopStack();
        gameObject.SetActive(false);
    }
}
