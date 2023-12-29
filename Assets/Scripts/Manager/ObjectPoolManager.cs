using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public GameObject prefabs;

    public int initialPoolSize;
    public int maxPoolSize;

    private List<GameObject> objectPool1;

    void Start()
    {
        //Enemy A
        objectPool1 = new List<GameObject>();
        for (int i = 0; i < initialPoolSize; i++)
        {
            GameObject obj = Instantiate(prefabs, transform);
            obj.SetActive(false);
            objectPool1.Add(obj);
        }
    }

    public GameObject GetObjectFromPool()
    {
        foreach (GameObject obj in objectPool1)
        {
            if (obj.activeInHierarchy)
            {
                obj.SetActive(true);
                return obj;
            }
        }
        if (objectPool1.Count < maxPoolSize)
        {
            GameObject obj = Instantiate(prefabs, transform);
            objectPool1.Add(obj);
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return null;
        }
    }

    public void ReturnObjectToPool()
    {
        GameObject obj = Instantiate(prefabs, transform);
        obj.SetActive(false);
    }
}
