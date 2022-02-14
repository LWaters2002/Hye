using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpaleWeapon : EnemyWeapon
{
    [Header("Attack Settings")]

    public float attackDuration;

    public int maxProjectileAmount;
    public float timeInbetweenProjectiles;
    public float seeAheadTime;
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
        StartCoroutine(SetUpAttack());
    }

    private IEnumerator SetUpAttack()
    {
        enemy.rb.velocity = Vector3.zero;

        for (int i = 0; i < maxProjectileAmount; i++)
        {
            EnemyProjectile temp = Instantiate
            (projectilePrefab,
                 enemy.player.playerMovement.PredictPosition(1f) + Vector3.up * 6f, Quaternion.identity);

            temp.ProjectileSetup(temp.transform.position);

            yield return new WaitForSeconds(timeInbetweenProjectiles);
        }

        attacking = false;
        Finished();
    }
}
