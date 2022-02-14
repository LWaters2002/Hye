using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool :  MonoBehaviour
{
    Transform poolTransform;
    int poolSize;

    List<GameObject> poolList;
    GameObject poolObject;

    public Pool(GameObject _poolObject,Transform _poolTransform, int _poolSize) 
    {
        poolSize = _poolSize;
        poolTransform = _poolTransform;
        poolObject = _poolObject;

        GeneratePool();
    }

    private void GeneratePool() 
    {
        for (int i = 0; i < poolSize; i++) 
        {
            GameObject temp = Instantiate(poolObject, poolTransform);
            temp.SetActive(false);
            poolList.Add(temp);
        } 
    }

    public GameObject GetObjectFromPool(bool forceGet) 
    {
        foreach (GameObject obj in poolList) 
        {
            if (obj.activeSelf) 
            {
                return obj;
            }
        }
        if (forceGet) 
        {
            return poolList[0];
        }
        else { return null; }   
    }

}
