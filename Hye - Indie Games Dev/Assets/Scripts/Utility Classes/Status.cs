using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Status
{

    public float statusAmount { get; private set; }
    public float statusMax { get; private set; }
    public bool isAffecting { get; private set; }

    public Status(float _statusMax)
    {
        statusMax = _statusMax;
        statusAmount = 0f;
        isAffecting = false;
    }

    public void AddToStatus(float amount) 
    {
        statusAmount += amount;
        if (!isAffecting && (statusAmount >= statusMax)) { isAffecting = true; }
    }

    public bool UpdateStatus()
    {
        statusAmount -= Time.deltaTime*.5f;
        if (isAffecting && (statusAmount <= 0)) 
        {
            statusAmount = 0;
            isAffecting = false;
        }
        return isAffecting;
    }

}
