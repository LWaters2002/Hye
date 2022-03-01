using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushiiSpinAttack : EnemyWeapon
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

    public void MushAttack()  // needs to be in it's own method for animation events to work
    {
        Attack();
    }

    public void MushFinished() 
    {
        Finished();
    }
    public override void Attack()
    {
        base.Attack();
        EnemyProjectile ep = Instantiate(projectilePrefab, enemy.groundPoint.position + Vector3.up * 1.5f, Quaternion.LookRotation(transform.forward, Vector3.up));
        ep.ProjectileSetup(transform.position + transform.forward);
        attacking = false;
    }
}
