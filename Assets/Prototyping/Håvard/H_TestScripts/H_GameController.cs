using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_GameController : MonoBehaviour
{
    public GameObject[] buildingComp;
    public H_ResourceDisplay resourceDisplay;
    public H_ResourceTracking resourceTracking;
    [HideInInspector]public H_Building[] _buildingScr;

    [HideInInspector] public int[] buildingLevels;

    public GameObject camera;
    public float maxBackgroundPan;
    public float panAmount;

    public void OnApplicationQuit()
    {
        Save();
    }

    // Start is called before the first frame update
    void Start()
    {
        _buildingScr = new H_Building[buildingComp.Length];

        for (int i = 0; i < buildingComp.Length; i++)
        {
            if (buildingComp[i] != null)
            {
                _buildingScr[i] = buildingComp[i].GetComponent<H_Building>();
                _buildingScr[i].InsertResourceTracker(resourceTracking);
            }
        }

        buildingLevels = new int[_buildingScr.Length];

        Debug.Log("buildingscr contains " + _buildingScr.Length.ToString());
        for (int i = 0; i < _buildingScr.Length; i++)
        {
            Debug.Log("buildingscr " + i.ToString() + " contains " + _buildingScr[i].gameObject.name);
        }

        Restore();
        for (int i = 0; i < _buildingScr.Length; i++)
        {
            _buildingScr[i].Restore();
        }
        resourceTracking.Restore();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _buildingScr.Length; i++)
        {
            _buildingScr[i].UpdateUI();
            _buildingScr[i].GenerateResource();
            buildingLevels[i] = _buildingScr[i].buildingLevel;
            _buildingScr[i].BuildingLevelsInformant(buildingLevels);
        }

        resourceDisplay.UpdateTrackers();
        resourceTracking.IdleWorkers(_buildingScr);

        if (camera.transform.position.x < -maxBackgroundPan)
        {
            camera.transform.position = new Vector3(-maxBackgroundPan+0.1f, camera.transform.position.y, camera.transform.position.z);
        }

        if (camera.transform.position.x > maxBackgroundPan)
        {
            camera.transform.position = new Vector3(maxBackgroundPan-0.1f, camera.transform.position.y, camera.transform.position.z); ;
        }
    }

    public void SwipeScreen(float direction)
    {
        if (direction > 0 && camera.transform.position.x < maxBackgroundPan)
        {
            for (int i = 0; i < 1; i++)
            {
                if (camera.transform.position.x < maxBackgroundPan)
                {
                    camera.transform.position = new Vector3(camera.transform.position.x - panAmount, camera.transform.position.y, camera.transform.position.z);
                }
            }
        }
        else if (direction < 0 && camera.transform.position.x > -maxBackgroundPan)
        {
            for (int i = 0; i < 1; i++)
            {
                if (camera.transform.position.x > -maxBackgroundPan)
                {
                    camera.transform.position = new Vector3(camera.transform.position.x + panAmount, camera.transform.position.y, camera.transform.position.z);
                }
            }
        }
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void Save()
    {
        PlayerPrefs.SetString("Game quit at", System.DateTime.Now.ToString());
    }

    public void Restore()
    {
        string timeOfExit = PlayerPrefs.GetString("Game quit at");

        var curDate = System.DateTime.Now;

        System.TimeSpan duration = curDate - System.DateTime.Parse(timeOfExit);

        var passedTimeInSeconds = Mathf.FloorToInt((float)duration.TotalSeconds);

        for (int i = 0; i < _buildingScr.Length; i++)
        {
            _buildingScr[i].RestoreProductionTime(passedTimeInSeconds);
        }


        resourceTracking.OfflineCollection(passedTimeInSeconds, _buildingScr);
    }
}
