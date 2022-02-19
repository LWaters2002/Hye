using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomTextureSwitcher : MonoBehaviour
{

    public float hitRecovery;

    public Texture normalFace;
    public Texture hitFace;

    Renderer rend;
    private Enemy e;
    // Start is called before the first frame update
    void Start()
    {
        e = GetComponentInParent<Enemy>();
        rend = GetComponent<Renderer>();
        e.OnHit += GotHit;
    }

    void GotHit(float doesntMatter)
    {
        rend.material.mainTexture = hitFace;
        Invoke("SetNormalFace",hitRecovery);
    }

    void SetNormalFace()
    {
        rend.material.mainTexture = normalFace;
    }
}
