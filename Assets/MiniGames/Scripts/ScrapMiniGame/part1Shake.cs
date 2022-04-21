using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class part1Shake : MonoBehaviour
{
    public int timer, shakes;
    public GameObject soundPooler, nextPart, clock, scoreTracker;
    Rigidbody2D rb;

    private InputManager inputManager;
    private void Start()
    {
        inputManager = InputManager.Instance;
        rb = clock.GetComponent<Rigidbody2D>();
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

    private void FixedUpdate()
    {
        timer++;

        if (Vector3.Magnitude(inputManager.GetPhoneGyro()) >= 5f)
        {
            shakes++;
            soundPooler.GetComponent<soundPooler>().playSound();
            clock.GetComponent<AudioSource>().volume = clock.GetComponent<AudioSource>().volume - 0.02f;
        }
        if (shakes >= 50)
        {
            var starScore = scoreTracker.GetComponent<scoreTracker>();
            switch (timer / 50)
            {
                case <= 10:
                    starScore.part1 = 3;
                    break;
                case <= 15:
                    starScore.part1 = 2;
                    break;
                default:
                    starScore.part1 = 1;
                    break;
            }
            nextPart.SetActive(true);
            gameObject.SetActive(false);
        }
        rb.velocity = inputManager.GetPhoneGyro();
    }
}
