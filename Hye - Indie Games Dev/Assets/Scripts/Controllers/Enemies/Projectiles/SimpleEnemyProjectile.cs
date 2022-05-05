using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemyProjectile : EnemyProjectile
{
    // Start is called before the first frame update

    public override void ProjectileSetup(Vector3 target)
    {
        base.ProjectileSetup(target);
        LaunchAtPosition(player.transform.position);
    }
}
