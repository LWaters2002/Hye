using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{

    Vector3 startPos;
    public float displaceAmount;
    public float displaceSpeed; 
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.Sin(Time.time*displaceSpeed)*displaceAmount;
        transform.position += Vector3.up*y*Time.deltaTime;
    }
}
