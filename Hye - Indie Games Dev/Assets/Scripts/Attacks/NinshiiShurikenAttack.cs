using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinshiiShurikenAttack : EnemyWeapon
{
    [Header("Attack Settings")]
    public EnemyProjectile projectilePrefab;
    bool beingUsed;

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

    public void ShurikenStart()
    {
        beingUsed = true;
    }
    public void ShurikenAttack()
    {
        Attack();
    }

    public void ShurikenFinished()
    {
        Finished();
    }
    public override void Attack()
    {
        base.Attack();
        EnemyProjectile ep = Instantiate(projectilePrefab, transform.position + transform.forward, Quaternion.identity);
        ep.ProjectileSetup(transform.forward);
        beingUsed = false;
        attacking = false;
    }
}
