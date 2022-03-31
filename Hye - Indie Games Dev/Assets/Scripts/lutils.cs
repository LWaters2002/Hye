using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lutils
{
    public static void SpawnDamageNumber(Vector3 pos, float damage) 
    {
        DamageNumbers d = GameObject.Instantiate(Resources.Load<DamageNumbers>("Prefabs/DamageNumbers"), pos, Quaternion.identity);
        d.Init(damage);
    }
}
