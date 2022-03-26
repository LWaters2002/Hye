using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthEarth : MonoBehaviour
{
    public int splitAmount;
    public float splitForce;
    public EarthEarthSub ePrefab;
    public float upwardForce;

    public void Init()
    {
        Burst();
    }

    void Burst()
    {
        for (int i = 0; i < splitAmount; i++)
        {
            Vector2 rand = Random.insideUnitCircle.normalized;
            Vector3 rand3 = new Vector3(rand.x, upwardForce, rand.y);

            EarthEarthSub ees = Instantiate(ePrefab, transform.position + new Vector3(rand3.x, 0, rand3.z), Quaternion.identity);
            ees.Init(rand3, 3f);
        }
    }
}
