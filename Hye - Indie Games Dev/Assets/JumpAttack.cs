using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpAttack : EnemyWeapon
{
    [Header("Attack Settings")]
    private int projectileAmount;

    public EnemyProjectile projectilePrefab;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void jStart()
    {
        enemy.rb.useGravity = false;
    }
    public void jAttack()  // needs to be in it's own method for animation events to work
    {
        Attack();
    }

    public void jFinishd()
    {
        Finished();
        enemy.rb.useGravity = true;
    }
    public override void Attack()
    {
        base.Attack();
        EnemyProjectile ep = Instantiate(projectilePrefab, enemy.groundPoint.position + Vector3.up * 1f, Quaternion.LookRotation(transform.forward, Vector3.up));
        ep.ProjectileSetup(enemy.transform.position + enemy.transform.forward);
        attacking = false;
    }
}
