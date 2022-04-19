using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miniGame_Shave : MonoBehaviour
{
    public Renderer woodMat;
    public float OpacitySpeed;
    public int time;
    Vector2 lastPos;
    
    public GameObject sound, nextPart,scoreTracker;

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
        if (inputManager.GetPlayerTouch() && woodMat.material.color.a >= 0f && !finished)
        {
            sound.GetComponent<AudioSource>().enabled = true;
            if (Vector2.Distance(inputManager.GetPlayerTouchPosition(),lastPos) > 50f)
            {
                woodMat.material.color = new Color(1, 1, 1, woodMat.material.color.a - OpacitySpeed);
                lastPos = inputManager.GetPlayerTouchPosition();
                
            }
        }
        else
        {
            sound.GetComponent<AudioSource>().enabled = false;
        }
        if(woodMat.material.color.a <= 0.5f)
        {
            finished = true;
            var starScore = scoreTracker.GetComponent<scoreTracker>();
            switch (time / 50)
            {
                case <= 5:
                    starScore.part2 = 3;
                    break;
                case <= 10:
                    starScore.part2 = 2;
                    break;
                default:
                    starScore.part2 = 1;
                    break;
            }
            nextPart.gameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        if (!finished)
        {
            time++;
            
        }
    }
}
