using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeListener : MonoBehaviour
{
    protected Camera cam;

    CinemachineVirtualCamera shakeCamera;
    CinemachineBasicMultiChannelPerlin camPerlin;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        shakeCamera = GameObject.Find("Player_Camera").GetComponent<CinemachineVirtualCamera>();
        camPerlin = shakeCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void SetPerlin(float frequency, float amplitude)
    {
        if (frequency >= 0) { camPerlin.m_FrequencyGain = frequency; }
        if (amplitude >= 0) { camPerlin.m_AmplitudeGain = amplitude; }
    }

    public void Shake(float duration, float frequency, float amplitude, bool falloff)
    {
        StartCoroutine(EShake(duration, frequency, amplitude, falloff));
    }

    IEnumerator EShake(float duration, float frequency, float amplitude, bool falloff)
    {
        float t = 0;
        while (t < duration)
        {
            t += Time.deltaTime;
            float elapsed = t / duration;
            if (falloff)
            {
                camPerlin.m_FrequencyGain = frequency * elapsed;
                camPerlin.m_AmplitudeGain = amplitude * elapsed;
            }
            else
            {
                camPerlin.m_FrequencyGain = frequency;
                camPerlin.m_AmplitudeGain = amplitude;
            }
            yield return null;
        }

        camPerlin.m_FrequencyGain = 0;
        camPerlin.m_AmplitudeGain = 0;
    }
}
