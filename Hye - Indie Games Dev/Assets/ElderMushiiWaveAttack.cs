using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElderMushiiWaveAttack : EnemyWeapon
{
    [Header("Attack Settings")]
    private int projectileAmount;
    public EnemyProjectile projectilePrefab;
    public Transform projectileSpawn;
    public float waveScale = 3f;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void waveAttack()  // needs to be in it's own method for animation events to work
    {
        Attack();
    }

    public void waveFinished()
    {
        Finished();
    }

    public override void Attack()
    {
        base.Attack();
        EnemyProjectile ep = Instantiate(projectilePrefab, projectileSpawn.position, Quaternion.LookRotation(transform.forward, Vector3.up));
        ep.ProjectileSetup(projectileSpawn.position + projectileSpawn.forward);
        attacking = false;
    }
}
