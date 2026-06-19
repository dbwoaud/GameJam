using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected bool isDestroyable; //파괴 가능여부

    private static T m_instance;

    public static T Instance 
    { 
        get
        {
            if(m_instance == null)
            {
                
                if(m_instance == null)
                {
                    var g = new GameObject(typeof(T).Name);
                    m_instance = g.AddComponent<T>();
                }
            }            
            
            return m_instance;
        }
    }

    protected virtual void Awake()
    {
        if(m_instance == null)
        {
            m_instance = this as T;
        }
        else
        {
            Destroy(gameObject);
        }

        if(isDestroyable == false)
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
