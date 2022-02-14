using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour, IPowerable
{
    public bool powerState { get; private set; }

    public void Toggle() { SetPower(!powerState); }

    public void SetPower(bool power)
    {
        powerState = power;

        if (power)
        {
            Destroy(gameObject);
        }
        else
        {

        }
    }

}
