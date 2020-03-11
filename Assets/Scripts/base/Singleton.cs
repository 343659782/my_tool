using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    protected static T _instance = null;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("Singleton");
                DontDestroyOnLoad(go);
                _instance = go.AddComponent<T>();
            }
            return _instance;
        }
    }

    public void Init()
    {

    }
}
