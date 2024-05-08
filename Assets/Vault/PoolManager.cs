using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public static PoolManager Instance;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CreatePoolObjects();
    }
    public void CreatePoolObjects()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
                for (int i = 0; i < pool.poolSize; i++)
                {

                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }

                poolDictionary.Add(pool.tag, objectPool);
        

        }
    }
    public GameObject GetPooledObject(string tag)
    {
        GameObject obj;
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }
        else
        {
             obj = poolDictionary[tag].Dequeue();
        }
        if (obj != null)
        {
            
            obj.SetActive(true);
           
            return obj;
        }
        else
        {
            Debug.LogWarning("No object available in pool with tag " + tag);
            return null;
        }
       
    }

    public void ReturnToPool(string tag, GameObject obj)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return;
        }
        poolDictionary[tag].Enqueue(obj);
        obj.SetActive(false);
     
       
    }

}
[System.Serializable]
public class Pool
{
    public string tag;
    public GameObject prefab;
    public int poolSize;
}