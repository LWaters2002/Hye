using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class cameraSettings : MonoBehaviour
{
    private CinemachineVirtualCamera virtualCamera;
    private Cinemachine3rdPersonFollow cFollow;
    private PlayerControls controls;

    float targetDistance, targetFOV;
    float t = 1;
    public AnimationCurve cZoomCurve;
    // Start is called before the first frame update
    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cFollow = virtualCamera.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
    }

    private void Awake() {
        controls = new PlayerControls();
        controls.Enable();
        controls.Player.Aim.performed += ctx => OnAim(ctx);
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        t = 0;
        float val = context.ReadValue<float>();
        
        if (val > .5)
        {
            targetDistance = 3f;
            targetFOV = 80f;
        }else
        {
            targetDistance = 4f;
            targetFOV = 90f;
        }
    }
    
    private void Update() {
        if (t < 1)
        {
            t += Time.deltaTime;
            cFollow.CameraDistance = Mathf.Lerp(cFollow.CameraDistance, targetDistance,t);
            float tempFov = virtualCamera.m_Lens.FieldOfView;
            virtualCamera.m_Lens.FieldOfView = Mathf.Lerp(tempFov,targetFOV,t);
        }
    }
}
