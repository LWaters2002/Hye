using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyProjectile : MonoBehaviour
{

    [Header("Stats")]
    public float speed;
    public float damage;
    public float knockback;

    protected PlayerController player;
    protected Rigidbody rb;
    Vector3 playerDir;
    protected Vector3 target;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Empty
    }

    public virtual void ProjectileSetup(Vector3 target)
    {
        player = GameObject.FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody>();
        this.target = target;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        PlayerController pc = other.gameObject.GetComponentInParent<PlayerController>();
        Debug.Log(other.gameObject.name);
        
        if (pc != null)
        {
            pc.playerStats.TakeDamage(damage, StatusType.none, transform.position);
            pc.rb.AddForce(knockback * rb.velocity.normalized, ForceMode.VelocityChange);
            Destroy(gameObject);
        }

        IDamagable d = other.gameObject.GetComponentInParent<IDamagable>();

        if (d != null)
        {
            if (d is EnemyHitbox) {return;}
            d.TakeDamage(damage,StatusType.none, transform.position);
            Destroy(gameObject);
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("Ground")) { Destroy(gameObject); }
    }

    protected virtual void LaunchAtPosition(Vector3 pos)
    {
        Vector3 dir = (-transform.position + pos).normalized;
        rb.AddForce(dir * speed, ForceMode.VelocityChange);
    }

    protected virtual bool MoveToPoint(Vector3 point, float moveStrength, bool stopAtPoint)
    {
        Vector3 pointDir = (point - transform.position).normalized;
        rb.AddForce(pointDir * moveStrength, ForceMode.Acceleration);

        Debug.DrawLine(transform.position, transform.position + pointDir, Color.green);
        if (Vector3.Distance(point, transform.position) < .3f)
        {
            if (stopAtPoint) { rb.velocity = Vector3.zero; }
            return true;
        }
        return false;
    }

    protected virtual bool LerpToPoint(Vector3 point, float moveStrength, bool stopAtPoint)
    {

        transform.position = Vector3.Lerp(transform.position, point, moveStrength * Time.deltaTime);

        if (Vector3.Distance(point, transform.position) < .3f)
        {
            if (stopAtPoint) { rb.velocity = Vector3.zero; }
            return true;
        }
        return false;
    }
}
