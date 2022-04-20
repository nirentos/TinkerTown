using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class H_ResourceTracking : MonoBehaviour
{
    public int hardCurrency;
    public int townLevel;

    public GameObject forestLayer, layer2, layer3;
    public GameObject demoEnd;

    public int playerEnergyCurrent;
    public int playerEnergyMaximum;

    public int avaliableWorkers;
    public int[] maxWorkersAtTownLevel;

    public int[] curResources;

    private float idleWorkersCollectionTimer = 0;
    public float idleWorkersTimeBetweenCollects = 30;

    public void OnApplicationQuit()
    {
        Save();
    }

    private void Start()
    {
        avaliableWorkers = maxWorkersAtTownLevel[townLevel];
    }

    private void Update()
    {
        switch (townLevel)
        {
            case >= 4:
                demoEnd.SetActive(true);
                break;
            case >= 2:
                forestLayer.SetActive(false);
                layer3.SetActive(true);
                break;
            case >= 1:
                layer2.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void RestorePlayerEnergy(int energyToRestore)
    {
        playerEnergyCurrent += energyToRestore;

        if (playerEnergyCurrent > playerEnergyMaximum)
        {
            playerEnergyCurrent = playerEnergyMaximum;
        }
    }

    public void ExpendPlayerEnergy(int energyToUse)
    {
        playerEnergyCurrent -= energyToUse;
    }

    public void CollectResources(int resourceToCollect, int resourceAmountToCollect)
    {
        curResources[resourceToCollect] += resourceAmountToCollect;
    }

    public void BorrowWorker()
    {
        avaliableWorkers--;
    }

    public void ReturnWorker()
    {
        avaliableWorkers++;
    }

    public void UpgradeTownLevel()
    {
        avaliableWorkers += maxWorkersAtTownLevel[townLevel];
        townLevel++;
    }

    public void IdleWorkers(H_Building[] h_BuildingsAr)
    {
        if (avaliableWorkers > 0 && idleWorkersCollectionTimer < idleWorkersTimeBetweenCollects)
        {
            idleWorkersCollectionTimer += Time.deltaTime;
        }
        else if (avaliableWorkers > 0 && idleWorkersCollectionTimer >= idleWorkersTimeBetweenCollects)
        {
            idleWorkersCollectionTimer = 0;

            for (int j = 0; j < avaliableWorkers; j++)
            {
                int i = Random.Range(1, 2);
                if (Mathf.FloorToInt(h_BuildingsAr[i].curResourceAmount) > 0)
                {
                    h_BuildingsAr[i].CollectByWorker(1);
                }
            }
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("hardCurrency", hardCurrency);
        PlayerPrefs.SetInt("playerEnergy", playerEnergyCurrent);
        PlayerPrefs.SetInt("townLevel", townLevel);
        PlayerPrefs.SetInt("avaliableWorkers", avaliableWorkers);
        for (int i = 0; i < curResources.Length; i++)
        {
            PlayerPrefs.SetInt("Tracking of Resource " + i.ToString(), curResources[i]);
        }

        Debug.Log("Resource Tracking has been saved");
    }

    public void Restore()
    {
        hardCurrency = PlayerPrefs.GetInt("hardCurrency");
        playerEnergyCurrent = PlayerPrefs.GetInt("playerEnergy");
        townLevel = PlayerPrefs.GetInt("townLevel");
        avaliableWorkers = PlayerPrefs.GetInt("avaliableWorkers");
        for (int i = 0; i < curResources.Length; i++)
        {
            curResources[i] = PlayerPrefs.GetInt("Tracking of Resource " + i.ToString());
        }

        Debug.Log("Resource Tracking has been Restored");
    }

    public void OfflineCollection(int timePassed, H_Building[] h_BuildingsAr)
    {
        //var buildings = new H_Building[h_BuildingsAr.Length];

        //for (int i = 0; i < buildings.Length; i++)
        //{
        //    buildings[i] = h_BuildingsAr[i];
        //}

        //var resourcesToCollect = timePassed / idleWorkersTimeBetweenCollects;

        //for (int i = 0; i < resourcesToCollect; i++)
        //{
        //    int j = Random.Range(0, buildings.Length);
        //    if (buildings[j]._restorableResources > 0)
        //    {
        //        buildings[j].CollectFromOfflineTime(1);
        //    }
        //}
    }
}
