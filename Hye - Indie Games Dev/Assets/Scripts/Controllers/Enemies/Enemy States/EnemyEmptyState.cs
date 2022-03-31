using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEmptyState : EnemyBaseState
{

    public override void OnSwitch()
    {
    }

    public EnemyEmptyState(Enemy enemy) : base(enemy) { }

    public override System.Type Tick()
    {

        return null;
    }

    public override void FixedTick()
    {
    }

    public override void OnCollisionEnter(Collision collision) { }
}
