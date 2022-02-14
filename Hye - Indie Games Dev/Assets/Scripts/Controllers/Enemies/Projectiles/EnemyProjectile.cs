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
        player = GameObject.FindObjectOfType<PlayerController>();
        rb = GetComponent<Rigidbody>();
    }

    public virtual void ProjectileSetup(Vector3 target)
    {
        this.target = target;
    }

    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out PlayerController player))
        {
            player.playerStats.TakeDamage(damage, StatusType.none);
            player.rb.AddForce(knockback * rb.velocity.normalized, ForceMode.VelocityChange);
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
