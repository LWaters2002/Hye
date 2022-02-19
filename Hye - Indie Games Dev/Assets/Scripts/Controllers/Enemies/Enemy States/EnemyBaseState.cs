using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EnemyBaseState : BaseState
{
    protected Enemy enemy;
    protected Rigidbody rb; 

    public EnemyBaseState(Enemy enemy) : base(enemy.gameObject)
    {
        this.enemy = enemy;
        rb = this.enemy.rb;
    }
    
    public abstract void FixedTick();
    public abstract void OnCollisionEnter(Collision collision);

}
