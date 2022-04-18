using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cogCheck : MonoBehaviour
{
    public string tag;
    public GameObject host,cog;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(tag))
        {
            host.gameObject.GetComponent<part3Cogs>().score++;
            cog.SetActive(true);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
