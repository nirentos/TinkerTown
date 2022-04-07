using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigame_WoodPart1 : MonoBehaviour
{
    public GameObject log;
    public Vector2 logEndPos;
    public float speed, Score;

    bool sawing;
    private void Update()
    {
        log.transform.position = Vector2.MoveTowards(log.transform.position, logEndPos, speed * Time.deltaTime);

        if (Vector2.Distance(log.transform.position,logEndPos) <= 1)
        {
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Score++;
        Destroy(collision.gameObject);
    }
}
