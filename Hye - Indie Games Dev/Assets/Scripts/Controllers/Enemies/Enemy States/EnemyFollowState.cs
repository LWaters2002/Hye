using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowState : EnemyBaseState
{
    public EnemyFollowState(Enemy enemy) : base(enemy) { }

    public override void OnSwitch()
    {

    }

    public override Type Tick()
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

        if (Mathf.Abs(enemy.distanceToPlayer) > enemy.followRange) { return; }

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
