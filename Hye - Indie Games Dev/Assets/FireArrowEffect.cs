using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireArrowEffect : MonoBehaviour
{
    public float distancePer;
    public Blazebox blazeBox;

    Rigidbody rb;
    Vector3 lastPos;

    float distanceTravelled = 0;
    bool cycloneStarted;

    public UnityAction startCyclone;
    void start()
    {
        rb = GetComponent<Rigidbody>();
        lastPos = transform.position;
    }

    void Update()
    {
        distanceTravelled += Mathf.Abs(Vector3.Distance(transform.position, lastPos));
        if (distanceTravelled > distancePer)
        {
            distanceTravelled -= distancePer;
            SpawnBlazeBox();
        }
        lastPos = transform.position;
    }

    void SpawnBlazeBox()
    {
        Blazebox bb = Instantiate(blazeBox, transform.position, transform.rotation);
        bb.Init(this);
        bb.triggered += () => { if (!cycloneStarted) { cycloneStarted = true; startCyclone.Invoke(); } };
    }



}
