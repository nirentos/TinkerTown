using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class scoreTracker : MonoBehaviour
{
    public int part1, part2, part3, total;
    int reward, type;
    public bool woodforking;
    public GameObject UI, resourceGain;
    
    [Header("On Stars")]
    public RawImage star1, star2, star3;

    [Header("Off Stars")]
    public RawImage offStar2, offStar3;

    [Header("UI Text")]
    public string rewardName;
    public TMP_Text rewardText;

    public int scrappReward, woodReward;
    int rewardType;
    private void Start()
    {
        if (woodforking)
        {
            rewardType = woodReward;
            type = 1;
        }
        else
        {
            rewardType = scrappReward;
            type = 2;
        }
    }
    public void endScore()
    {
        total = part1 + part2 + part3;
        switch (total)
        {
            case >= 9:
                ThreeStar(3);
                break;
            case >= 5:
                TwoStar(2);
                break;
            default:
                OneStar(1);
                break;
        }

    }
    void OneStar(int stars)
    {
        reward = ((rewardType + Random.Range(-2, 3)) * stars);
        UI.SetActive(true);
        star1.enabled = true;
        offStar2.enabled = true;
        offStar3.enabled = true;
        rewardText.text = "Reward: " + reward + " " + rewardName;
    }
    void TwoStar(int stars)
    {
        reward = ((rewardType + Random.Range(-2, 3)) * stars);
        UI.SetActive(true);
        star1.enabled = true;
        star2.enabled = true;
        offStar3.enabled = true;
        rewardText.text = "Reward: " + reward + " " + rewardName;
    }
    void ThreeStar(int stars)
    {
        reward = ((rewardType + Random.Range(-2, 3)) * stars);
        UI.SetActive(true);
        star1.enabled = true;
        star2.enabled = true;
        star3.enabled = true;
        rewardText.text = "Reward: " + reward + " " + rewardName;
    }
    private void OnDestroy()
    {
        GameObject objToSpawn = Instantiate(resourceGain);
        objToSpawn.GetComponent<resourceGain>().spawn(reward, type);

    }

    public void SceneChange(string sceneToLoad)
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
