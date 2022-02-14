using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroomSpawner : MonoBehaviour
{
    public GameObject mush;
    public float mushroomInterval;
    float distanceCount;

    RaycastHit lastHit;

    private void Update()
    {
        RaycastHit hit;


        Physics.Raycast(transform.position, Vector3.down * 5f, out hit);

        if (lastHit.point != null)
        {
            distanceCount += (lastHit.point - hit.point).magnitude;
        }

        if (distanceCount > mushroomInterval) 
        {
            SpawnMushroom();
            distanceCount -= mushroomInterval;
        }
    
        lastHit = hit;


    }
    void SpawnMushroom() 
    {
        Instantiate(mush, transform.position, Quaternion.identity);
    }

    
}
