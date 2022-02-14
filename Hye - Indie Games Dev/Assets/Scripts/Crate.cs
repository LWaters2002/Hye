using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour, IStatusable, IDamagable
{
    public float health {get;}

    private void Start() 
    {
    }

    public void TakeDamage(float damageAmount, StatusType damageType)
    {

    }

    public void ApplyStatus(float statusAmount, StatusType statusRecieved)
    {
        Debug.Log(statusAmount + " " + statusRecieved +" status has been applied");
    }

    public void ClearStatus()
    {
        Debug.Log("Status Cleared");
    }


}
