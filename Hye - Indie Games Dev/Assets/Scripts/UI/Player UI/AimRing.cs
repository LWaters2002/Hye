using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimRing : MonoBehaviour, IPlayer_UI
{

    private BowController bow;

    public void Setup(PlayerStats stats)
    {
        bow = stats.playerController.bowController;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (bow == null) return;
        transform.localScale = Vector3.one*(1-bow.chargePercent);
        if (bow.chargePercent == 0){transform.localScale = Vector3.zero;}
    }
}
