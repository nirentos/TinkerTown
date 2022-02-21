using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class H_ResourceDisplay : MonoBehaviour
{
    public TMP_Text[] resourceAmountCounter;

    private H_ResourceTracking _resourceTracking;

    private void Awake()
    {
        _resourceTracking = GetComponent<H_ResourceTracking>();
    }

    public void UpdateTrackers()
    {
        for (int i = 0; i < _resourceTracking.resources.Count; i++)
        {
            if (resourceAmountCounter[i] != null)
            {
                string resource = "";

                foreach (var key in _resourceTracking.resources)
                {
                    if (key.Value == i)
                    {
                        resource = key.Key;
                    }
                }

                resourceAmountCounter[i].text = _resourceTracking.resources[resource].ToString();
            }
            else
            {
                Debug.Log("Not Enough Counters");
            }
        }
    }

}
