using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyBaseState
{
    public override void OnSwitch()
    {
        
    }

    public EnemyIdleState(Enemy enemy) : base(enemy) { }

    public override Type Tick()
    {
        return null;
    }
    public override void FixedTick() { }
    public override void OnCollisionEnter(Collision collision) { }
}
