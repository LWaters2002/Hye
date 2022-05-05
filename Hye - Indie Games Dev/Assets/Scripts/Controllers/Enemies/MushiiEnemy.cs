using System.Timers;
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
            {typeof(EnemyIdleState), new EnemyIdleState(this)},
            {typeof(EnemyFollowState), new EnemyFollowState(this)},
            {typeof(EnemyAttackState), new MushiAttackState(this)},
            {typeof(MushiiHitState), new MushiiHitState(this)},
            {typeof(EnemyEmptyState), new EnemyEmptyState(this)}
        };

        stateMachine.SetStates(states, typeof(EnemyIdleState));
    }

    protected override void Update()
    {
        base.Update();
        an.SetBool("isGrounded", isGrounded);
        if (Vector3.Dot(transform.up, Vector3.up) < .95f)
        {
            transform.up = Vector3.Lerp(transform.up, Vector3.up, Time.deltaTime);
            if (Vector3.Dot(transform.up, Vector3.up) < .95f) { transform.up = Vector3.up; }
        }

    }


    public IEnumerator ChangeToState(Type type, float delay)
    {
        yield return new WaitForSeconds(delay);
        stateMachine.SwitchState(type);
    }


    public override void ApplyStatus(GameObject obj, float statusAmount, StatusType statusRecieved)
    { base.ApplyStatus(obj, statusAmount, statusRecieved); }

    public override void TakeDamage(float damageAmount, StatusType damageType, Vector3 damagePos)
    {
        base.TakeDamage(damageAmount, damageType, damagePos);

        if (stateMachine.currentState is EnemyFollowState)
        {
            stateMachine.SwitchState(typeof(MushiiHitState));
        }
    }

    protected override void Death()
    {
        an.Play(deathAnimationName);
        stateMachine.SwitchState(typeof(EnemyEmptyState));
        Invoke("DestroyMe", 1f);
    }

    void DestroyMe()
    {
        Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Hazard"))
        {
            TakeDamage(1000f, StatusType.none, Vector3.zero);
        }
    }
}
