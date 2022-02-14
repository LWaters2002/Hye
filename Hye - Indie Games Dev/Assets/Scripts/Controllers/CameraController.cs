using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class CameraController : MonoBehaviour
{

//    private PlayerControls controls;

    [Header("Mouse Settings")]
    public float sensitivity = .01f;
    public float sensX;
    public float sensY;

    float mouseX, mouseY;
    float xRotation, yRotation;

    [SerializeField] Transform orientation;

    public Transform cameraTarget;

    public void Setup(PlayerControls controls)
    {
        controls.Player.Look.performed += ctx => ReadInput(ctx);
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        cameraTarget.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    public void ReadInput(InputAction.CallbackContext context)
    {
        Vector2 mouseVector = context.ReadValue<Vector2>();
        mouseX = mouseVector.x;
        mouseY = mouseVector.y;

        xRotation -= mouseY * sensY * sensitivity;//*Time.deltaTime;
        yRotation += mouseX * sensX * sensitivity;//*Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, -85f, 85f);

    }
}
