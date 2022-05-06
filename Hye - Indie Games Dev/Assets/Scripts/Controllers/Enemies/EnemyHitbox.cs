using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : MonoBehaviour, IDamagable, IStatusable
{
    public float health { get; private set; }
    public float partHealth;
    public StatusType statusWeakness;
    public bool onlyTakesDamageFromWeakness = false;
    public float partMultiplier = 1f;
    public bool isVulnerable;
    public int vulnerableNumber;
    public bool partIsDestroyable;
    public bool partContributesToHealth;

    Enemy enemyReference;
    public Status[] statuses { get; private set; }

    private void Start()
    {
        health = partHealth;
        enemyReference = GetComponentInParent<Enemy>();
    }

    public void TakeDamage(float damageAmount, StatusType damageType, Vector3 damagePos)
    {
        if (onlyTakesDamageFromWeakness && damageType != statusWeakness) { return; }
        health -= damageAmount;
        if (!partContributesToHealth)
        {
            damageAmount = 0;
        }

        if (isVulnerable)
        {
            // enemyReference.VulnerabilityHit(vulnerableNumber,damageType);
        }

        enemyReference.TakeDamage(damageAmount * partMultiplier, damageType, damagePos);

        if (health <= 0 && partIsDestroyable)
        {
            Destroy(gameObject);
        }
    }

    public void ApplyStatus(GameObject obj, float statusAmount, StatusType statusRecieved)
    {
        enemyReference.ApplyStatus(obj, statusAmount, statusRecieved);
    }
}
