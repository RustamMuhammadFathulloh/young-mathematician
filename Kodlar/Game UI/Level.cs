using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BayatGames.SaveGameFree;

public class Level : MonoBehaviour
{
    public int level;
    public LevelSO levelSO;
    public RewardSO medal;
    public SaveLoadSO saveLoad;
    

    string medalStr;
    Image rewardImg;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();       
        button.onClick.AddListener(TaskOnClick);
        medalStr = "";
        rewardImg = transform.Find("Reward").GetComponent<Image>();
        if (SaveGame.Exists(saveLoad.gameName + saveLoad.levels[level - 1]))
        {
            medalStr = SaveGame.Load<string>(saveLoad.gameName + saveLoad.levels[level - 1]);
            UpdateMedal();
        }

    }

    public void TaskOnClick()
    {
        levelSO.level = level;
    }

    void UpdateMedal()
    {
        if (medalStr.Equals("Gold"))
        {
            rewardImg.sprite = medal.gold;

        }
        else if (medalStr.Equals("Silver"))
        {
            rewardImg.sprite = medal.silver;
        }
        else if (medalStr.Equals("Bronze"))
        {
            rewardImg.sprite = medal.bronze;
        }
    }

}
