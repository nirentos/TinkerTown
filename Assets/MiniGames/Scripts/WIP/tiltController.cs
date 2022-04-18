using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class tiltController : MonoBehaviour
{
    public GameObject player;
    public float tiltSpeed;
    public Vector3 phoneGyro;
    Vector3 mousePos;

    public TextMeshProUGUI textbox;

    private InputManager inputManager;
    private void OnEnable()
    {
        InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
    }
    private void OnDisable()
    {
        InputSystem.DisableDevice(UnityEngine.InputSystem.Gyroscope.current);
    }
    private void Start()
    {
        inputManager = InputManager.Instance;
    }
    private void Update()
    {
        phoneGyro = inputManager.GetPhoneGyro();
    }

}
