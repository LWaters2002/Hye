using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumbers : MonoBehaviour
{
    public TextMeshPro text;
    Camera cam;
    private void Start()
    {
        cam = Camera.main;
    }
    public void Init(float damageAmount)
    {
        text.text = Mathf.RoundToInt( damageAmount).ToString();
    }

    private void Update()
    {
        Billboard();
    }
    void Billboard() 
    {
        transform.rotation = cam.transform.rotation;
    }

}
