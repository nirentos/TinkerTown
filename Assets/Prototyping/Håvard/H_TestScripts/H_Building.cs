using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class H_Building : MonoBehaviour
{
    private H_ResourceTracking _resourceTracking;
    private H_GameController _gameController;

    #region floats
    public float resourceGenVar;
    #endregion

    #region integers
    public int buildingAndResourceType;
    public int buildingLevel;
    public float curResourceAmount;

    public int[] resourceMaxAtLevel;
    public int[] workerMaxAtLevel;

    private int curWorkers = 0;
    #endregion

    #region building sprites
    public Sprite[][] buildingSpriteCollection = new Sprite[5][];

    public Sprite defaultSprite;

    public Image[]buildingDisp;

    public Sprite[] buildingType0;
    public Sprite[] buildingType1;
    public Sprite[] buildingType2;
    #endregion

    #region Upgrade Requirements
    [Header("Upgrade Requirements")]
    public int[] reqTownLevelToUpgradeAtLevel;
    public int[] reqBuilding1LevelToUpgradeAtLevel;
    public int[] reqBuilding2LevelToUpgradeAtLevel;
    public int[] reqBuilding3LevelToUpgradeAtLevel;
    public int[] reqRes1ToUpgradeAtLevel;
    public int[] reqRes2ToUpgradeAtLevel;


    #endregion

    #region UI Elements
    public TMP_Text resourceCount;
    public TMP_Text workerCount;
    public TMP_Text prototypeHelpText;
    public GameObject hireWorkersButton;
    public GameObject fireWorkersButton;
    public GameObject upgradeButton;
    #endregion

    private void Awake()
    {
        _gameController = GetComponent<H_GameController>();

        buildingSpriteCollection = new Sprite[5][];

        buildingSpriteCollection[0].SetValue(buildingType0, buildingType0.Length);
        buildingSpriteCollection[1].SetValue(buildingType1, buildingType1.Length);
        buildingSpriteCollection[2].SetValue(buildingType2, buildingType2.Length);

        for (int i = 0; i < buildingSpriteCollection.Length; i++)
        {
            for (int j = 0; j < buildingSpriteCollection[i].Length; j++)
            {
                if (buildingSpriteCollection[i][j] == null)
                {
                    buildingSpriteCollection[i][j] = defaultSprite;
                }
            }
        }

        for (int i = 0; i < buildingDisp.Length; i++)
        {
            if (buildingSpriteCollection[buildingAndResourceType][i] != null && i <= buildingLevel)
            {
                buildingDisp[i].sprite = buildingSpriteCollection[buildingAndResourceType][i];
                buildingDisp[i].enabled = true;
            }
            else if (buildingSpriteCollection[buildingAndResourceType][i] != null && i > buildingLevel)
            {
                buildingDisp[i].sprite = buildingSpriteCollection[buildingAndResourceType][i];
                buildingDisp[i].enabled = false;
            }
            else
            {
                buildingDisp[i].enabled = false;
            }
        }

        if (buildingLevel > 0)
        {
            buildingDisp[0].enabled = false;
        }
    }

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

        if (reqRes1ToUpgradeAtLevel[buildingLevel] <= _resourceTracking.curResources[buildingAndResourceType] && buildingLevel < reqRes1ToUpgradeAtLevel.Length && _resourceTracking.playerEnergyCurrent >= 5*(buildingLevel+1))
        {
            upgradeButton.SetActive(true);
        }
        else
        {
            upgradeButton.SetActive(false);
        }

        if (prototypeHelpText != null)
        {
            prototypeHelpText.text = "Resource " + buildingAndResourceType.ToString();
        }
    }

    private bool Upgraderequirements()
    {
        if (reqTownLevelToUpgradeAtLevel[buildingLevel] <= _resourceTracking.townLevel || reqTownLevelToUpgradeAtLevel[buildingLevel] == null)
        {
            if (reqBuilding1LevelToUpgradeAtLevel[buildingLevel] <= _gameController._buildingScr[0].buildingLevel || reqBuilding1LevelToUpgradeAtLevel[buildingLevel] == null)
            {
                if (reqBuilding2LevelToUpgradeAtLevel[buildingLevel] <= _gameController._buildingScr[1].buildingLevel || reqBuilding2LevelToUpgradeAtLevel[buildingLevel] == null)
                {
                    if (reqBuilding3LevelToUpgradeAtLevel[buildingLevel] <= _gameController._buildingScr[2].buildingLevel || reqBuilding3LevelToUpgradeAtLevel[buildingLevel] == null)
                    {
                        if (reqRes1ToUpgradeAtLevel[buildingLevel] <= _resourceTracking.curResources[0] || reqRes1ToUpgradeAtLevel[buildingLevel] == null)
                        {
                            if (reqRes2ToUpgradeAtLevel[buildingLevel] <= _resourceTracking.curResources[1] || reqRes2ToUpgradeAtLevel[buildingLevel] == null)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }

        return false;
    }

    public void CollectButtonPressed()
    {
        _resourceTracking.CollectResources(buildingAndResourceType, Mathf.FloorToInt(curResourceAmount));
        curResourceAmount = 0;
    }

    public void CollectByWorker(int collectAmount)
    {
        _resourceTracking.CollectResources(buildingAndResourceType, collectAmount);
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
        buildingDisp[buildingLevel].enabled = true;
    }

    public void InsertResourceTracker(H_ResourceTracking resTrackScr)
    {
        _resourceTracking = resTrackScr;
    }
}
