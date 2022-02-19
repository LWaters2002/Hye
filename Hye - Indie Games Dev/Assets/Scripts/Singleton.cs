using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject instance = new GameObject("Singleton : " + typeof(T).Name);
                instance.AddComponent<T>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this.GetComponent<T>();
    }
}
