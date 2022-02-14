using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyProjectile : EnemyProjectile
{
    // Start is called before the first frame update

    public override void ProjectileSetup(Vector3 target)
    {
        base.ProjectileSetup(target);
        StartCoroutine(AttackSteps());
    }

    IEnumerator AttackSteps()
    {
        yield return new WaitForEndOfFrame();

        float t  = 0;
        transform.localScale = Vector3.zero;
        while (transform.localScale.x < 1)
        {
            transform.localScale = Vector3.one*t;
            t += Time.deltaTime*3;

            yield return null;
        }

        yield return new WaitForSeconds(.1f);

        LaunchAtPosition(player.transform.position);
    }
}
