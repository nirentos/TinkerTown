using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveCogs : MonoBehaviour
{
    private InputManager inputManager;
    Vector2 startPos;
    private void Start()
    {
        inputManager = InputManager.Instance;
        startPos = transform.position;

    }
    private void FixUpdate()
    {
        if (inputManager.GetPlayerTouch())
        {
            if (Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(inputManager.GetPlayerTouchPosition())) <= .5f)
            {
                transform.position = new Vector3(Camera.main.ScreenToWorldPoint(inputManager.GetPlayerTouchPosition()).x, Camera.main.ScreenToWorldPoint(inputManager.GetPlayerTouchPosition()).y, 0);
            }
        }
        if (!inputManager.GetPlayerTouch() && Vector2.Distance(transform.position, startPos) >= .5f)
        {
            GetComponent<AudioSource>().Play();
            transform.position = startPos;
        }
    }
}
