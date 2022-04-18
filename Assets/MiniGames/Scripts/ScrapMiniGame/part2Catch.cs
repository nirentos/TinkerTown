using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class part2Catch : MonoBehaviour
{
    public List<GameObject> scrap;
    public GameObject nextPart, thispart;

    Vector2 dragOrigin;
    Vector2 anchorPos;

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
        if (timer >= 500)
        {
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
        Instantiate(scrap[Random.Range(0, scrap.Count)], randPos, Quaternion.identity);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        score++;
        Destroy(collision.gameObject);
    }
}
