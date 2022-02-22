using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour, IStatusable
{

    public ParticleSystem ps;

    public float damage;
    public float explosionRadius;
    public float bombLifetime;
    float bombLife;

    public float maxVelocity;

    public float respawnTime;
    float respawnTimer;

    bool isHanging = true;
    bool hasBlown = false;

    MeshRenderer mr;
    Vector3 startPos;
    Quaternion startRot;
    Transform parentTransform;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        bombLife = bombLifetime;
        respawnTimer = respawnTime;
        parentTransform = transform.parent;
        startPos = transform.position;
        startRot = transform.rotation;
    }

    private void Update()
    {
        if (!isHanging && !hasBlown)
        {
            bombLife -= Time.deltaTime;
            if (bombLife < 0) { Blow(); bombLife = bombLifetime; }
        }

        if (hasBlown)
        {
            respawnTimer -= Time.deltaTime;

            if (respawnTimer < 0)
            {
                hasBlown = false;
                isHanging = true;
                transform.parent = parentTransform;
                transform.position = startPos;
                transform.rotation = startRot;
                mr.enabled = true;
                rb.isKinematic = true;
                respawnTimer = respawnTime;
            }
        }
    }

    public void ApplyStatus(float statusAmount, StatusType statusRecieved)
    {
        transform.parent = null;
        rb.isKinematic = false;
        isHanging = false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (rb.velocity.magnitude > maxVelocity)
        {
            if ((Vector3.Dot((other.gameObject.transform.position - transform.position).normalized, rb.velocity.normalized)) > .2)
            {
                Blow();
            }
        }
    }
    private void Blow()
    {
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, explosionRadius);
        List<GameObject> rootHitboxes = new List<GameObject>();
        foreach (Collider obj in hitObjects)
        {
            if (obj.gameObject.TryGetComponent(out EnemyHitbox eb))
            {
                GameObject rootObj = eb.gameObject.transform.root.gameObject;
                if (!rootHitboxes.Contains(rootObj))
                {
                    rootHitboxes.Add(rootObj);
                }
                else { continue; }
            }

            if (obj.gameObject.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(damage, StatusType.none, obj.ClosestPoint(transform.position));
            }
            else
            {
                damagable = obj.GetComponentInParent<IDamagable>();
                if (damagable != null)
                {
                    damagable.TakeDamage(damage, StatusType.none, obj.ClosestPoint(transform.position));
                }
            }

            if (obj.gameObject.TryGetComponent(out IDestructable destructable))
            {
                destructable.TryDestroy(gameObject);
            }
        }

        mr.enabled = false;
        hasBlown = true;
        ps.Play();
    }


}
