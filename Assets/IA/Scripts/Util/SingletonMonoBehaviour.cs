using UnityEngine;
using System;
using System.Collections;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : SingletonMonoBehaviour<T>
{
    public static T Instance
    {
        get
        {
            T[] managers = GameObject.FindObjectsOfType(typeof(T)) as T[];
            if (managers != null)
            {
                if (managers.Length == 1)
                {
                    instance = managers[0];
                    return instance;
                }
                else if (managers.Length > 1)
                {
                    for (int i = 0; i < managers.Length; ++i)
                    {
                        T manager = managers[i];
                        Destroy(manager.gameObject);
                    }
                }
            }

            if (instance == null)
            {
                GameObject go = new GameObject(typeof(T).Name, typeof(T));
                instance = go.GetComponent<T>();
            }
            
            return instance;
        }
    }

    public static bool HasInstance
    {
        get { return instance != null; }
    }

    private static T instance = null;
    private bool initialized = false;

    protected virtual void OnInitialize()
    {
    }

    protected virtual void OnAwakeSingleton()
    {
    }

    protected virtual void OnDestroySingleton()
    {
        alive = false;
    }

    public static T Get()
    {
        return Instance;
    }

    void Initialize()
    {
        if (!initialized)
        {
            initialized = true;
            OnInitialize();
        }
    }

    void Awake()
    {
        Initialize();
        OnAwakeSingleton();
    }

    void OnDestroy()
    {
        if (instance == this)
        {
            OnDestroySingleton();
            instance = null;
        }
    }

    void OnApplicationQuit()
    {
        if (instance == this)
        {
            instance = null;
            OnDestroySingleton();
        }
    }

    private bool alive = true;

    public static bool IsAlive
    {
        get
        {
            if (instance == null)
                return false;
            return instance.alive;
        }
    }
}