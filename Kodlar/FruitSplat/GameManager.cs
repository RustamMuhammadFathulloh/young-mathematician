using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using BayatGames.SaveGameFree;
using UnityEngine.UI;

namespace FruitSplat
{
    public class GameManager : MonoBehaviour
    {
        
        public GameObject life;
        public Image medalImg;
        public SaveLoadSO saveLoad;
        public MedalSlider medalSlider;
        public int maximumWrongChoise;
        public int wrong;
        public string correctText;
        public float fruitSpeed;
        public TMP_Text stateText;
        public FruitSpawner fruitSpawner;
        [SerializeField]
        private int maxStateNumber;    
        public int currentStateNumber;
        [SerializeField]
        private LevelSO level;

        public bool isOverTen;
        public bool isPlus;
        public bool isMinus;
        public bool isMultiply;
        public bool isDivide;

        public UnityEvent finishEvent;
        public UnityEvent gameOverEvent;
        public GameObject correctParticle;
        public GameObject wrongParticle;

        public float [] durationLevelOne;
        public float[] durationLevelTwo;
        public float[] durationLevelThree;


        private void Awake()
        {
            SetLevelCondition();

            Input.multiTouchEnabled = false;
            Application.targetFrameRate = 100;
        }

        void SetLevelCondition()
        {
            if (level.level.Equals(1))
            {
                fruitSpeed = 12;
                medalSlider.medalPeriodLimit = durationLevelOne;
            }
            else if (level.level.Equals(2))
            {
                fruitSpeed = 6;
                medalSlider.medalPeriodLimit = durationLevelTwo;
            }
            else
            {
                fruitSpeed = 3;
                medalSlider.medalPeriodLimit = durationLevelThree;
            }
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

        public void FindCorrectFruit()
        {
            foreach (GameObject obj in fruitSpawner.fruits)
            {
                if (obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name.Equals(correctText))
                {
                    obj.GetComponent<Fruit>().isCorrect = true;
                }
                else
                {
                    obj.GetComponent<Fruit>().isCorrect = false;
                }
            }           
        }

        public void CheckWrong()
        {
            if (wrong.Equals(maximumWrongChoise))
            {
                gameOverEvent.Invoke();                
            }
        }

        public void SaveAndLoadEvent()
        {
            if (!wrong.Equals(maximumWrongChoise))
            {
                SaveGame.Save<string>(saveLoad.gameName + saveLoad.levels[level.level - 1], medalImg.sprite.name.ToString());
            }
            //SaveGame.Save<string>(saveLoad.gameName + saveLoad.levels[level.level - 1], medalImg.sprite.name.ToString());
        }


    }

}

