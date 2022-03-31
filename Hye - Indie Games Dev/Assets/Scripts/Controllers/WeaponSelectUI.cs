using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WeaponSelectUI : MonoBehaviour, IUIManagable
{
    PlayerControls controls;
    PlayerControls playersControls;

    public GameObject UI;

    public UIManager UImanager { get; private set; }

    // Start is called before the first frame update
    public void Setup(UIManager UImanager)
    {
        this.UImanager = UImanager;

        playersControls = Object.FindObjectOfType<PlayerController>().controls;
        controls = new PlayerControls();
        controls.Player.Enable();

        controls.Player.ShowMenu.performed += ctx => ShowMenu(ctx);

        UI.SetActive(false);
    }

    private void ShowMenu(InputAction.CallbackContext context)
    {
        float val = context.ReadValue<float>();
        bool held = (val == 1);

        Cursor.visible = held;

        if (held)
        {
            playersControls.Disable();
            Time.timeScale = 0.05f;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            playersControls.Enable();
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        UI.SetActive(held);
    }

}
