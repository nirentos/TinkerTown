using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class H_Building : MonoBehaviour
{
    private H_ResourceTracking _resourceTracking;

    #region floats
    public float resourceGenVar;
    #endregion

    #region integers
    public int resourceType;
    public int buildingLevel;
    public float curResourceAmount;

    public int[] resourceMaxAtLevel;
    public int[] workerMaxAtLevel;
    public int[] reqResToUpgradeAtLevel;

    private int curWorkers = 0;
    #endregion

    #region UI Elements
    public TMP_Text resourceCount;
    public TMP_Text workerCount;
    public TMP_Text prototypeHelpText;
    public GameObject hireWorkersButton;
    public GameObject fireWorkersButton;
    public GameObject upgradeButton;
    #endregion

    public void GenerateResource()
    {
        if (curResourceAmount < resourceMaxAtLevel[buildingLevel])
        {
            curResourceAmount += resourceGenVar * curWorkers * Time.deltaTime;
        }
    }

    public void UpdateUI()
    {
        resourceCount.text = Mathf.FloorToInt(curResourceAmount).ToString() + " / " + resourceMaxAtLevel[buildingLevel].ToString();
        workerCount.text = curWorkers.ToString() + " / " + workerMaxAtLevel[buildingLevel].ToString();
        
        if (curWorkers == workerMaxAtLevel[buildingLevel])
        {
            hireWorkersButton.SetActive(false);
        }
        else if (curWorkers == 0)
        {
            fireWorkersButton.SetActive(false);
        }
        else
        {
            hireWorkersButton.SetActive(true);
            fireWorkersButton.SetActive(true);
        }

        if (reqResToUpgradeAtLevel[buildingLevel] <= _resourceTracking.curResources[resourceType] && buildingLevel < reqResToUpgradeAtLevel.Length && _resourceTracking.playerEnergyCurrent >= 5*(buildingLevel+1))
        {
            upgradeButton.SetActive(true);
        }
        else
        {
            upgradeButton.SetActive(false);
        }

        if (prototypeHelpText != null)
        {
            prototypeHelpText.text = "Resource " + resourceType.ToString();
        }
    }

    public void CollectButtonPressed()
    {
        _resourceTracking.CollectResources(resourceType, Mathf.FloorToInt(curResourceAmount));
        curResourceAmount = 0;
    }

    public void CollectByWorker(int collectAmount)
    {
        _resourceTracking.CollectResources(resourceType, collectAmount);
        curResourceAmount -= collectAmount;
    }

    public void HireWorkerPressed()
    {
        if (curWorkers < workerMaxAtLevel[buildingLevel] && _resourceTracking.avaliableWorkers > 0)
        {
            curWorkers++;
            _resourceTracking.BorrowWorker();
        }
    }

    public void FireWorkerPressed()
    {
        if (curWorkers > 0)
        {
            curWorkers--;
            _resourceTracking.ReturnWorker();
        }
    }

    public void UpgradePressed()
    {
        _resourceTracking.ExpendPlayerEnergy(5 * (buildingLevel + 1));
        buildingLevel++;
    }

    public void InsertResourceTracker(H_ResourceTracking resTrackScr)
    {
        _resourceTracking = resTrackScr;
    }
}
