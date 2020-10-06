using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugX : MonoBehaviour
{
    public static void Log(object msg)
    {
#if UNITY_EDITOR
        Debug.Log(msg);
#endif
    }

    public static void Log(string methodName, object msg)
    {
        Debug.Log("Log MeghtodName : " + methodName);
        Debug.Log(msg);
        Debug.Log("----------------------------------------------------");
    }
}
