using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerUI_Manager : MonoBehaviour, IUIManagable
{
    private IPlayer_UI[] player_UIs;

    public UIManager UImanager { get; private set; }

    public void Setup(UIManager UImanager)
    {
        this.UImanager = UImanager;
        
        PlayerStats stats = this.UImanager.stats;
        
        player_UIs = GetComponentsInChildren<IPlayer_UI>();
        
        foreach (IPlayer_UI i in player_UIs)
        {
            i.Setup(stats);
        }

    }
}
