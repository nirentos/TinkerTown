using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleDetection : MonoBehaviour
{
    public GameObject puzzlePiece;
    public GameObject parent;

    private void Update()
    {
        if (Vector2.Distance(transform.position, puzzlePiece.transform.position)<= 0.5f)
        {
            GetComponent<SpriteRenderer>().enabled = true;
            parent.GetComponent<minigame_clockPuzzle>().IncreaseCount();
            Destroy(puzzlePiece);
            GetComponent<puzzleDetection>().enabled = false;
        }
    }
}
