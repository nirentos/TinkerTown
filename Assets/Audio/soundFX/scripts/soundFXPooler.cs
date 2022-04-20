using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundFXPooler : MonoBehaviour
{
    public Queue<AudioSource>noisemaker;
    private void Start()
    {
        noisemaker = new Queue<AudioSource>();
        foreach (AudioSource aSS in GetComponentsInChildren<AudioSource>())
        {
            noisemaker.Enqueue(aSS);

        }
        print(noisemaker.Count);
        print("all added");
    }

    public void makeNoise()
    {
        print("playing noise");
        print(noisemaker == null);
        AudioSource noiseToPlay = noisemaker.Dequeue();
        noiseToPlay.Play();
        noisemaker.Enqueue(noiseToPlay);
    }
}
