using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElderMushiiBreathAttack : EnemyWeapon
{
    [Header("Attack Settings")]
    private int projectileAmount;
    public Transform fireTransform;
    public float projectileSpawnRate;
    public EnemyProjectile projectilePrefab;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void BreathAttackStart()  // needs to be in it's own method for animation events to work
    {
        InvokeRepeating("Attack", 0, projectileSpawnRate);
    }

    public void BreathAttackStop()  // needs to be in it's own method for animation events to work
    {
        CancelInvoke("Attack");
    }

    public void BreathAttackFinished()
    {
        Finished();
    }
    public override void Attack()
    {
        base.Attack();
        EnemyProjectile ep = Instantiate(projectilePrefab, fireTransform.position, Quaternion.LookRotation(transform.forward, Vector3.up));
        ep.ProjectileSetup(fireTransform.position + fireTransform.forward);
    }
}
