using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rockwall : MonoBehaviour, IDestructable
{
    public void TryDestroy(GameObject source)
    {
        if (source.TryGetComponent(out BombScript bomb))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy() {
        
    }
}
