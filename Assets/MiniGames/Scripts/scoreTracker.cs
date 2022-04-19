using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scoreTracker : MonoBehaviour
{
    public int part1, part2, part3, total;
    public void endScore()
    {
        total = part1 + part2 + part3;
        switch (total)
        {
            case >= 9:
                print("3");
                break;
            case >= 5:
                print("2");
                break;
            default:
                print("1");
                break;
        }
        
    }
}
