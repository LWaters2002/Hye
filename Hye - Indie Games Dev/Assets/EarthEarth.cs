using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthEarth : MonoBehaviour
{
    public int splitAmount;
    public EarthEarthSub ePrefab;

    public void Init() 
    {
        Burst();
    }

    void Burst() 
    {
        for (int i = 0; i < splitAmount; i++)
        {
            EarthEarthSub ees = Instantiate(ePrefab, transform.position, Quaternion.identity);
            ees.Init(Random.insideUnitSphere, 10f );
        }
    }
}
