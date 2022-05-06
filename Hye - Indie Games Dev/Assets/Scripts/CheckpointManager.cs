using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public Checkpoint activeCheckpoint { get; private set; }
    private Checkpoint[] checkpoints;

    public CheckpointManager()
    {
        checkpoints = Object.FindObjectsOfType<Checkpoint>();
    }

    public void SetActiveCheckpoint(Checkpoint check)
    {
        foreach (Checkpoint ch in checkpoints)
        {
            ch.SetAsActive(false);
        }
        check.SetAsActive(true);
        activeCheckpoint = check;
    }
}
