using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H_GameController : MonoBehaviour
{
    public GameObject[] buildingComp;
    public H_ResourceDisplay resourceDisplay;
    public H_ResourceTracking resourceTracking;
    private H_Building[] _buildingScr;

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
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < _buildingScr.Length; i++)
        {
            _buildingScr[i].UpdateUI();
            _buildingScr[i].GenerateResource();
        }

        resourceDisplay.UpdateTrackers();
        resourceTracking.IdleWorkers(_buildingScr);
    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
