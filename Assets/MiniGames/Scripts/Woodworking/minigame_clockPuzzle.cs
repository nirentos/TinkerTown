using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigame_clockPuzzle : MonoBehaviour
{
    public int piecesInPlace;

    // Update is called once per frame
    void Update()
    {
        if (piecesInPlace == 7)
        {
            //end
        }
    }
    public void IncreaseCount()
    {
        piecesInPlace++;
    }
}
