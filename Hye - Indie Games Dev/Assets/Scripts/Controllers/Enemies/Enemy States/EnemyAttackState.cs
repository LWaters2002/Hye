
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAttackState : EnemyBaseState
{
    public EnemyAttackState(Enemy enemy) : base(enemy) { }

    private System.Type stateToSwitch;

    public override void OnSwitch()
    {
        stateToSwitch = null;
        enemy.activeWeapon.OnFinished += SwitchState;
        enemy.activeWeapon.Attack();
    }

    public override System.Type Tick()
    {
        return stateToSwitch;
    }

    private void SwitchState()
    {
        Debug.Log("State switched");
        enemy.AddTimeToAttack(enemy.activeWeapon.recoveryTime);
        enemy.AddExhaust(enemy.activeWeapon.exhaustAmount);

        enemy.activeWeapon = null;
        stateToSwitch = typeof(EnemyFollowState);

    }

    public override void FixedTick()
    {
    }

    public override void OnCollisionEnter(Collision collision) { }
}
