using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class part1Shake : MonoBehaviour
{
    public int timer, shakes;
    public GameObject soundPooler, nextPart;

    private InputManager inputManager;
    private void Start()
    {
        inputManager = InputManager.Instance;
    }
    // Update is called once per frame
    private void OnEnable()
    {
        InputSystem.EnableDevice(UnityEngine.InputSystem.Gyroscope.current);
    }
    private void OnDisable()
    {
        InputSystem.DisableDevice(UnityEngine.InputSystem.Gyroscope.current);
    }

    private void Update()
    {
        if (Vector3.Magnitude(inputManager.GetPhoneGyro()) >=5f || inputManager.GetPlayerTap())
        {
            shakes++;
            soundPooler.GetComponent<soundPooler>().playSound();
        }
        if (shakes >= 100)
        {
            Destroy(gameObject);
            nextPart.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        timer++;
    }
}
