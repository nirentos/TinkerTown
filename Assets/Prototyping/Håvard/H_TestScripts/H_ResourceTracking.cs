using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_ResourceTracking : MonoBehaviour
{
    public int hardCurrency;

    public int playerEnergyCurrent;
    public int playerEnergyMaximum;

    public int workers;
    public Dictionary<string, int> resources;

    public void RestorePlayerEnergy(int energyToRestore)
    {
        playerEnergyCurrent += energyToRestore;

        if (playerEnergyCurrent > playerEnergyMaximum)
        {
            playerEnergyCurrent = playerEnergyMaximum;
        }
    }

    public void CollectResources(string resourceToCollect, int resourceAmountToCollect)
    {
        if (resources.TryGetValue(resourceToCollect, out int currentResourceAmount))
        {
            currentResourceAmount += resourceAmountToCollect;
        }
    }
}
