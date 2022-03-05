using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_ResourceTracking : MonoBehaviour
{
    public int hardCurrency;
    public int townLevel;

    public int playerEnergyCurrent;
    public int playerEnergyMaximum;

    public int avaliableWorkers;
    public int[] maxWorkersAtTownLevel;

    public int[] curResources;

    private float idleWorkersCollectionTimer = 0;

    private void Start()
    {
        avaliableWorkers = maxWorkersAtTownLevel[townLevel];
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

    public void IdleWorkers(H_Building[] h_BuildingsAr)
    {
        if (avaliableWorkers > 0 && idleWorkersCollectionTimer < 60)
        {
            idleWorkersCollectionTimer += Time.deltaTime;
        }
        else if (avaliableWorkers > 0 && idleWorkersCollectionTimer >= 60)
        {
            idleWorkersCollectionTimer = 0;

            int i = Random.RandomRange(0, h_BuildingsAr.Length);
            h_BuildingsAr[i].Collect(avaliableWorkers);
        }
    }
}
