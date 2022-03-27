using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using BayatGames.SaveGameFree;

namespace ComposingShape
{
    public class GameManager : MonoBehaviour
    {
        public LevelSO level;
        public Image medalImg;
        public SaveLoadSO saveLoad;
        public int maxStateNumber;
        public int currentStateNumber;
        public TMP_Text stateText;

        public GameObject shelf;
        public UnityEvent startEvent;
        public UnityEvent finishEvent;



        private void Awake()
        {
            Input.multiTouchEnabled = false;
            Application.targetFrameRate = 100;

            UpdateStateNum();



        }



        public void UpdateStateNum()
        {
            currentStateNumber++;
            stateText.text = currentStateNumber.ToString() + "/" + maxStateNumber.ToString();

        }


        public void FinishGame()
        {

            finishEvent.Invoke();

        }

        public void SaveAndLoadEvent()
        {
            SaveGame.Save<string>(saveLoad.gameName + saveLoad.levels[level.level - 1], medalImg.sprite.name.ToString());
        }


    }

}

