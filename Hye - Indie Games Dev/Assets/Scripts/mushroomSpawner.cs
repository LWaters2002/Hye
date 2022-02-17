using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mushroomSpawner : MonoBehaviour
{
    public GameObject mush;
    public float mushroomInterval;
    float distanceCount;

    bool canSpawn;
    RaycastHit lastHit;

    void Start()
    {
        Invoke("CanSpawnTrue", 2f);
    }

    private void Update()
    {
        RaycastHit hit;


        Physics.Raycast(transform.position, Vector3.down * 5f, out hit);

        if (lastHit.point != null)
        {
            distanceCount += (lastHit.point - hit.point).magnitude;
        }

        if (distanceCount > mushroomInterval && canSpawn)
        {
            SpawnMushroom();
            distanceCount -= mushroomInterval;
        }

        lastHit = hit;
    }

    void CanSpawnTrue() { canSpawn = true; distanceCount = 0;}

    void SpawnMushroom()
    {
        Instantiate(mush, transform.position, Quaternion.identity);
    }


}
