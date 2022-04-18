using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigame_WoodPart1 : MonoBehaviour
{
    [Header("log info")]
    public GameObject log;
    public Vector2 logEndPos;
    public float speed, Score;

    [Header("player controll")]
    public float dragSpeed;
    Vector2 dragOrigin;
    Vector2 anchorPos;


    private InputManager inputManager;
    private void Start()
    {
        inputManager = InputManager.Instance;
    }
    private void Update()
    {
        if (inputManager.GetPlayerTap())
        {
            dragOrigin = inputManager.GetPlayerTapPos();
            anchorPos = transform.position;

        }//get tap/mouse position and an anchor position
        if (inputManager.GetPlayerTouch())
        {
            Vector2 dragPos = inputManager.GetPlayerTouchPosition();

            transform.position = new Vector2(anchorPos.x - ((dragOrigin.x - dragPos.x) * dragSpeed), transform.position.y);
        }


        log.transform.position = Vector2.MoveTowards(log.transform.position, logEndPos, speed * Time.deltaTime);

        if (Vector2.Distance(log.transform.position,logEndPos) <= 1)
        {
            //end minigame
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Score++;
        Destroy(collision.gameObject);
    }
}
