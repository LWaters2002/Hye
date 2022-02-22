using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushiAttackState : EnemyAttackState
{
    public MushiAttackState(Enemy enemy) : base(enemy) { }

    private System.Type stateToSwitch;

    public override void OnSwitch()
    {
        stateToSwitch = null;
        enemy.activeWeapon.OnFinished += SwitchState;

        enemy.an.Play(enemy.activeWeapon.animationName,-1,0f);
    }

    public override System.Type Tick()
    {
        return stateToSwitch;
    }

    private void SwitchState()
    {
        enemy.activeWeapon = null;
        stateToSwitch = typeof(EnemyFollowState);
    }

    public override void FixedTick()
    {

    }

    public override void OnCollisionEnter(Collision collision) { }
}
