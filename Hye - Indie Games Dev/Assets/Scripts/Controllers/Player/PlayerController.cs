using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [HideInInspector] public PlayerControls controls { get; private set; }

    [Header("Player Objects")]
    public PlayerSettingsMovement mSettings;
    public PlayerSettingsStats sSettings;

    public GameManager gameManager { get; private set; }

    public Rigidbody rb { get; private set; }
    public PlayerMovement playerMovement { get; private set; }
    public PlayerStats playerStats { get; private set; }
    public CheckpointManager checkpointManager { get; private set; }
    public BowController bowController { get; private set; }
    public CameraController cameraController { get; private set; }
    public ItemSystem itemSystem { get; private set; }

    Vector3 spawnPos;

    public void Setup(GameManager gameManager)
    {
        this.gameManager = gameManager;
        spawnPos = transform.position;

        controls = new PlayerControls();
        controls.Player.Enable();

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        checkpointManager = new CheckpointManager();

        //Sub-scripts
        playerMovement = new PlayerMovement(transform, rb, controls, mSettings);
        playerStats = new PlayerStats(this, transform, sSettings);

        bowController = GetComponentInChildren<BowController>();
        cameraController = GetComponent<CameraController>();
        itemSystem = GetComponent<ItemSystem>();

        itemSystem.Init(this);

        cameraController.Setup(controls);
        bowController.Setup(controls, playerStats);

        playerStats.OnDeath += Death;
    }

    private void Update()
    {
        playerMovement.Tick();
        playerStats.Tick();
    }
    private void FixedUpdate()
    {
        playerMovement.FixedTick();
    }

    private void Death()
    {
        if (checkpointManager.activeCheckpoint != null)
        {
            transform.position = checkpointManager.activeCheckpoint.transform.position;
        }
        else
        {
            transform.position = spawnPos;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        playerStats.OnCollisionEnter(other);
    }
}
