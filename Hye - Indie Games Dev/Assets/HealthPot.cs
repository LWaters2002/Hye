using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPot : Item
{

    private float healAmount = 40f;

    public HealthPot(PlayerController _pc, int _amount) : base(_pc, _amount) { }

    public override bool Use()
    {
        if (!base.Use()){return false;};

        if (pc != null)
        {
            pc.playerStats.Heal(healAmount);
        }

        return true;
    }
}
