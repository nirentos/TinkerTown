using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourceGain : MonoBehaviour
{
    public int amount;
    public int resourceType;

  
    public void spawn(int gains, int type)
    {
        amount = gains;
        resourceType = type;
        DontDestroyOnLoad(this.gameObject);
    }
}
