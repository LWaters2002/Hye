using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinshiiSlashAttack : EnemyWeapon
{
    [Header("Attack Settings")]
    bool beingUsed;
    public ParticleSystem ps;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void SlashStart()
    {
        ps.Play();
    }
    public void SlashFinished()
    {
        Finished();
        ps.Stop();
    }
    public override void Attack()
    {
        base.Attack();
        attacking = false;
    }
}
