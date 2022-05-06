using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform destination;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player == null) { player = other.GetComponentInParent<PlayerController>(); }

        if (player != null)
        {
            player.transform.position = destination.position;
        }
    }
}
