using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Movement Settings", menuName = "sObjects/New Movement Settings")]
public class PlayerSettingsMovement : ScriptableObject
{
    [Header("Movement Settings")]
    public float speed;

    [Header("Slide Settings")]
    public float minSlideTime = .3f;
    public float slideTime = 1.2f;
    public float slideForce = 3f;
    public float slideRecoveryTime = .5f;
    public float autoSlideOnSlopes = .93f;

    [Header("Jumping")]
    public float jumpForce = 15f;


    [Header("Movement multipliers")]
    public float generalMovementMultiplier = 10f;
    public float airMovementMultiplier = .4f;
    public float aimMovementMultiplier = .5f;
    public float slideMovementMultiplier = 0.1f;

    [Header("Drag Settings")]
    public float groundDrag;
    public float slideDrag = 0.1f;
    public float airDrag;

    [Header("Grouding and Jumping")]
    public LayerMask groundMask;

}
