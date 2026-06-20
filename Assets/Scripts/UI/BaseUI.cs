using UnityEngine;

public class BaseUI : MonoBehaviour
{
    public virtual void Show() { }

    public virtual void Hide()
    {
        UIManager.Instance.PopStack();
        gameObject.SetActive(false);
    }
}
