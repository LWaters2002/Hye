using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthEarthSub : MonoBehaviour, IDamagable
{
    public Rigidbody rb { get; private set; }
    public float maxHealth; 
    public float health { get; private set; }

    ParticleSystem ps;

    Animator an;
    public LayerMask mask;

    bool hasLanded = false;

    float distance = .65f;

    public void Init(Vector3 dir, float force) 
    {
        rb = GetComponent<Rigidbody>();
        ps = GetComponentInChildren<ParticleSystem>();
        an = GetComponent<Animator>();
        GetComponentInChildren<CylinderCollider>().overlapEvent += ColliderOverlap; //Sets up overlap

        health = maxHealth;
        
        rb.AddForce(dir * force, ForceMode.VelocityChange);
    }

    private void Update()   
    {
        if (hasLanded) { return; } 
        //Checks if touching floor.
        if (Physics.Raycast(transform.position,Vector3.down, distance,mask))
        {
            rb.isKinematic = true;
            ps.Play();
            an.SetBool("HasLanded", true);
            hasLanded = true;
        }
   
    }
    public void TakeDamage(float damageAmount, StatusType damageType, Vector3 damagePos) 
    {
        health -= damageAmount;
        if (health < 0) 
        {
            Destroy(gameObject);
        }
        lutils.SpawnDamageNumber(damagePos, damageAmount);
    }

    void ColliderOverlap(Collider col)
    {
        Enemy e = GetComponentInParent<Enemy>();
        e.TakeDamage(10,StatusType.earth, transform.position);
    }



}
