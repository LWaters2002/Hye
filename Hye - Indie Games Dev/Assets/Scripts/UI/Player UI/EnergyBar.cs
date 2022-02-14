using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour, IPlayer_UI
{
    [SerializeField] private Image slider;
    [SerializeField] private AnimationCurve eBarAnim;

    private float energy, maxEnergy;

    public void Setup(PlayerStats stats)
    {
        maxEnergy = stats.stats.maxEnergy;
        energy = stats.energy;

        SetEnergy(maxEnergy);
        stats.OnEnergyChange += SetEnergy;
    }

    private void SetEnergy(float tEnergy)
    {
        if (Mathf.Abs(tEnergy - energy) < 1)
        {
            energy = tEnergy;
            slider.fillAmount = energy/maxEnergy;
        }
        else
        {
            StopCoroutine("ESetEnergy");
            StartCoroutine(ESetEnergy(tEnergy));
        }
    }

    IEnumerator ESetEnergy(float tEnergy)
    {
        float t = 0;
        float tempEnergy = energy;

        while (t < 1)
        {
            t += Time.deltaTime * 5f;

            energy = Mathf.Lerp(tempEnergy, tEnergy, eBarAnim.Evaluate(t));
            slider.fillAmount = energy / maxEnergy;
            yield return null;
        }
    }
}
