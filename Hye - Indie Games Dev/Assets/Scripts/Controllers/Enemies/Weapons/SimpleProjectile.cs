using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : EnemyWeapon
{
    [Header("Attack Settings")]

    public float attackDuration;

    public int maxProjectileAmount;
    public float timeInbetweenProjectiles;

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

        for (int i = 0; i < maxProjectileAmount; i++)
        {
            EnemyProjectile temp = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            temp.ProjectileSetup(enemy.player.transform.position);

            yield return new WaitForSeconds(timeInbetweenProjectiles);
        }

        attacking = false;
        Finished();
    }
}
