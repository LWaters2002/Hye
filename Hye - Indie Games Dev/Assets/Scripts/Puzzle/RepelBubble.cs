using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepelBubble : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out BaseArrow arrow)) 
        {
            Rigidbody arrowRb = arrow.gameObject.GetComponent<Rigidbody>();

            Vector3 dir = (transform.position - arrowRb.gameObject.transform.position).normalized;
            arrowRb.velocity = Vector3.zero;
            arrowRb.AddForce(-dir*arrow.arrowForce*.2f,ForceMode.VelocityChange);
        }
    }
}
