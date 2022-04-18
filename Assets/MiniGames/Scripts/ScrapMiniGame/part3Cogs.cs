using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class part3Cogs : MonoBehaviour
{
    public int score, timer;
    private void Update()
    {
        if (score >=3)
        {
            //end reload main scene
        }
    }
    private void FixedUpdate()
    {
        timer++;
    }
}
