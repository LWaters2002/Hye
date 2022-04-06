using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement
{
    PlayerControls controls;
    PlayerSettingsMovement settings;

    Transform groundPosition;
    Transform orientation;

    RaycastHit slopeHit;

    Transform t;
    Rigidbody rb;

    Vector3 move;
    Vector3 slopeMove;

    float hIn = 0, vIn = 0;
    float movementMultiplier;
    float drag;
    float slideTimer;
    float slideRecoverTimer;

    bool slideInput;
    bool isGrounded;
    bool isAiming;
    bool isSliding;

    public PlayerMovement(Transform t, Rigidbody rb, PlayerControls controls, PlayerSettingsMovement settings)
    {
        this.t = t;
        this.rb = rb;
        this.controls = controls;
        this.settings = settings;

        controls = new PlayerControls();
        controls.Enable();
        controls.Player.Aim.performed += ctx => OnAim(ctx);
        controls.Player.Jump.performed += _ => Jump();
        controls.Player.Move.performed += ctx => ProcessInput(ctx);
        controls.Player.Crouch.performed += ctx => Slide(ctx);

        groundPosition = t.Find("Ground_Position").transform;
        orientation = t.Find("Orientation").transform;
    }

    public Vector3 PredictPosition(float timeInFuture)
    {
        return t.position + rb.velocity * timeInFuture;
    }
    // Update is called once per frame
    public void Tick()
    {
        isGrounded = Physics.CheckSphere(groundPosition.position, .4f, settings.groundMask);
        move = hIn * orientation.transform.right + vIn * orientation.transform.forward;

        slopeMove = Vector3.ProjectOnPlane(move, slopeHit.normal);

        SlideCalculate();
    }

    public void FixedTick()
    {
        CalculateForces();
        ScaleGravity();

    }


    private void SlideCalculate()
    {
        if (isSliding)
        {

            if (isGrounded) { slideTimer += Time.deltaTime; }

            if (isGrounded && !CheckSlope())
            {

                if (slideTimer > settings.slideTime)
                {
                    isSliding = false;
                }
            }

            if (!slideInput && slideTimer > settings.minSlideTime)
            {
                isSliding = false;
            }
        }
        else
        {
            slideRecoverTimer -= Time.deltaTime;
        }

        if (isSliding)
        {
            t.localScale = new Vector3(1, .5f, 1);
        }
        else
        {
            t.localScale = Vector3.one;
        }
    }

    private void Slide(InputAction.CallbackContext context)
    {
        bool input = (context.ReadValue<float>() == 1);
        slideInput = input;

        if (input && !isSliding && move != Vector3.zero && slideRecoverTimer < 0)
        {
            slideRecoverTimer = settings.slideRecoveryTime;
            slideTimer = 0;
            isSliding = true;

            if (CheckSlope())
            {
                rb.AddForce(settings.slideForce * slopeMove.normalized, ForceMode.VelocityChange);
            }
            else
            {
                rb.AddForce(settings.slideForce * move, ForceMode.VelocityChange);
            }
        }
    }

    private bool CheckSlope()
    {
        if (Physics.Raycast(t.position, Vector3.down, out slopeHit, 2f))
        {

            if (Vector3.Dot(slopeHit.normal, Vector3.up) < settings.autoSlideOnSlopes)
            {
                isSliding = true;
                slideRecoverTimer = settings.slideRecoveryTime;
                slideTimer = 0;
            }
            if (slopeHit.normal != Vector3.up) { return true; }

        }
        return false;
    }

    private void Jump()
    {

        if (isGrounded)
        {
            if (isAiming) { rb.AddForce(settings.jumpForce * t.up * .5f, ForceMode.VelocityChange); return; }
            rb.AddForce(settings.jumpForce * t.up, ForceMode.VelocityChange);
        }
    }

    private void OnAim(InputAction.CallbackContext context)
    {
        float val = context.ReadValue<float>();
        isAiming = val > .5;
    }

    void ScaleGravity()
    {
        if (!isGrounded)
        {
            float gravityStrength = 9.81f * (rb.drag);
            gravityStrength = Mathf.Clamp(gravityStrength, 9.81f, Mathf.Infinity);
            rb.AddForce(Vector3.down * gravityStrength, ForceMode.Acceleration);
        }
    }

    void CalculateForces()
    {
        Vector3 force = Vector3.zero;
        bool isSlope = CheckSlope();

        movementMultiplier = 1f;
        drag = settings.groundDrag;

        if (isGrounded)
        {
            movementMultiplier = 1f;
            drag = settings.groundDrag;
        }
        else
        {
            movementMultiplier = settings.airMovementMultiplier;
            drag = settings.airDrag;
        }

        if (isSliding) //Sliding
        {
            movementMultiplier = settings.slideMovementMultiplier;
            drag = settings.slideDrag;
        }

        float aim = (isAiming && isGrounded) ? 1 : 0;
        movementMultiplier *= (1 - (settings.aimMovementMultiplier * aim));

        Vector3 moveDir = (CheckSlope()) ? slopeMove.normalized : move.normalized;
        force = moveDir * settings.speed * settings.generalMovementMultiplier * movementMultiplier;

        rb.AddForce(force, ForceMode.Acceleration);
        rb.drag = drag;
    }

    void ProcessInput(InputAction.CallbackContext context)
    {
        Vector2 inp = context.ReadValue<Vector2>();
        hIn = inp.x;
        vIn = inp.y;
    }
}
