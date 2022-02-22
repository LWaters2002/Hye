using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfacesInitialisation
{
}

public interface IDamagable
{
    float health {get;}
    void TakeDamage(float damageAmount, StatusType damageType, Vector3 damagePos);
}

public interface IStatusable
{
    void ApplyStatus(float statusAmount, StatusType statusRecieved);
}

public interface IDestructable
{
    void TryDestroy(GameObject source);
}


public interface IPowerable
{
    bool powerState{get;}
    void Toggle();
    void SetPower(bool power);
}
public enum StatusType
{
    none,
    earth,
    water,
    lightning,
    fire
}