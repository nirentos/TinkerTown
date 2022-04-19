using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningSaw : MonoBehaviour
{
    
    void Update()
    {
        transform.Rotate(50, 0, 0 * Time.deltaTime);    
    }
}
