using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class scoreTracker : MonoBehaviour
{
    public int part1, part2, part3, total;
    public bool woodforking;
    public GameObject UI;
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
        }
        else
        {
            rewardType = scrappReward;
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
        UI.SetActive(true);
        star1.enabled = true;
        offStar2.enabled = true;
        offStar3.enabled = true;
        rewardText.text = "Reward: " + ((rewardType + Random.Range(-2, 3)) * stars) + " " + rewardName;
    }
    void TwoStar(int stars)
    {
        UI.SetActive(true);
        star1.enabled = true;
        star2.enabled = true;
        offStar3.enabled = true;
        rewardText.text = "Reward: " + ((rewardType + Random.Range(-2, 3)) * stars) + " " + rewardName;
    }
    void ThreeStar(int stars)
    {
        UI.SetActive(true);
        star1.enabled = true;
        star2.enabled = true;
        star3.enabled = true;
        rewardText.text = "Reward: " + ((rewardType + Random.Range(-2, 3)) * stars) + " " + rewardName;
    }
}
