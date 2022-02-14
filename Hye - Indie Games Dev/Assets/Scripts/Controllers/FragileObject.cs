using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileObject : MonoBehaviour
{

    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb.isKinematic = true;
    }

    public void BreakLink() 
    {
        rb.isKinematic = false;
    
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out EarthPillar ep)) 
        {
            rb.isKinematic = false;
        }
    }
}
