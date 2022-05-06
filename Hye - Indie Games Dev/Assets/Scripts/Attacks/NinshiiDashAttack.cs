using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinshiiDashAttack : EnemyWeapon
{
    [Header("Attack Settings")]
    public float dashForce = 10f;
    public ParticleSystem ps;
    bool beingUsed;
    public GameObject requiredObject;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (beingUsed)
        {
            enemy.rb.MoveRotation(Quaternion.Slerp(enemy.rb.rotation, Quaternion.LookRotation(enemy.player.transform.position - enemy.transform.position, Vector3.up), Time.deltaTime * 3f));
        }
    }

    protected override bool CheckConditions()
    {
        bool result = base.CheckConditions();
        if (requiredObject == null) { result = false; }
        return result;
    }

    public void DashStart()
    {
        beingUsed = true;
        ps.Play();
    }

    public void DashAttack()  // needs to be in it's own method for animation events to work
    {
        beingUsed = false;
        Attack();
    }

    public void DashFinished()
    {
        Finished();

        ps?.Stop();

    }
    public override void Attack()
    {
        base.Attack();
        enemy.rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);
        attacking = false;
    }
}
