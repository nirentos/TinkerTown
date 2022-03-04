using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class H_ResourceDisplay : MonoBehaviour
{
    public TMP_Text[] resourceCounters;
    public TMP_Text workerCounter;
    public TMP_Text playerEnergyCounter;

    private H_ResourceTracking _resourceTracking;

    private void Awake()
    {
        _resourceTracking = GetComponent<H_ResourceTracking>();
    }

    public void UpdateTrackers()
    {
        for (int i = 0; i < resourceCounters.Length; i++)
        {
            resourceCounters[i].text = _resourceTracking.curResources[i].ToString();
        }
        workerCounter.text = _resourceTracking.avaliableWorkers.ToString() + " / " + _resourceTracking.maxWorkersAtTownLevel[_resourceTracking.townLevel].ToString();
        playerEnergyCounter.text = _resourceTracking.playerEnergyCurrent.ToString();
    }
}
