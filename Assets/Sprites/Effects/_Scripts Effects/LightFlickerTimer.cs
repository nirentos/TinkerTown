using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlickerTimer : MonoBehaviour
{
    public Animator anim;
    public float timer;

    void Start()
    {
        timer = Random.Range(5f, 12f);
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            anim.SetTrigger("flicker");
            timer = Random.Range(anim.GetCurrentAnimatorStateInfo(0).length + 5f, 12f);
        }
    }
}
