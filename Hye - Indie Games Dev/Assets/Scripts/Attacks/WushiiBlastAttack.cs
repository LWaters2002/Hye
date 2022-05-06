using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WushiiBlastAttack : EnemyWeapon
{
    [Header("Attack Settings")]
    bool beingUsed;
    public GameObject requiredObject;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        if (beingUsed)
        {
            Quaternion targetRot = Quaternion.Euler(Quaternion.LookRotation(enemy.player.transform.position - enemy.transform.position, Vector3.up).eulerAngles + new Vector3(0, 90, 0));
            enemy.rb.MoveRotation(Quaternion.Slerp(enemy.rb.rotation, targetRot, Time.deltaTime * 3f));
        }
    }

    protected override bool CheckConditions()
    {
        bool result = base.CheckConditions();
        if (requiredObject == null) { result = false; }
        return result;
    }
    public void BlastStart()
    {
        beingUsed = true;
    }

    public void BlastFinished()
    {
        Finished();
        beingUsed = false;
    }
    public override void Attack()
    {
        base.Attack();
        attacking = false;
    }
}
