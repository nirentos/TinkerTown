using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class minigame_WoodPart1 : MonoBehaviour
{
    [Header("log info")]
    public GameObject log;
    public Vector2 logEndPos;
    public float speed, Score;
    public AudioSource sawGoing, sawSawing;

    [Header("player controll")]
    public float dragSpeed;

    public GameObject self, nextPart, scoreTracker;
    Vector2 sawBegin = new Vector2(0, 0.10f);
    Vector2 sawEnd = new Vector2(0, -7.2f);

    private InputManager inputManager;
    private void Start()
    {
        inputManager = InputManager.Instance;
        

    }
    private void Update()
    {

        if (inputManager.GetPlayerTouch())
        {
            Vector2 dragPos = inputManager.GetPlayerTouchPosition();

            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(inputManager.GetPlayerTouchPosition()).x, transform.position.y, 0);
        }


        log.transform.position = Vector2.MoveTowards(log.transform.position, logEndPos, speed * Time.deltaTime);
        if (Vector2.Distance(log.transform.position, sawBegin) <= 0.5f)
        {
            sawGoing.enabled = false;
            sawSawing.enabled = true;
        }

        if (Vector2.Distance(log.transform.position, sawEnd) <= 1)
        {
            sawGoing.enabled = true;
            sawSawing.enabled = false;
        }
        if (Vector2.Distance(log.transform.position, logEndPos) <= 0.5f)
        {
            var starScore = scoreTracker.GetComponent<scoreTracker>();
            switch (Score)
            {
                case >= 15:
                    starScore.part1 = 3;
                        break;
                case >= 7:
                    starScore.part1 = 2;
                        break;
                default:
                    starScore.part1 = 1;
                    break;
            }
            self.SetActive(false);
            nextPart.SetActive(true);
        }



    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Score++;
        Destroy(collision.gameObject);
    }
}
