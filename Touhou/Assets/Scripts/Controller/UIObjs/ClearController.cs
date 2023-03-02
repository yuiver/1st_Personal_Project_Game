using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClearController : MonoBehaviour
{
    [SerializeField]
    TMP_Text clear;
    [SerializeField]
    TMP_Text point;
    [SerializeField]
    TMP_Text graze;
    [SerializeField]
    TMP_Text time;
    [SerializeField]
    TMP_Text rank;
    [SerializeField]
    TMP_Text total;
    private int clearScore = default;
    private int totalScore = default;
    private int clearRank = default;
    
    // Start is called before the first frame update

    // Update is called once per frame
    private void OnEnable()
    {
        ClearLevel(LevelSelectScene.level);
        ClearRank(LevelSelectScene.level);
        totalScore = ((clearScore + (PlayerController.pointItem) + (PlayerController.graze * 10000) + (PlayerController.time)) * clearRank) + PlayerController.score;

        clear.text = $"{clearScore}";
        point.text = $"{PlayerController.pointItem}";
        graze.text = $"{PlayerController.graze}";
        time.text = $"{PlayerController.time}";
        rank.text = $"{clearRank}";
        total.text = $"{totalScore}";
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Util.QuitThisGame();
        }
    }

    private void ClearLevel(int level)
    {
        switch (level)
        {
            case 1:
                clearScore = 100000;
                break;
            case 2:
                clearScore = 1000000;
                break;
            case 3:
                clearScore = 10000000;
                break;
            case 4:
                clearScore = 66666666;
                break;
            default:
                break;
        }
    }
    private void ClearRank(int level)
    {
        switch (level)
        {
            case 1:
                clearRank = 1;
                break;
            case 2:
                clearRank = 2;
                break;
            case 3:
                clearRank = 3;
                break;
            case 4:
                clearRank = 4;
                break;
            default:
                break;
        }
    }
}
