using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StatusSpawnable : MonoBehaviour
{

    public float lifetime;
    protected float currentLifetime = 0;

    public abstract void SetupSpawnable(GameObject spawner, RaycastHit hitInfo);
    
    protected virtual void Update()
    {
        currentLifetime += Time.deltaTime;
        if (currentLifetime > lifetime)
        {
            Destroy(gameObject);
            currentLifetime = -1000;
        }
    }
}
