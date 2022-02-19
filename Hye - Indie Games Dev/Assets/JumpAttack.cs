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

    public override void Attack()
    {
        base.Attack();
        EnemyProjectile ep = Instantiate(projectilePrefab,enemy.groundPoint.position+Vector3.up*1f,Quaternion.LookRotation(transform.forward,Vector3.up));
        ep.ProjectileSetup(enemy.transform.position + enemy.transform.forward);
        attacking = false;
    }
}
