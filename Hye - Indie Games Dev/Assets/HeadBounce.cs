using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBounce : MonoBehaviour
{
    [SerializeField]
    float bounceForce;
    [SerializeField]
    float damageDealt;

    Enemy e;

    void Start()
    {
        e = GetComponent<Enemy>();
    }

    void OnTriggerEnter(Collider other)
    {
        PlayerController p = other.GetComponentInParent<PlayerController>();

        if (p != null)
        {
            p.rb.AddForce(Vector3.up * bounceForce, ForceMode.VelocityChange);

            if (e != null)
            {
                e.TakeDamage(damageDealt, StatusType.none, e.transform.position + Vector3.up * 2f);
            }
        }
    }
}
