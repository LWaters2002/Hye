using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerWeapon : MonoBehaviour
{
    [Header("Stats")]
    public int chargeDamage;
    public float chargeSpeed, chargeCooldown, stunRecovery, knockbackForce;
    private float chargeCoolTime, stunRecoverTime;
    public AnimationCurve chargeCurve;
    
    private float chargeCurrentDuration = 0;
    private bool isCharging;
    private bool canCharge = true;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
