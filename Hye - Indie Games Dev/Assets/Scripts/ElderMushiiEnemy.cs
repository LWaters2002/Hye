using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElderMushiiEnemy : Enemy
{
    protected override void InitialiseStateMachine()
    {
        Dictionary<Type, EnemyBaseState> states = new Dictionary<Type, EnemyBaseState>()
        {
            {typeof(EnemyFollowState), new EnemyFollowState(this)},
            {typeof(EnemyIdleState), new EnemyIdleState(this)},
            {typeof(EnemyAttackState), new MushiAttackState(this)}
        };

        stateMachine.SetStates(states, typeof(EnemyFollowState));
    }

    public override void ApplyStatus(GameObject obj, float statusAmount, StatusType statusRecieved)
    { base.ApplyStatus( obj, statusAmount, statusRecieved); }

    public override void TakeDamage(float damageAmount, StatusType damageType, Vector3 damageSource)
    {
        base.TakeDamage(damageAmount, damageType, damageSource);
    }

    protected override void Death()
    {
        health = 1000;
        //stateMachine.SwitchState(typeof(EnemyIdleState));
        weapons.Clear();
        an.Play("ElderMushiiPhase2Transition",-1,0f);
    }
}
