using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElderMushiiSpawnerAttack : EnemyWeapon
{
    [Header("Attack Settings")]
    private int projectileAmount;
    public MushiiEnemy mushiiPrefab;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    public void SpawnerSpawn()  // needs to be in it's own method for animation events to work
    {
        Attack();
    }

    public void SpawnerFinished()
    {
        Finished();
    }

    public override void Attack()
    {
        base.Attack();
        Instantiate(mushiiPrefab, enemy.player.transform.position+Vector3.up*10f, Quaternion.identity);
    }
}
