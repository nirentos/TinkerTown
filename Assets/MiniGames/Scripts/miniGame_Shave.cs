using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniGame_Shave : MonoBehaviour
{
    public Renderer woodMat;
    public float OpacitySpeed;
    public int time;
    Vector2 lastPos;

    private InputManager inputManager;
    private void Start()
    {
        inputManager = InputManager.Instance;

    }
    private void FixedUpdate()
    {
        bool finished = false;

        if (inputManager.GetPlayerTap())
        {
            lastPos = inputManager.GetPlayerTapPos();
        }
        if (inputManager.GetPlayerTouch() && woodMat.material.color.a >= 0f)
        {
            if (Vector2.Distance(inputManager.GetPlayerTouchPosition(),lastPos) > 2f)
            {
                woodMat.material.color = new Color(1, 1, 1, woodMat.material.color.a - OpacitySpeed);
                //spawn wood shaving
                lastPos = inputManager.GetPlayerTouchPosition();
            }
        }
        if(woodMat.material.color.a <= 0.5f)
        {
            print("finished");
            finished = true;
        }

        if (finished != true)
        {
            time++;
        }
    }
}
