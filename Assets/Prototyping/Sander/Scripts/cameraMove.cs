using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMove : MonoBehaviour
{
    public float dragSpeed;
    bool canMove;
    Vector2 dragOrigin;
    Vector2 camOrigin;

    private InputManager inputManager;
    private void Start()
    {
        canMove = true;
        inputManager = InputManager.Instance;
    }

    
    void Update()
    {
       if (inputManager.GetPlayerTap())
        {
            dragOrigin = inputManager.GetPlayerTapPos();
            camOrigin = transform.position;

        }
        if (inputManager.GetPlayerTouch() && canMove)
        {
            Vector2 dragPos = inputManager.GetPlayerTouchPosition();

            transform.position = new Vector2(camOrigin.x + ((dragOrigin.x - dragPos.x)* dragSpeed), transform.position.y);
        } 

    }
    public void checkMove(bool shouldmove)
    {
        canMove = shouldmove;
    }
}
