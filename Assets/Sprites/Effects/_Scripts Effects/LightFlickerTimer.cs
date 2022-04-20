using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickerTimer : MonoBehaviour
{
    public Animator anim;
    public float timer, minTime, maxTime;

    void Start()
    {
        timer = Random.Range(minTime, maxTime);
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            anim.SetTrigger("flicker");
            timer = Random.Range(minTime, maxTime);
        }
    }
}
