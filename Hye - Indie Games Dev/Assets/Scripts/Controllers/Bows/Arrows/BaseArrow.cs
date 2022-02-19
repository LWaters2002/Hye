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

    bool hasHit;

    public void Setup(float percent)
    {
        arrowForce *= percent;
        damage *= percent;
        statusStrength *= percent;
    }

    protected virtual void OnEnable()
    {
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
            //Points in direciton of rotation
            transform.forward = Vector3.Slerp(transform.forward, rb.velocity.normalized, Time.deltaTime * 4f);
        }

        if (fixedJoint != null)
        {
            if (fixedJoint.connectedBody == null) { Destroy(gameObject); }
        }

        Vector3 velDir = rb.velocity * Time.deltaTime;

        hasHit = Physics.Raycast(lastPos, velDir, out hitObject, (lastPos-transform.position).magnitude, hitMask);
    //    Debug.DrawLine(lastPos,lastPos + velDir.normalized* (lastPos - transform.position).magnitude, Color.red,2f);

        if (!(lastPos != Vector3.zero && !isStuck && hasHit)) return;
        
        ArrowStick();
        transform.position = hitObject.point;
        ApplyActions(hitObject.collider.gameObject);

    }

    void ArrowStick()
    {
        Rigidbody hitRb = hitObject.collider.gameObject.GetComponentInParent<Rigidbody>();

        if (hitRb != null)
        {
            fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = hitRb;
            rb.mass = 0;
            //Knockback
            hitRb.AddForce(rb.velocity.normalized * arrowForce / 2, ForceMode.Impulse);
        }
        else if (hitObject.collider.gameObject.TryGetComponent(out Rigidbody otherRb))
        {
            fixedJoint = gameObject.AddComponent<FixedJoint>();
            fixedJoint.connectedBody = otherRb;
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
            otherDamagable.TakeDamage(damage, statusType);
            lutils.SpawnDamageNumber(transform.position, damage);
        }
        else
        {
            IDamagable tempDmg = other.GetComponentInParent<IDamagable>();
            if (tempDmg != null)
            {
                tempDmg.TakeDamage(damage, statusType);
                lutils.SpawnDamageNumber(transform.position, damage);
            }
        }

    }

    void spawnStatusEffect()
    {
        StatusSpawnable temp = Instantiate(statusSpawnPrefab, hitObject.point, Quaternion.identity);
        temp.SetupSpawnable(gameObject, hitObject);
    }
}
