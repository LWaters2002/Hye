using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    Vector3 transformPosition;
    bool active;
    Material material;
    CheckpointManager checkpointManager;
    PlayerController playerController;
    
    private void Awake()
    {
        material = GetComponent<MeshRenderer>().material;
        playerController = Object.FindObjectOfType<PlayerController>();
        SetAsActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !active)
        {
            playerController.checkpointManager.SetActiveCheckpoint(this);
        }
    }

    public void SetAsActive(bool _active)
    {
        if (_active)
        {
            material.color = Color.green;
        }
        else
        {
            material.color = Color.red;
        }

        active = _active;
    }
}
