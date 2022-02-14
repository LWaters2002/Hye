using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class DebugTools : MonoBehaviour
{
    public float timeStep;
    public void AlterTimeScale(InputAction.CallbackContext context) 
    {
        float val = context.ReadValue<float>();
        Time.timeScale += val * timeStep;
    }
}
