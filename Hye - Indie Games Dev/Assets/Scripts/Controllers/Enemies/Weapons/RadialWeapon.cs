using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialWeapon : EnemyWeapon
{
    [Header("Attack Settings")]

    public float attackDuration;
    public float attackSpacing;
    public int maxProjectileAmount;
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
        Transform eT = enemy.transform; //Enemy Transform
        enemy.rb.velocity = Vector3.zero;

        Vector3 playerDir = (enemy.player.transform.position - eT.position);
        Vector3 tDir = new Vector3(eT.position.x, 0, eT.position.z);
        Vector3 tPlayerDir = new Vector3(playerDir.x, 0, playerDir.z).normalized;

        Quaternion tRot = Quaternion.LookRotation(tPlayerDir, Vector3.up);

        while (Mathf.Abs(Quaternion.Angle(eT.rotation, tRot)) > 2f)
        {
            eT.rotation = Quaternion.Slerp(eT.rotation, tRot, 2 * Time.deltaTime);
            yield return null;
        }

        projectileAmount = Mathf.RoundToInt(maxProjectileAmount * (enemy.health / enemy.maxHealth));

        float angleStep = Mathf.PI / (projectileAmount + 1);
        float angle = 0;

        for (int i = 0; i < projectileAmount; i++)
        {
            angle += angleStep;

            float targetX = attackSpacing * Mathf.Cos(angle);
            float targetY = attackSpacing * Mathf.Sin(angle);

            Vector3 spawnPos = transform.position + Vector3.up * 2;
            Vector3 targetPos = spawnPos + Vector3.up * targetY + transform.right * targetX;

            EnemyProjectile temp = Instantiate(projectilePrefab, spawnPos, Quaternion.identity);
            temp.ProjectileSetup(targetPos);

            yield return new WaitForSeconds(attackDuration / projectileAmount);
        }

        attacking = false;
        Finished();
    }



}
