using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public float dragSpeed;
    Vector2 dragOrigin;
    Vector2 camOrigin;

    private InputManager inputManager;
    private void Start()
    {
        inputManager = InputManager.Instance;
    }

    
    void Update()
    {
       if (inputManager.GetPlayerTap())
        {
            dragOrigin = inputManager.GetPlayerTapPos();
            camOrigin = transform.position;

        }
        if (inputManager.GetPlayerTouch())
        {
            Vector2 dragPos = inputManager.GetPlayerTouchPosition();

            transform.position = new Vector2(camOrigin.x + ((dragOrigin.x - dragPos.x)* dragSpeed), transform.position.y);
        } 

    }
}
