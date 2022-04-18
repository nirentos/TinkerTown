using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    //https://www.youtube.com/watch?v=5n_hmqHdijM&ab_channel=samyam
    //how to check current controlscheme https://github.com/UnityTechnologies/InputSystem_Warriors -> PlayerController.cs -> public void OnControlsChanged()
    private static InputManager _instance;
    private string curConScheme = null;
    private PlayerInput playerInput;

    public static InputManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private PlayerControls playerControls;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }

        playerControls = new PlayerControls();
    }

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    public bool GetPlayerTouch()
    {
        if (playerControls.Player.Touch.ReadValue<float>() > 0.5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool GetPlayerTap()
    {
        if (playerControls.Player.Tap.ReadValue<float>()> 0.5)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public Vector2 GetPlayerTapPos()
    {
        return playerControls.Player.TapPos.ReadValue<Vector2>();
    }

    public Vector2 GetPlayerTouchPosition()
    {
        return playerControls.Player.TouchPos.ReadValue<Vector2>();
    }

    public string CurrentControlScheme()
    {
        if (playerInput.currentControlScheme != curConScheme)
        {
            curConScheme = playerInput.currentControlScheme;
        }
        Debug.Log(curConScheme);
        return curConScheme;
    }

    public Vector3 GetPhoneGyro()
    {
        if (UnityEngine.InputSystem.Gyroscope.current == null)
        {
            return Vector3.zero;
        }
        return UnityEngine.InputSystem.Gyroscope.current.angularVelocity.ReadValue();
    }
}
