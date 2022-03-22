using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthEarthSub : MonoBehaviour
{
    public Rigidbody rb { get; private set; }
    float distance = .1f;

    public void Init(Vector3 dir, float force) 
    {
        rb = GetComponent<Rigidbody>();

        rb.AddForce(dir * force, ForceMode.VelocityChange);
    }

    private void Update()
    {
        //if (Physics.Raycast(transform.position,Vector3.down*distance, LayerMask.NameToLayer("Ground")))
        //{
        //    rb.isKinematic = true;
        //}
    }



}
