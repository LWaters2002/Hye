using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyWeapon : MonoBehaviour
{
    [Header("Attack Statistics")]
    public float attackRate;

    public float minAttackRange;
    public float maxAttackRange;

    public int priority;

    [Header("Attack Owner Effector")]
    public float exhaustAmount;
    public float recoveryTime;

    protected float attackCooldown;

    public string animationName;
    protected bool attacking;

    bool isUsable = false;

    public delegate void voidEvent();
    public event voidEvent OnFinished;

    public Action<EnemyWeapon, bool> OnConditionsMet;

    protected Enemy enemy;

    protected virtual void Start()
    {
        attacking = false;
        attackCooldown = attackRate;
        enemy = GetComponentInParent<Enemy>();
    }

    protected virtual void Update()
    {
        if (!attacking)
        {
            attackCooldown -= Time.deltaTime;
        }

        bool conditionMet = CheckConditions();
        
        if (isUsable != conditionMet)
        {
            OnConditionsMet?.Invoke(this, conditionMet);
            isUsable = conditionMet;
        }
    }

    protected virtual bool CheckConditions() // Default Conditions : Within minimum and maximum range, Attack is not on cooldown.
    {
        if (!(enemy.distanceToPlayer > minAttackRange && enemy.distanceToPlayer < maxAttackRange)) { return false; } //Checks player is within range
        if (attackCooldown > 0) { return false; } //Checks if the weapon is offCooldown

        return true;
    }

    protected void Finished()
    {
        attackCooldown = attackRate;
        attacking = false;
        OnFinished?.Invoke();
    }

    public virtual void Attack()
    {
        attacking = true;
    }

    private void OnDestroy()
    {
        OnConditionsMet?.Invoke(this, false);
    }

}
