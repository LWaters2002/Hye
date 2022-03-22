using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : Enemy
{
    public BossUI bossUI;
    public string bossName;

    protected override void Awake()
    {
        base.Awake();
        Instantiate(bossUI, Vector3.zero, Quaternion.identity).Setup(this, bossName);
    }


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



    public override void ApplyStatus(GameObject obj, float statusAmount, StatusType statusRecieved)
    { base.ApplyStatus(obj, statusAmount, statusRecieved); }

}
