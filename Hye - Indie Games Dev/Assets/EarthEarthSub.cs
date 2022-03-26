using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthEarthSub : MonoBehaviour
{
    public Rigidbody rb { get; private set; }
    public LayerMask mask;

    float distance = .1f;

    public void Init(Vector3 dir, float force) 
    {
        rb = GetComponent<Rigidbody>();
        GetComponentInChildren<CylinderCollider>().overlapEvent += ColliderOverlap; //Sets up overlap
        
        rb.AddForce(dir * force, ForceMode.VelocityChange);
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position,Vector3.down*distance, mask))
        {
            rb.AddForce(Vector3.up*5f, ForceMode.VelocityChange);
        }
    }

    void ColliderOverlap(Collider col)
    {
        rb.AddForce(Vector3.up * 5f, ForceMode.VelocityChange);
    }



}
