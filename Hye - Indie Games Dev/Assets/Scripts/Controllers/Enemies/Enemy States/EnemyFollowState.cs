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
        NavMesh.CalculatePath(transform.position, enemy.player.transform.position, NavMesh.AllAreas, enemy.path);

        if (enemy.path.corners.Length > 1)
        {
            Vector3 moveDir = (enemy.path.corners[1] - transform.position).normalized;
            rb.AddForce(enemy.speed * moveDir, ForceMode.Acceleration);
        }
    }

    public override void OnCollisionEnter(Collision collision) { }
}
