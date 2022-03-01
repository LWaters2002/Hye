using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class riseUp : MonoBehaviour
{
    public float gravityScale;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddForce(Vector3.up*gravityScale*9.81f, ForceMode.Acceleration);
    }

}
