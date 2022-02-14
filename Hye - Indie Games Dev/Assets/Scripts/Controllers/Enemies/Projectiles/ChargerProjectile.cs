using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerProjectile : EnemyProjectile
{
    Vector3 firstPosition;
    bool movedToPoints = false;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    public override void ProjectileSetup(Vector3 target)
    {
        base.ProjectileSetup(target);
        firstPosition = target;
        StartCoroutine(AttackSteps());
    }

    IEnumerator AttackSteps()
    {
        yield return new WaitForEndOfFrame();

        while (!LerpToPoint(firstPosition, 2f, true))
        {
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        LaunchAtPosition(player.playerMovement.PredictPosition(.5f));
        while (true)
        {
            transform.position += (player.transform.position - transform.position).normalized*5f*Time.deltaTime;
            yield return null;
        }
    }
}
