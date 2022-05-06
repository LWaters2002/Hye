using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinshiiSlashAttack : EnemyWeapon
{
    [Header("Attack Settings")]
    bool beingUsed;
    public ParticleSystem ps;
    public GameObject requiredObject;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override bool CheckConditions()
    {
        bool result = base.CheckConditions();
        if (requiredObject == null) { result = false; }
        return result;
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
