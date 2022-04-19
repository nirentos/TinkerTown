using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate : MonoBehaviour
{
    public bool Clockwise;
    public bool isRotating;
    public GameObject lastCog;
    public float speed;
    private void Start()
    {
        //GetComponent<AudioSource>().pitch = Random.Range(0.7f, 1.4f);
    }
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
            GetComponent<AudioSource>().enabled = true;
        }
    }
}
