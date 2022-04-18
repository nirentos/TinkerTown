using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selfdestruct : MonoBehaviour
{
    int timer = 0;
    private void FixedUpdate()
    {
        timer++;
        if (timer >= 150)
        {
            Destroy(gameObject);
        }
    }
}
