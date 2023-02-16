using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class SingletonBase<T> : MonoBehaviour where T : SingletonBase<T>
{
    private static readonly Lazy<T> _instance = new Lazy<T>(() =>
    {
        T instance = FindObjectOfType(typeof(T)) as T;

        if (instance == null)
        {
            GameObject obj = new GameObject();
            instance = obj.AddComponent(typeof(T)) as T;
            DontDestroyOnLoad(obj);
        }
        else
        {
            DontDestroyOnLoad(instance.gameObject);
        }

        return instance;
    });

    public static T Instance
    {
        get
        {
            return _instance.Value;
        }
    }

    protected void Awake()
    {
        Instance.CreateInstance();
    }

    public virtual void CreateInstance() { }
}
