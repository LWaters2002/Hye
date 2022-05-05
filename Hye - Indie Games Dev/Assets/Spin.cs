using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotatationSpeed;

    float angle = 0;
    // Update is called once per frame
    void Update()
    {
        angle += rotatationSpeed*Time.deltaTime;
        transform.RotateAround(transform.position, transform.right, angle);
    }
}
