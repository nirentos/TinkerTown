using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class part3Cogs : MonoBehaviour
{
    public int score, timer;
    public GameObject scoreTracker;
    private void Update()
    {
        if (score >=3)
        {
            var starScore = scoreTracker.GetComponent<scoreTracker>();
            switch (timer / 50)
            {
                case <= 8:
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
            gameObject.GetComponent<part3Cogs>().enabled = false;
        }
    }
    private void FixedUpdate()
    {
        timer++;
    }
}
