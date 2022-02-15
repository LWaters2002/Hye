using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shrink : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.zero, Time.deltaTime);

        if (transform.localScale.magnitude < .1) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent( out PlayerController pc)) 
        {
        
        }
    }
}
