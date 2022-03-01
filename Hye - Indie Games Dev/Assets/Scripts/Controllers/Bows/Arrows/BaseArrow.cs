using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseArrow : MonoBehaviour
{
    [Header("Arrow Stats")]
    public float arrowForce;
    public float damage;
    public StatusType statusType;
    public float energyCost;
    public LayerMask hitMask;

    public GameObject hitParticlePrefab;

    [Header("Status Settings")]
    public float statusStrength;
    public StatusSpawnable statusSpawnPrefab;

    protected float arrowDepth = .1f;
    protected bool canHit = false;

    protected Rigidbody rb;

    protected Vector3 lastPos;
    protected Quaternion lastRot;

    protected bool isStuck;
    protected Transform stuckParent;
    protected Vector3 stuckDifPos;
    protected Quaternion stuckDifRot;

    protected FixedJoint fixedJoint;
    protected RaycastHit hitObject;

    protected float chargePercent;
    bool hasHit;

    public void Init(float percent, Vector3 direction)
    {
        transform.forward = direction.normalized;

        chargePercent = percent;

        arrowForce *= percent;
        damage *= percent;
        statusStrength *= percent;

        isStuck = false;
        transform.localScale = Vector3.one;

        if (fixedJoint != null)
        {
            Destroy(fixedJoint);
        }

        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }

        rb.AddForce(transform.forward * arrowForce, ForceMode.Impulse);
    }

    protected virtual void Update()
    {
        if (!isStuck)
        {
            //Points in direction of rotation
            transform.forward = Vector3.Slerp(transform.forward, rb.velocity.normalized, Time.deltaTime * 4f);
        }

        if (fixedJoint != null)
        {
            if (fixedJoint.connectedBody == null) { Destroy(gameObject); }
        }

        Vector3 velDir = rb.velocity * Time.deltaTime;

        hasHit = Physics.Raycast(lastPos, velDir, out hitObject, (lastPos - transform.position).magnitude, hitMask);

        if (!(lastPos != Vector3.zero && !isStuck && hasHit)) return;

        ArrowStick();
       transform.position = hitObject.point + transform.forward*arrowDepth*chargePercent;
        ApplyActions(hitObject.collider.gameObject);

    }

    void ArrowStick()
    {
        Rigidbody hitRb = hitObject.collider.gameObject.GetComponentInParent<Rigidbody>();

        if (hitRb != null)
        {
            Instantiate(hitParticlePrefab, transform.position - transform.forward*2, Quaternion.identity);
            //Destroy(gameObject);
            /*             fixedJoint = gameObject.AddComponent<FixedJoint>();
                        fixedJoint.connectedBody = hitRb;
                        rb.mass = 0;
                        //Knockback
                        hitRb.AddForce(rb.velocity.normalized * arrowForce / 2, ForceMode.Impulse); */
        }
        else if (hitObject.collider.gameObject.TryGetComponent(out Rigidbody otherRb))
        {
            Instantiate(hitParticlePrefab, transform.position - transform.forward*2, Quaternion.identity);
            //Destroy(gameObject);
            /*             fixedJoint = gameObject.AddComponent<FixedJoint>();
                        fixedJoint.connectedBody = otherRb; */
        }
        else
        {
            rb.isKinematic = true;
        }
        rb.velocity = Vector3.zero;
        isStuck = true;
    }

    private void FixedUpdate()
    {
        //On hit
        if (!(lastPos != Vector3.zero && !isStuck && hasHit)) return;
    }

    protected virtual void LateUpdate()
    {
        if (!isStuck)
        {
            lastPos = transform.position;
            lastRot = transform.rotation;
        }
    }

    private void ApplyActions(GameObject other)
    {

        if (other.TryGetComponent(out StatusEnvironment otherStatEnv))
        {
            if (otherStatEnv.statusType == statusType)
            {
                spawnStatusEffect();
            }
        }
        if (other.TryGetComponent(out RepelBubble rBubble)) { return; }
        if (other.TryGetComponent(out IStatusable otherStatus))
        {
            otherStatus.ApplyStatus(statusStrength, statusType);
        }
        else
        {
            IStatusable tempStat = other.GetComponentInParent<IStatusable>();
            if (tempStat != null)
            {
                tempStat.ApplyStatus(statusStrength, statusType);
            }
        }

        if (other.TryGetComponent(out IDamagable otherDamagable))
        {
            otherDamagable.TakeDamage(damage, statusType, transform.position - transform.forward);
        }
        else
        {
            IDamagable tempDmg = other.GetComponentInParent<IDamagable>();
            if (tempDmg != null)
            {
                tempDmg.TakeDamage(damage, statusType, transform.position - transform.forward);
            }
        }

    }

    void spawnStatusEffect()
    {
        StatusSpawnable temp = Instantiate(statusSpawnPrefab, hitObject.point, Quaternion.identity);
        temp.SetupSpawnable(gameObject, hitObject);
    }
}
