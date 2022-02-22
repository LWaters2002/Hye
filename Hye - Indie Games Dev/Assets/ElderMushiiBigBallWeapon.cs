using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElderMushiiBigBallWeapon : EnemyWeapon
{
    [Header("Attack Settings")]
    private int projectileAmount;
    public Transform fireTransform;
    public float projectileSpawnRate;
    public EnemyProjectile projectilePrefab;
    private int repeatRandom = 1;
    private int repeatAmount = 1;
    public int repeatRange = 3;

    protected override void Start()
    {
        base.Start();
        repeatRandom = Mathf.RoundToInt(Random.Range(1, repeatRange));
    }

    protected override void Update()
    {
        base.Update();
    }

    public void BigBallStart()  // needs to be in it's own method for animation events to work
    {
        InvokeRepeating("Attack", Random.Range(0, .05f), projectileSpawnRate);
    }

    public void BigBallStop()
    {
        CancelInvoke("Attack");
    }

    public void BigBallFinished()
    {
        if (repeatAmount != repeatRandom)
        {
            repeatAmount++;
            enemy.an.Play(animationName, -1, 0f);
        }
        else
        {
            Finished();
            repeatAmount = 0;
            repeatRandom = Mathf.RoundToInt(Random.Range(1, repeatRange));
        }
    }

    public override void Attack()
    {
        base.Attack();
        EnemyProjectile ep = Instantiate(projectilePrefab, fireTransform.position, Quaternion.LookRotation(transform.forward, Vector3.up));
        ep.ProjectileSetup(fireTransform.position + fireTransform.forward);
    }
}
