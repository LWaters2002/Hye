using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericEnemy : Enemy
{
    protected override void InitialiseStateMachine()
    {
        Dictionary<Type, EnemyBaseState> states = new Dictionary<Type, EnemyBaseState>()
        {
            {typeof(EnemyFollowState), new EnemyFollowState(this)},
            {typeof(EnemyIdleState), new EnemyIdleState(this)},
            {typeof(EnemyAttackState), new EnemyAttackState(this)}
        };

        stateMachine.SetStates(states, typeof(EnemyFollowState));
    }

    public override void ApplyStatus(float statusAmount, StatusType statusRecieved)
    { base.ApplyStatus(statusAmount, statusRecieved); }

}
