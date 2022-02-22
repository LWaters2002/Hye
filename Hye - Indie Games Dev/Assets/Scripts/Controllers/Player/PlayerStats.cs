using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : IDamagable
{
    public float health { get; private set; }
    public float energy { get; private set; }
 
    public Transform t { get; private set; }
    public PlayerSettingsStats stats { get; private set; }
    public PlayerController playerController { get; private set; }

    private float energyTimer = 0f, healthTimer = 0f;

    //Events and Delegates
    public delegate void VoidEvent();
    public delegate void FloatEvent(float f);

    public event VoidEvent OnDeath;
    public event FloatEvent OnEnergyChange;
    public event FloatEvent OnHealthChange;

    public PlayerStats(PlayerController playerController,Transform t, PlayerSettingsStats stats)
    {
        this.playerController = playerController;
        this.stats = stats;
        this.t = t;

        health = this.stats.maxHealth;
        energy = this.stats.maxEnergy;

        healthTimer = this.stats.healthRegenTime;
        energyTimer = this.stats.energyRegenTime;

        OnDeath += Death;
    }

    public void Tick()
    {
        if (healthTimer < 0 && health < stats.maxHealth)
        {
            health += Time.deltaTime * stats.healthRegenRate;
            OnHealthChange?.Invoke(health);
        }
        else { healthTimer -= Time.deltaTime; }

        if (energyTimer < 0 && energy < stats.maxEnergy)
        {
            energy += Time.deltaTime * stats.energyRegenRate;
            OnEnergyChange?.Invoke(energy);
        }
        else { energyTimer -= Time.deltaTime; }

        //Clamp Values
        health = Mathf.Clamp(health, 0, stats.maxHealth);
        energy = Mathf.Clamp(energy, 0, stats.maxEnergy);
    }

    public void TakeDamage(float damageAmount, StatusType damageType, Vector3 damagePos)
    {
        health -= damageAmount;
        healthTimer = stats.healthRegenTime;
        OnHealthChange?.Invoke(health);
        if (health <= 0) { OnDeath?.Invoke(); }
    }

    public void TakeEnergy(float energyAmount)
    {
        energy -= energyAmount;
        energyTimer = stats.energyRegenTime;
        OnEnergyChange?.Invoke(energy);
    }

    private void Death()
    {
        health = stats.maxHealth;
        energy = stats.maxEnergy;
        
        OnHealthChange?.Invoke(health);
        OnEnergyChange?.Invoke(energy);
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Hazard"))
        {
            TakeDamage(1000f, StatusType.none, Vector3.zero);
        }
    }


}
