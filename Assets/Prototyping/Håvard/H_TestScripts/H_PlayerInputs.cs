using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_PlayerInputs : MonoBehaviour
{
    private Vector2 startPos;
    public int minimumSwipeDistance = 50;
    private bool fingerDown;
    private InputManager inputManager;
    private H_GameController gameController;

    private void Start()
    {
        inputManager = InputManager.Instance;
        gameController = GetComponent<H_GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fingerDown == false && inputManager.GetPlayerTouch())
        {
            startPos = inputManager.GetPlayerTouchPosition();
            fingerDown = true;
        }

        if (fingerDown)
        {
            if (inputManager.GetPlayerTouchPosition().y >= startPos.y + minimumSwipeDistance)
            {
                fingerDown = false;
                Debug.Log("Upwards Swipe");
            }

            if (inputManager.GetPlayerTouchPosition().y <= startPos.y - minimumSwipeDistance)
            {
                fingerDown = false;
                Debug.Log("Downwards Swipe");
            }

            if (inputManager.GetPlayerTouchPosition().x >= startPos.x + minimumSwipeDistance)
            {
                fingerDown = false;
                gameController.SwipeScreen(1);
            }

            if (inputManager.GetPlayerTouchPosition().x <= startPos.x - minimumSwipeDistance)
            {
                fingerDown = false;
                gameController.SwipeScreen(-1);
            }
        }
    }
}
