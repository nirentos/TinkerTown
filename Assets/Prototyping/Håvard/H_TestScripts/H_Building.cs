using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class H_Building : MonoBehaviour
{
    private H_ResourceTracking _resourceTracking;
    private H_GameController _gameController;
    private int[] _buildingLevels;

    [HideInInspector]public int _restorableResources;

    #region floats
    public float resourceGenVar;
    #endregion

    #region integers
    public int buildingAndResourceType;
    public int buildingLevel;
    public int maxBuildingLevel;
    public float curResourceAmount;

    public int[] resourceMaxAtLevel;
    public int[] workerMaxAtLevel;

    private int curWorkers = 0;
    #endregion

    #region building sprites
    public List<Sprite[]> buildingSpriteCollection = new List<Sprite[]>();

    public Sprite defaultSprite;

    public Image[] buildingDisp;

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
    [Header("UI Elements")]
    [SerializeField]private bool visibleUI;

    public TMP_Text resourceCount;
    public TMP_Text workerCount;
    public TMP_Text prototypeHelpText;
    public GameObject hireWorkersButton;
    public GameObject fireWorkersButton;
    public GameObject upgradeButton;

    [Header("UI Buttons")]
    public Button HireWorker;
    public Button FireWorker;
    public Button Collect;
    public Button Upgrade;
    public Button Exit;

    [Header("Upgrade Requirements UI")]
    public Image[] buildingN1_UpgradeReqUI;
    public Image[] buildingN2_UpgradeReqUI;
    public TMP_Text resource1_UpgradeReqUI;
    public TMP_Text resource2_UpgradeReqUI;
    public TMP_Text playerEnergy_UpgradeReqUI;

    #endregion

    public void OnApplicationQuit()
    {
        Save();
    }

    private void Awake()
    {
        _gameController = GetComponent<H_GameController>();

        buildingSpriteCollection.Add(buildingType0);
        buildingSpriteCollection.Add(buildingType1);
        buildingSpriteCollection.Add(buildingType2);

        for (int i = 0; i < buildingSpriteCollection.Count; i++)
        {
            for (int j = 0; j < buildingSpriteCollection[i].Length; j++)
            {
                if (buildingSpriteCollection[i][j] == null)
                {
                    buildingSpriteCollection[i][j] = defaultSprite;
                }
            }
        }

        for (int j = 0; j < buildingDisp.Length; j++)
        {
            if (j < buildingSpriteCollection[buildingAndResourceType].Length)
            {
                buildingDisp[j].sprite = buildingSpriteCollection[buildingAndResourceType][j];
            }
            else
            {
                buildingDisp[j].enabled = false;
            }
        }

        for (int j = 1; j < buildingDisp.Length; j++)
        {
            buildingDisp[j].gameObject.SetActive(false);
        }

        buildingDisp[0].gameObject.SetActive(true);

        if (buildingLevel > 0)
        {
            Debug.Log("building " + gameObject.name + " is of level " + buildingLevel.ToString());
            for (int i = 1; i < buildingDisp.Length; i++)
            {
                if (i <= buildingLevel)
                {
                    Debug.Log("Activating building disp " + i.ToString());
                    buildingDisp[0].gameObject.SetActive(false);
                    buildingDisp[i].gameObject.SetActive(true);
                }
            }
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
        if (visibleUI)
        {
            //resourceCount.text = Mathf.FloorToInt(curResourceAmount).ToString() + " / " + resourceMaxAtLevel[buildingLevel].ToString();
            workerCount.text = curWorkers.ToString() + " / " + workerMaxAtLevel[buildingLevel].ToString();

            if (buildingAndResourceType == 0)
            {
                hireWorkersButton.SetActive(false);
                fireWorkersButton.SetActive(false);
                workerCount.gameObject.SetActive(false);
                resourceCount.gameObject.SetActive(false);
                prototypeHelpText.gameObject.SetActive(false);
                Collect.gameObject.SetActive(false);
            }
            else
            {
                hireWorkersButton.SetActive(true);
                fireWorkersButton.SetActive(true);
                workerCount.gameObject.SetActive(true);
                resourceCount.gameObject.SetActive(true);
                prototypeHelpText.gameObject.SetActive(true);
                Collect.gameObject.SetActive(true);
            }

            if (curWorkers == workerMaxAtLevel[buildingLevel])
            {
                hireWorkersButton.SetActive(false);
            }
            else if (curWorkers == 0)
            {
                fireWorkersButton.SetActive(false);
            }
            else if (buildingAndResourceType != 0)
            {
                hireWorkersButton.SetActive(true);
                fireWorkersButton.SetActive(true);
            }

            if (buildingAndResourceType == 0)
            {
                Collect.gameObject.SetActive(false);
            }

            if (Upgraderequirements())
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

            if (buildingAndResourceType != 0)
            {
                resourceCount.text = Mathf.FloorToInt(curResourceAmount).ToString() + " / " + resourceMaxAtLevel[buildingLevel].ToString();
                workerCount.text = curWorkers.ToString() + " / " + workerMaxAtLevel[buildingLevel].ToString();
            }

            #region Upgrade Requirements UI

            if (buildingAndResourceType == 0)
            {
                if (!buildingN1_UpgradeReqUI[reqBuilding2LevelToUpgradeAtLevel[buildingLevel]].enabled || buildingN1_UpgradeReqUI[reqBuilding2LevelToUpgradeAtLevel[buildingLevel] + 1].enabled)
                {
                    for (int i = 1; i < buildingN1_UpgradeReqUI.Length; i++)
                    {
                        if (i <= reqBuilding2LevelToUpgradeAtLevel[buildingLevel])
                        {
                            buildingN1_UpgradeReqUI[i].enabled = true;
                        }
                        else if (i > reqBuilding2LevelToUpgradeAtLevel[buildingLevel])
                        {
                            buildingN1_UpgradeReqUI[i].enabled = false;
                        }
                    }
                }

                if (!buildingN2_UpgradeReqUI[reqBuilding3LevelToUpgradeAtLevel[buildingLevel]].enabled || buildingN2_UpgradeReqUI[reqBuilding3LevelToUpgradeAtLevel[buildingLevel] + 1].enabled)
                {
                    for (int i = 1; i < buildingN2_UpgradeReqUI.Length; i++)
                    {
                        if (i <= reqBuilding3LevelToUpgradeAtLevel[buildingLevel])
                        {
                            buildingN2_UpgradeReqUI[i].enabled = true;
                        }
                        else if (i > reqBuilding3LevelToUpgradeAtLevel[buildingLevel])
                        {
                            buildingN2_UpgradeReqUI[i].enabled = false;
                        }
                    }
                }

                for (int i = 0; i < buildingN1_UpgradeReqUI.Length; i++)
                {
                    if (buildingSpriteCollection[1].Length < i)
                    {
                        buildingN1_UpgradeReqUI[i].sprite = buildingSpriteCollection[1][i];
                    }
                }
                for (int i = 0; i < buildingN1_UpgradeReqUI.Length; i++)
                {
                    if (buildingSpriteCollection[2].Length < i)
                    {
                        buildingN2_UpgradeReqUI[i].sprite = buildingSpriteCollection[2][i];
                    }
                }
            }
            else if (buildingAndResourceType == 1)
            {
                if (!buildingN1_UpgradeReqUI[reqBuilding1LevelToUpgradeAtLevel[buildingLevel]].enabled || buildingN1_UpgradeReqUI[reqBuilding1LevelToUpgradeAtLevel[buildingLevel] + 1].enabled)
                {
                    for (int i = 1; i < buildingN1_UpgradeReqUI.Length; i++)
                    {
                        if (i <= reqBuilding1LevelToUpgradeAtLevel[buildingLevel])
                        {
                            buildingN1_UpgradeReqUI[i].enabled = true;
                        }
                        else if (i > reqBuilding1LevelToUpgradeAtLevel[buildingLevel])
                        {
                            buildingN1_UpgradeReqUI[i].enabled = false;
                        }
                    }
                }

                if (!buildingN2_UpgradeReqUI[reqBuilding3LevelToUpgradeAtLevel[buildingLevel]].enabled || buildingN2_UpgradeReqUI[reqBuilding3LevelToUpgradeAtLevel[buildingLevel] + 1].enabled)
                {
                    for (int i = 1; i < buildingN2_UpgradeReqUI.Length; i++)
                    {
                        if (i <= reqBuilding3LevelToUpgradeAtLevel[buildingLevel])
                        {
                            buildingN2_UpgradeReqUI[i].enabled = true;
                        }
                        else if (i > reqBuilding3LevelToUpgradeAtLevel[buildingLevel])
                        {
                            buildingN2_UpgradeReqUI[i].enabled = false;
                        }
                    }
                }

                for (int i = 0; i < buildingN1_UpgradeReqUI.Length; i++)
                {
                    if (buildingSpriteCollection[0].Length < i)
                    {
                        buildingN1_UpgradeReqUI[i].sprite = buildingSpriteCollection[0][i];
                    }
                }
                for (int i = 0; i < buildingN1_UpgradeReqUI.Length; i++)
                {
                    if (buildingSpriteCollection[2].Length < i)
                    {
                        buildingN2_UpgradeReqUI[i].sprite = buildingSpriteCollection[2][i];
                    }
                }
            }
            else if (buildingAndResourceType == 2)
            {
                if (!buildingN1_UpgradeReqUI[reqBuilding1LevelToUpgradeAtLevel[buildingLevel]].enabled || buildingN1_UpgradeReqUI[reqBuilding1LevelToUpgradeAtLevel[buildingLevel] + 1].enabled)
                {
                    for (int i = 1; i < buildingN1_UpgradeReqUI.Length; i++)
                    {
                        if (i <= reqBuilding1LevelToUpgradeAtLevel[buildingLevel])
                        {
                            buildingN1_UpgradeReqUI[i].enabled = true;
                        }
                        else if (i > reqBuilding1LevelToUpgradeAtLevel[buildingLevel])
                        {
                            buildingN1_UpgradeReqUI[i].enabled = false;
                        }
                    }
                }

                if (!buildingN2_UpgradeReqUI[reqBuilding2LevelToUpgradeAtLevel[buildingLevel]].enabled || buildingN2_UpgradeReqUI[reqBuilding2LevelToUpgradeAtLevel[buildingLevel] + 1].enabled)
                {
                    for (int i = 1; i < buildingN2_UpgradeReqUI.Length; i++)
                    {
                        if (i <= reqBuilding2LevelToUpgradeAtLevel[buildingLevel])
                        {
                            buildingN2_UpgradeReqUI[i].enabled = true;
                        }
                        else if (i > reqBuilding2LevelToUpgradeAtLevel[buildingLevel])
                        {
                            buildingN2_UpgradeReqUI[i].enabled = false;
                        }
                    }
                }

                for (int i = 0; i < buildingN1_UpgradeReqUI.Length; i++)
                {
                    if (buildingSpriteCollection[0].Length < i)
                    {
                        buildingN1_UpgradeReqUI[i].sprite = buildingSpriteCollection[0][i];
                    }
                }
                for (int i = 0; i < buildingN1_UpgradeReqUI.Length; i++)
                {
                    if (buildingSpriteCollection[1].Length < i)
                    {
                        buildingN2_UpgradeReqUI[i].sprite = buildingSpriteCollection[1][i];
                    }
                }
            }

            resource1_UpgradeReqUI.text = reqRes1ToUpgradeAtLevel[buildingLevel].ToString();
            resource2_UpgradeReqUI.text = reqRes2ToUpgradeAtLevel[buildingLevel].ToString();
            playerEnergy_UpgradeReqUI.text = (5 * (buildingLevel + 1)).ToString();

            #endregion
        }

    }

    private bool Upgraderequirements()
    {
        if (buildingLevel < maxBuildingLevel && reqTownLevelToUpgradeAtLevel[buildingLevel] <= _resourceTracking.townLevel)
        {
            if (buildingLevel < maxBuildingLevel && reqBuilding1LevelToUpgradeAtLevel[buildingLevel] <= _buildingLevels[0])
            {
                if (buildingLevel < maxBuildingLevel && reqBuilding2LevelToUpgradeAtLevel[buildingLevel] <= _buildingLevels[1])
                {
                    if (buildingLevel < maxBuildingLevel && reqBuilding3LevelToUpgradeAtLevel[buildingLevel] <= _buildingLevels[2])
                    {
                        if (buildingLevel < maxBuildingLevel && reqRes1ToUpgradeAtLevel[buildingLevel] <= _resourceTracking.curResources[1])
                        {
                            if (buildingLevel < maxBuildingLevel && reqRes2ToUpgradeAtLevel[buildingLevel] <= _resourceTracking.curResources[2])
                            {
                                if (_resourceTracking.playerEnergyCurrent >= 5 * (buildingLevel + 1))
                                {
                                    return true;
                                }
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
        if (visibleUI)
        {
            _resourceTracking.CollectResources(buildingAndResourceType, Mathf.FloorToInt(curResourceAmount));
            curResourceAmount = 0;
        }
    }

    public void CollectByWorker(int collectAmount)
    {
        _resourceTracking.CollectResources(buildingAndResourceType, collectAmount);
        curResourceAmount -= collectAmount;
    }

    public void HireWorkerPressed()
    {
        if (curWorkers < workerMaxAtLevel[buildingLevel] && _resourceTracking.avaliableWorkers > 0 && visibleUI)
        {
            curWorkers++;
            _resourceTracking.BorrowWorker();
        }
    }

    public void FireWorkerPressed()
    {
        if (curWorkers > 0 && visibleUI)
        {
            curWorkers--;
            _resourceTracking.ReturnWorker();
        }
    }

    public void UpgradePressed()
    {
        if (Upgraderequirements() && visibleUI)
        {
            _resourceTracking.ExpendPlayerEnergy(5 * (buildingLevel + 1));

            _resourceTracking.CollectResources(1, -reqRes1ToUpgradeAtLevel[buildingLevel]);
            _resourceTracking.CollectResources(2, -reqRes2ToUpgradeAtLevel[buildingLevel]);

            buildingLevel++;
            buildingDisp[buildingLevel].gameObject.SetActive(true);

            if (buildingAndResourceType == 0 && buildingLevel == 2)
            {
                _resourceTracking.UpgradeTownLevel();
            }
        }
        
    }

    public void ExitPressed()
    {
        if (visibleUI)
        {
            visibleUI = false;
            HireWorker.onClick.RemoveListener(HireWorkerPressed);
            FireWorker.onClick.RemoveListener(FireWorkerPressed);
            Upgrade.onClick.RemoveListener(UpgradePressed);
            Exit.onClick.RemoveListener(ExitPressed);
        }
        
    }

    public void EnterBuildingPressed()
    {
        visibleUI = true;
    }

    public void InsertResourceTracker(H_ResourceTracking resTrackScr)
    {
        _resourceTracking = resTrackScr;
    }

    public void BuildingLevelsInformant(int[] info)
    {
        _buildingLevels = new int[info.Length];
        for (int i = 0; i < _buildingLevels.Length; i++)
        {
            _buildingLevels[i] = info[i];
        }
    }

    public void Save()
    {
        PlayerPrefs.SetInt("building " + buildingAndResourceType.ToString() + "buildingLevel", buildingLevel);
        PlayerPrefs.SetFloat("building " + buildingAndResourceType.ToString() + " curResourceAmount", curResourceAmount);
        PlayerPrefs.SetInt("building " + buildingAndResourceType.ToString() + " curworkers", curWorkers);
        PlayerPrefs.SetString("building " + buildingAndResourceType.ToString() + " quit at", System.DateTime.Now.ToString());
    }

    public void Restore()
    {
        int buildingLvl = PlayerPrefs.GetInt("building " + buildingAndResourceType.ToString() + "buildingLevel");
        float resourcesToRestore = PlayerPrefs.GetFloat("building " + buildingAndResourceType.ToString() + " curResourceAmount");
        int workersToRestore = PlayerPrefs.GetInt("building " + buildingAndResourceType.ToString() + " curworkers");

        buildingLevel = buildingLvl;
        curResourceAmount = resourcesToRestore;
        curWorkers = workersToRestore;

        if (buildingLevel > 0)
        {
            buildingDisp[0].gameObject.SetActive(false);
            for (int i = 1; i <= buildingLevel; i++)
            {
                buildingDisp[i].gameObject.SetActive(true);
            }
        }
        Debug.Log("attempted to restore building information");
    }

    public void RestoreProductionTime(int passedTime)
    {
        _restorableResources = Mathf.FloorToInt(resourceGenVar * curWorkers * passedTime);

        if (curResourceAmount < resourceMaxAtLevel[buildingLevel])
        {
            for (int i = 0; i < _restorableResources; i++)
            {
                if (curResourceAmount < resourceMaxAtLevel[buildingLevel] && _restorableResources > 0)
                {
                    curResourceAmount+=1;
                    _restorableResources--;
                }
            }
        }
    }

    public void CollectFromOfflineTime(int resourceAmount)
    {
        _resourceTracking.CollectResources(buildingAndResourceType, resourceAmount);
        _restorableResources -= resourceAmount;
    }
}
