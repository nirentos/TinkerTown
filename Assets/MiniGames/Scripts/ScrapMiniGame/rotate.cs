using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public bool Clockwise;
    public bool isRotating;
    public GameObject lastCog;
    public float speed;
    private void Update()
    {
        if (lastCog.GetComponent<rotate>().isRotating)
        {


            switch (Clockwise)
            {
                case true:
                    transform.Rotate(0, 0, -50 * Time.deltaTime * speed);
                    break;
                case false:
                    transform.Rotate(0, 0, 50 * Time.deltaTime * speed);
                    break;
            }
            isRotating = true;
        }
    }
}
