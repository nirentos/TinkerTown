using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePieces : MonoBehaviour
{
    private InputManager inputManager;
    private void Start()
    {
        inputManager = InputManager.Instance;

    }
    private void Update()
    {
        if (inputManager.GetPlayerTouch())
        {
            if (Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(inputManager.GetPlayerTouchPosition())) <= .5f)
            {
                transform.position = new Vector3(Camera.main.ScreenToWorldPoint(inputManager.GetPlayerTouchPosition()).x, Camera.main.ScreenToWorldPoint(inputManager.GetPlayerTouchPosition()).y, 0);
            }
        }
    }
}
