using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpaleEnemyProjectile : EnemyProjectile
{

    public float waitTime;
    private Vector3 pos;

    protected override void Start()
    {
        base.Start();
        rb.AddForce(Vector3.up * 10f, ForceMode.VelocityChange);
    }

    public override void ProjectileSetup(Vector3 target)
    {
        base.ProjectileSetup(target);
        StartCoroutine(AttackSteps());
    }

    IEnumerator AttackSteps()
    {
        yield return new WaitForEndOfFrame();

        float t = 0;

        while (t < waitTime)
        {
            t += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(.1f);

        LaunchAtPosition(target + Vector3.down * 10);
    }
}
