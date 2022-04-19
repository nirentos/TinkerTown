using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigame_clockPuzzle : MonoBehaviour
{
    public int piecesInPlace;
    public int time;
    public GameObject scoreTracker;
    bool finished = false;

    // Update is called once per frame
    void Update()
    {
        if (piecesInPlace == 7)
        {
            finished = true;
            GetComponent<AudioSource>().enabled = true;
            var starScore = scoreTracker.GetComponent<scoreTracker>();
            switch (time/50)
            {
                case <= 10:
                    starScore.part3 = 3;
                    break;
                case <= 15:
                    starScore.part3 = 2;
                    break;
                default:
                    starScore.part3 = 1;
                    break;
            }
            starScore.endScore();
            gameObject.GetComponent<minigame_clockPuzzle>().enabled = false;
        }
    }
    public void IncreaseCount()
    {
        piecesInPlace++;
    }
    private void FixedUpdate()
    {
        if (!finished)
        {
            time++;
        }
    }
}
