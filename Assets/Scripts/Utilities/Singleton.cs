using UnityEngine;
using UnityEngine.Rendering;

[DefaultExecutionOrder(-1)]
public abstract class Singleton<T> : MonoBehaviour where T : Component
{
    protected static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
                instance = FindAnyObjectByType<T>();

            if(instance == null)
            {
                GameObject obj = new GameObject($"{typeof(T).Name}.");
                instance = obj.AddComponent<T>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (!instance)
        {
            instance = this as T;
            DontDestroyOnLoad(instance);
            return;
        }

        Destroy(gameObject);
    }
}
