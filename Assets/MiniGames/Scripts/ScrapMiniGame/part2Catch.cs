using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class part2Catch : MonoBehaviour
{
    public List<GameObject> scrap;
    public GameObject nextPart, thispart,scoreTracker;

    //Vector2 dragOrigin;
    //Vector2 anchorPos;

    public int timer, score, dropTimer;
    int spawnTimer;
    public float dragSpeed;
    private InputManager inputManager;
    private void Start()
    {
        inputManager = InputManager.Instance;

    }
    private void Update()
    {
        //if (inputManager.GetPlayerTap())
        //{
        //    dragOrigin = inputManager.GetPlayerTapPos();
        //    anchorPos = transform.position;

        //}//get tap/mouse position and an anchor position
        if (inputManager.GetPlayerTouch())
        {
            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(inputManager.GetPlayerTouchPosition()).x, transform.position.y, 0);
        }
        if (timer/50 >= 15)
        {
            var starScore = scoreTracker.GetComponent<scoreTracker>();
            switch (score)
            {
                case >= 12:
                    starScore.part2 = 3;
                    break;
                case >=8:
                    starScore.part2 = 2;
                    break;
                default:
                    starScore.part2 = 1;
                    break;
            }
            nextPart.SetActive(true);
            thispart.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        if (spawnTimer >= (dropTimer*50))
        {
            spawnObject();
            spawnTimer = 0;
        }
        spawnTimer++;
        timer++;
    }

    void spawnObject()
    {
        Vector3 randPos = new Vector3(Random.Range(-2, 3), 6, 0);
        Instantiate(scrap[Random.Range(0, scrap.Count)], randPos, Quaternion.identity, gameObject.transform.parent);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        score++;
        Destroy(collision.gameObject);
        GetComponent<AudioSource>().Play();
    }
}
