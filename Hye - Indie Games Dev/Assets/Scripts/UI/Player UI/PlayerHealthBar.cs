using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour, IPlayer_UI
{
    
    [SerializeField] private Image slider;
    [SerializeField] private AnimationCurve hBarAnim;

    private float health, maxHealth;

    public void Setup(PlayerStats stats)
    {
        maxHealth = stats.stats.maxHealth;
        health = stats.health;

        SetHealth(maxHealth);
        stats.OnHealthChange += SetHealth;
    }

    private void SetHealth(float tHealth)
    {
        //Checks if difference is worth calling coroutine
        if (Mathf.Abs(tHealth - health) < 1)
        {
            health = tHealth;
            slider.fillAmount = health/maxHealth;
        }
        else
        {
            StopCoroutine("ESetHealth");
            StartCoroutine(ESetHealth(tHealth));
        }
    }

    IEnumerator ESetHealth(float tHealth)
    {

        float t = 0;
        float tempHealth = health;

        while (t < 1)
        {
            t += Time.deltaTime * 5;

            health = Mathf.Lerp(tempHealth, tHealth, hBarAnim.Evaluate(t));
            slider.fillAmount = health / maxHealth;
            yield return null;
        }

    }
}
