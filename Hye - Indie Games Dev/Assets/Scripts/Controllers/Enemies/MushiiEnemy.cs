using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushiiEnemy : Enemy
{
    protected override void InitialiseStateMachine()
    {
        Dictionary<Type, EnemyBaseState> states = new Dictionary<Type, EnemyBaseState>()
        {
            {typeof(EnemyFollowState), new EnemyFollowState(this)},
            {typeof(EnemyIdleState), new EnemyIdleState(this)},
            {typeof(EnemyAttackState), new MushiAttackState(this)},
            {typeof(MushiiHitState), new MushiiHitState(this)}
        };

        stateMachine.SetStates(states, typeof(EnemyFollowState));
    }

    protected override void Update()
    {
        base.Update();
        an.SetBool("isGrounded", isGrounded);
    }

    public IEnumerator ChangeToState(Type type, float delay)
    {
        yield return new WaitForSeconds(delay);

        stateMachine.SwitchState(type);
    }


    public override void ApplyStatus(float statusAmount, StatusType statusRecieved)
    { base.ApplyStatus(statusAmount, statusRecieved); }

    public override void TakeDamage(float damageAmount, StatusType damageType, Vector3 damagePos)
    {
        base.TakeDamage(damageAmount, damageType, damagePos);

        if (stateMachine.currentState is EnemyFollowState)
        {
            stateMachine.SwitchState(typeof(MushiiHitState));
        }
    }
}
