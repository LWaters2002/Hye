using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Stats Settings", menuName = "sObjects/New Stat Settings")]
public class PlayerSettingsStats : ScriptableObject
{
    [Header("Health")]
    public float maxHealth;
    public float healthRegenRate;
    public float healthRegenTime;

    [Header("Energy")]
    public float maxEnergy;
    public float energyRegenRate;
    public float energyRegenTime;





}
