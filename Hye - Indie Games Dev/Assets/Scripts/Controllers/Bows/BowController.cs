using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using UnityEngine.InputSystem;

public class BowController : MonoBehaviour
{
    private PlayerControls controls;

    [Header("Stats")]
    public float drawTime;
    protected float currentDrawTime = 0;
    protected bool isCharging;
    public float chargePercent { get; private set; }
    public Transform arrowHolder;
    public float chargeSpread = .2f;

    bool isAiming;
    public float fullChargeDistance = 50f;

    private ShakeListener shakeListener;
    private Camera cam;

    private PlayerStats stats;

    public BaseArrow equippedArrow;

    [Header("Transforms")]
    public Transform exitPoint;

    public void Setup(PlayerControls controls, PlayerStats stats)
    {
        this.stats = stats;
        this.controls = controls;

        shakeListener = Object.FindObjectOfType<ShakeListener>();
        cam = Camera.main;
        controls.Player.Fire.performed += ctx => ReadInput(ctx);
        controls.Player.Aim.performed += ctx => isAiming = ctx.ReadValue<float>() > .5;
    }

    // Update is called once per frame
    void Update()
    {
        DrawingAndFiring();
    }

    void DrawingAndFiring()
    {
        chargePercent = currentDrawTime / drawTime;
        chargePercent = Mathf.Clamp01(chargePercent);

        if (isCharging)
        {
            shakeListener.SetPerlin(chargePercent * 4, (1 - chargePercent) * .75f);
            currentDrawTime += Time.deltaTime;
        }

        if (currentDrawTime >= drawTime)
        {
            shakeListener.SetPerlin(0, -1);
            isCharging = false;
        }
    }

    public void ReadInput(InputAction.CallbackContext context)
    {
        if (!isAiming) { return; }
        float mouseIn = context.ReadValue<float>();

        if (mouseIn < .1 && chargePercent > .2)
        {
            Fire();
            shakeListener.SetPerlin(0, -1);
            currentDrawTime = 0;
        }

        if (mouseIn > .1)
        {
            isCharging = true;
        }
        else
        {
            currentDrawTime = 0;
            shakeListener.SetPerlin(0, -1);
            isCharging = false;
        }

    }

    void Fire()
    {
        stats.TakeEnergy(equippedArrow.energyCost);
        shakeListener.Shake(.2f, 1f, 7f, true);
        //Gets center of screen point

        Ray ray = cam.ViewportPointToRay(new Vector3(.5f, .5f, 0));
        RaycastHit hit;

        Vector3 targetPoint;

        if (Physics.Raycast(ray, out hit, fullChargeDistance))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(fullChargeDistance * chargePercent);
        }

        Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red, 3f);

        //Adds randomness e.g. spread if arrow isn't fully charged
        Vector3 arrowDirection = targetPoint - exitPoint.position + transform.right*(Random.Range(-1,1) * (1 - chargePercent) * chargeSpread);
        BaseArrow temp = Instantiate(equippedArrow, exitPoint.position, cam.transform.rotation, arrowHolder);

        temp.Init(chargePercent, arrowDirection);
    }

}
