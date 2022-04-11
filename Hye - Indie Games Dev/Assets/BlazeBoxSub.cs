using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlazeBoxSub : MonoBehaviour
{
    public float damage;

    void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.gameObject.GetComponent<IDamagable>();
        Debug.Log(other.gameObject);

        if (damagable != null)
        {
            damagable.TakeDamage(damage, StatusType.fire, transform.position);
        }
    }
}
