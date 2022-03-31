using UnityEngine;
using UnityEngine.AI;

public class EnemyIdleState : EnemyBaseState
{
    float dirCooldown = 2f;
    float dirTimer = 2f;

    float randSpeed;
    Vector3 randDir;
    Vector3 moveDir;

    public override void OnSwitch()
    {
        randSpeed = enemy.speed*.8f;
    }

    public EnemyIdleState(Enemy enemy) : base(enemy) { }

    public override System.Type Tick()
    {
        dirTimer += Time.deltaTime;

        if (Mathf.Abs(Vector3.Distance(enemy.player.transform.position, enemy.transform.position)) < enemy.senseRange) { return typeof(EnemyFollowState); }
        return null;
    }

    public override void FixedTick()
    {
        if (!enemy.isGrounded) { return; }

        if (rb.velocity != Vector3.zero)
        {   //Makes enemy point towards where it's moving to.
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(rb.velocity.normalized, Vector3.up), Time.deltaTime * 8f));
        }

        NavMesh.CalculatePath(transform.position, transform.position + randDir, NavMesh.AllAreas, enemy.path);

        if (enemy.path.corners.Length > 1)
        {
            moveDir = (enemy.path.corners[1] - transform.position);
        }
        else
        {
            randDir = Random.insideUnitSphere * 5f; //2f is wander radius
            randDir = new Vector3(randDir.x, 0, randDir.z);
            randSpeed = Random.Range(enemy.speed*.6f,enemy.speed*.9f); // Random Speed when wandering giving a more organic feel.
        }

        rb.AddForce(randSpeed * moveDir.normalized, ForceMode.Acceleration);
    }

    public override void OnCollisionEnter(Collision collision) { }
}
