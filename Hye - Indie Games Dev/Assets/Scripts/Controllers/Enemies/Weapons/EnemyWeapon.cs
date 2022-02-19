using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyWeapon : MonoBehaviour
{
    public float attackRate;
    protected float attackCooldown;

    public bool isReady { get; private set; }
    public string animationName;
    protected bool attacking;

    public delegate void voidEvent();
    public event voidEvent OnFinished;

    protected Enemy enemy;

    public float attackRange;

    protected virtual void Start()
    {
        attacking = false;
        isReady = false;
        attackCooldown = attackRate;
        enemy = GetComponentInParent<Enemy>();
    }

    protected virtual void Update()
    {
        if (!attacking)
        {
            attackCooldown -= Time.deltaTime;
        }

        isReady = (attackCooldown < 0);
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
        if (enemy != null)
        {
            enemy.weapons.Remove(this);
        }
    }

}
