using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{

    private static GameObject singleton;
    private static T _instance;

    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.\n");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                            " - there should never be more than 1 singleton!" +
                            " Reopenning the scene might fix it.\n");
                        return _instance;
                    }

                    //if (_instance == null)
                    //{

                    //    singleton = new GameObject();
                    //    _instance = singleton.AddComponent<T>();
                    //    singleton.name = "(singleton) " + typeof(T).ToString();

                    //    DontDestroyOnLoad(singleton);
                    //}
                    //else
                    //{
                    //    //Debug.Log("[Singleton] Using instance already created: " +
                    //    //	_instance.gameObject.name + "\n");
                    //}
                }

                return _instance;
            }
        }
    }

    private static bool applicationIsQuitting = false;
    public virtual void OnDestroy()
    {
        applicationIsQuitting = true;
    }

    public virtual void Awake()
    {
        if(Instance != null && Instance.tag != gameObject.GetInstanceID().ToString())
        {
            //if (FindObjectsOfType(typeof(T)).Length > 1 && gameObject.GetInstanceID() != FindObjectsOfType(typeof(T))[0].GetInstanceID())
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        Instance.tag = gameObject.GetInstanceID().ToString();
    }


}
