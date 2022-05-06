using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WushiiRadialAttack : EnemyWeapon
{
    [Header("Attack Settings")]
    public int projectileAmount;
    public float projectileSpacing;
    public EnemyProjectile ProjectilePrefab;
    public Transform spawnTransform;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void RadialAttack()
    {
        Attack();
    }

    public void RadialFinished()
    {
        Finished();
        attacking = false;
    }

    public override void Attack()
    {
        base.Attack();
        for (int i = 0; i < projectileAmount; i++)
        {
            Vector3 targetPos = Vector3.zero;
            float theta = Mathf.Deg2Rad * (i * (180 / projectileAmount - 1) + 90);
            targetPos = (transform.forward * Mathf.Sin(theta) + transform.up * Mathf.Cos(theta)).normalized;
            targetPos *= projectileSpacing;

            EnemyProjectile ep = Instantiate(ProjectilePrefab, spawnTransform.position + targetPos * .1f, Quaternion.identity);

            targetPos += spawnTransform.position;
            ep.ProjectileSetup(targetPos);
        }
    }
}
