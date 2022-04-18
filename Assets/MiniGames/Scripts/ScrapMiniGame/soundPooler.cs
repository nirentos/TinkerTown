using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }
    string soundSpawn = "";
    public List<Pool> soundPools;
    public Dictionary<string, Queue<GameObject>> poolDictonary;
    private void Start()
    {
        poolDictonary = new Dictionary<string, Queue<GameObject>>();

        foreach (Pool pool in soundPools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictonary.Add(pool.tag, objectPool);
        }
    }
    public void playSound()
    {
        int randNumb = Random.Range(1,4);
        switch (randNumb)
        {
            case 1:
                soundSpawn = "clang";
                break;
            case 2:
                soundSpawn = "thud";
                break;
            case 3:
                soundSpawn = "glass";
                break;
            default:
                return;
        }
        if (!poolDictonary.ContainsKey(soundSpawn))
        {
            return;
        }

        GameObject objToSpawn = poolDictonary[soundSpawn].Dequeue();

        objToSpawn.SetActive(false);
        objToSpawn.SetActive(true);

        poolDictonary[soundSpawn].Enqueue(objToSpawn);


    }
}
