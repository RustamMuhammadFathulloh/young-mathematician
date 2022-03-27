using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;

public class Reward : MonoBehaviour
{
    public RewardSO reward;
    Image rewardImg;
    public int levelNumber;
    public SaveLoadSO saveLoad;
    string medalStr;

    private void Awake()
    {
        medalStr = "";
        rewardImg = transform.Find("Reward").GetComponent<Image>();        
        if (SaveGame.Exists(saveLoad.gameName + saveLoad.levels[levelNumber - 1]))
        {           
            medalStr = SaveGame.Load<string>(saveLoad.gameName + saveLoad.levels[levelNumber - 1]);
            UpdateMedal();            
        }        
        
    }

    void UpdateMedal()
    {
        if (medalStr.Equals("Gold"))
        {
            rewardImg.sprite = reward.gold;

        }
        else if (medalStr.Equals("Silver"))
        {
            rewardImg.sprite = reward.silver;
        }
        else if (medalStr.Equals("Bronze"))
        {
            rewardImg.sprite = reward.bronze;
        }
    }

    
}
