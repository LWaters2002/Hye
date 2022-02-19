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
        if (enemy.activeWeapon == null) return null;

        return typeof(EnemyAttackState);
    }

    public override void FixedTick()
    {
        if (!enemy.isGrounded) { return; }
        NavMesh.CalculatePath(transform.position, enemy.player.transform.position, NavMesh.AllAreas, enemy.path);

        if (enemy.path.corners.Length > 1)
        {
            Vector3 moveDir = (enemy.path.corners[1] - transform.position).normalized;
            rb.AddForce(enemy.speed * moveDir, ForceMode.Acceleration);

            if (rb.velocity != Vector3.zero)
            {
                rb.MoveRotation(Quaternion.Slerp(rb.rotation, Quaternion.LookRotation(rb.velocity.normalized, Vector3.up), Time.deltaTime * 8f));
            }
        }
        else 
        {
            Vector3 moveDir = (-transform.position + enemy.player.transform.position).normalized;
            rb.AddForce(enemy.speed * moveDir, ForceMode.Acceleration);
        }
    }

    public override void OnCollisionEnter(Collision collision) { }
}
