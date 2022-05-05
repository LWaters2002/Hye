using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackHitbox : MonoBehaviour
{
    public float damage;
    void OnTriggerEnter(Collider other)
    {
        PlayerController p = other.GetComponent<PlayerController>();
        if (p == null)
        {
            p = other.GetComponentInParent<PlayerController>();
        }
        if (p != null)
        {
            p.playerStats.TakeDamage(damage, StatusType.none, transform.position);
        }
    }
}
