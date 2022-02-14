using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossUI : MonoBehaviour
{
    private Enemy enemy;

    private float health, maxHealth;
    public Image slider;
    public TextMeshProUGUI text;

    public void Setup(Enemy enemy, string bossName)
    {
        this.enemy = enemy;
        text.text = bossName;
        
        maxHealth = enemy.maxHealth;
        health = enemy.health;

        enemy.OnHealthChange += SetHealth;
    }

    private void SetHealth(float tHealth)
    {
        //Checks if difference is worth calling coroutine
        if (Mathf.Abs(tHealth - health) < 1)
        {
            health = tHealth;
            slider.fillAmount = health / maxHealth;
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
            t += Time.deltaTime * 3;

            health = Mathf.Lerp(tempHealth, tHealth, t);
            slider.fillAmount = health / maxHealth;
            yield return null;
        }

    }

}
