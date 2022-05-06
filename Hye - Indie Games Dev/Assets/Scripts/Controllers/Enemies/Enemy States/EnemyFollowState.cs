using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowState : EnemyBaseState
{
    Vector3 pDir;
    Vector3 surroundOffset;
    int combatDirection = -1;
    float combatDistanceTimer;

    public EnemyFollowState(Enemy enemy) : base(enemy) { }

    public override void OnSwitch()
    {
        pDir = (enemy.player.transform.position - enemy.transform.position).normalized;
    }

    public override System.Type Tick()
    {
        if (Mathf.Abs(enemy.distanceToPlayer) > enemy.followRange) { return typeof(EnemyIdleState); }
        if (enemy.activeWeapon == null) return null;
        return typeof(EnemyAttackState);
    }

    public override void FixedTick()
    {
        Vector3 dir = enemy.enemyInfluence;

        if (rb.velocity != Vector3.zero)
        {   //Makes enemy point towards where it's moving to.
            rb.MoveRotation(Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(rb.velocity.normalized, Vector3.up), Time.deltaTime * 8f));
        }

        //if (Mathf.Abs(enemy.distanceToPlayer) > enemy.followRange) { return; }

        if (Mathf.Abs(enemy.distanceToPlayer) < enemy.combatDistance) // Creates a *semi-random* wander when within combatDistance.
        {
            combatDistanceTimer -= Time.deltaTime;
            Vector3 playerDir = (enemy.player.transform.position - enemy.transform.position).normalized;

            if (combatDistanceTimer < 0)
            {
                combatDistanceTimer = Random.Range(.3f, 1.4f);

                surroundOffset = playerDir * .2f * Random.Range(-.3f, .3f);
                combatDirection = ((Random.Range(0, 2) * 2) - 1);
            }

            NavMeshHit hit;
            pDir = Vector3.Cross(Vector3.up, playerDir) * combatDirection + surroundOffset;

            if (!NavMesh.SamplePosition(enemy.transform.position + pDir * Time.fixedDeltaTime * enemy.speed, out hit, 10f, NavMesh.AllAreas))
            {
                surroundOffset = playerDir * .2f * Random.Range(-.3f, .3f);
                combatDirection = ((Random.Range(0, 2) * 2) - 1);
                return;
            }

            rb.AddForce(enemy.speed * .9f * pDir.normalized, ForceMode.Acceleration);
            return;
        }
        else { pDir = Vector3.zero; }


        if (!enemy.isGrounded) { return; }
        NavMesh.CalculatePath(transform.position, enemy.player.transform.position, NavMesh.AllAreas, enemy.path);

        if (enemy.path.corners.Length > 1)
        {
            Vector3 moveDir = (enemy.path.corners[1] - transform.position).normalized;
            dir += moveDir;
        }
        else
        {
            Vector3 moveDir = (-transform.position + enemy.player.transform.position).normalized;
            dir += moveDir;
        }

        rb.AddForce(enemy.speed * dir.normalized, ForceMode.Acceleration);
    }

    public override void OnCollisionEnter(Collision collision) { }
}
