using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTimer : MonoBehaviour
{
    public int minTime, maxTime;
    public int timer, randNumber;
    private void Start()
    {
        randNumber = Random.Range(minTime * 50, (maxTime + 1) * 50);
    }
    private void FixedUpdate()
    {

        if (timer >= randNumber)
        {
            GetComponent<AudioSource>().Play();
            int clipLeigth = Mathf.CeilToInt(GetComponent<AudioSource>().clip.length) * 50;
            randNumber = Random.Range((minTime * 50) + clipLeigth, (maxTime + 1) * 50);
            timer = 0;
        }

        timer++;


    }

}
