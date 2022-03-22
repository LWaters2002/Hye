using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    // Start is called before the first frame update
    public string name;
    public int amount { get; private set; }
    protected PlayerController pc;

    public Item(PlayerController _pc, int _amount)
    {
        amount = _amount;
        pc = _pc;
    }

    protected enum CannotUseTypes
    {
        NotEnough,
        InvalidPlace
    }

    public virtual bool Use()
    {
        if (amount <= 0) { CannotUse(CannotUseTypes.NotEnough); return false; }

        amount--;
        Debug.Log($"Item {name} used, {amount} remain");
        
        
        return true;
    }   

    protected virtual void CannotUse(CannotUseTypes reason)
    {
        Debug.Log($"Cannot use item {name} because {reason.ToString()}");
    }

}


