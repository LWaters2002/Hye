using System;
using UnityEngine;

public class MushiiHitState : EnemyBaseState
{
    public MushiiHitState(Enemy enemy) : base(enemy) { }

    public override void OnSwitch()
    {
        enemy.an.Play("MushiiHit", -1, 0f);
        
        MushiiEnemy m = enemy.GetComponent<MushiiEnemy>();
        m.StartCoroutine(m.ChangeToState(typeof(EnemyFollowState), .3f));
    }

    public override Type Tick()
    {
        return null;
    }

    public override void FixedTick()
    {

    }

    public override void OnCollisionEnter(Collision collision) { }
}
