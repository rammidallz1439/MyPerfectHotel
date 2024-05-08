using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MonoHelper : MonoBehaviour
{
    public static MonoHelper instance;

    private void Awake()
    {
      instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public GameObject InstantiateObject(GameObject obj,Transform parent = null)
    {
      
        return Instantiate(obj,parent);
    }
    public GameObject InstantiateObject(GameObject obj, Vector3 transform,Quaternion quat)
    {

        return Instantiate(obj, transform,quat);
    }
    public Coroutine RunCouroutine(IEnumerator enumerator)
    {
        return StartCoroutine(enumerator);
    }
    public void KillCoroutine(IEnumerator enumerator)
    {
      StopCoroutine(enumerator);
    }

    public  T GetObjectOfType<T>() where T : Object
    {
       return FindObjectOfType<T>();
    }
    public void DestroyObject(GameObject obj)
    {

        Destroy(obj);
    }
    public void DestroyObjectImmediate(GameObject obj)
    {
        DestroyImmediate(obj);
    }
    public void PrintLog(object obj)
    {
        Debug.Log($"<color=red>{obj}</color>");
    }
    public void PrintError(object msg)
    {
        Debug.LogError(msg);
    }
    public void PrintWarning(object msg)
    {
        Debug.LogWarning(msg);
    }
}
