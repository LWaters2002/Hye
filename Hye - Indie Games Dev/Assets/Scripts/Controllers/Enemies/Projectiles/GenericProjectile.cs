using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericProjectile : EnemyProjectile
{
    // Start is called before the first frame update
    public bool faceVelocity = true;

    public override void ProjectileSetup(Vector3 _target)
    {
        base.ProjectileSetup(_target);
        LaunchAtPosition(_target);
    }

    void Update()
    {
        if (!faceVelocity || rb.velocity == Vector3.zero) { return; }
        transform.rotation = Quaternion.LookRotation(rb.velocity, Vector3.up);
    }

}

