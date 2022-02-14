using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UIManager : MonoBehaviour, IManagable
{
    private PlayerUI_Manager playerUI_Manager;

    public PlayerStats stats { get; private set; }
    public GameManager gameManager { get; private set; }

    public void Setup(GameManager gameManager)
    {
        Debug.Log("Point");

        stats = gameManager.player.playerStats;

        IUIManagable[] mScripts = FindObjectsOfType<MonoBehaviour>().OfType<IUIManagable>().ToArray();

        foreach (IUIManagable s in mScripts)
        {
            s.Setup(this);
        }
    }



}
