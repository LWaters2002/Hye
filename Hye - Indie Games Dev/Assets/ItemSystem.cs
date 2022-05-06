using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSystem : MonoBehaviour
{

    PlayerControls controls;
    PlayerStats playerStats;

    Action<Item> itemChange;
    Item currentItem;

    public void Init(PlayerController pc)
    {
        controls = pc.controls;
        playerStats = pc.playerStats;

        controls.Player.UseItem.performed += _ => UseItemButton(_.ReadValue<float>());

        currentItem = new HealthPot(pc, 3);
        itemChange?.Invoke(currentItem);
    }

    void UseItemButton(float val)
    {
        if (val > .5)
        {
            if (currentItem != null)
            {
                currentItem.Use();
                itemChange.Invoke(currentItem);
            }
        }
    }



}
