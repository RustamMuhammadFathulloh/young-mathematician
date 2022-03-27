using BayatGames.SaveGameFree;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace YangiGame
{
    public class GameManager : MonoBehaviour
    {
        public Image medalImg;
        public SaveLoadSO saveLoad;
        //public MedalSlider medalSlider;

        public int maxStateNumber;
        public int currentStateNumber;
        public TMP_Text stateText;

        public int questionNumber;
        public int unlik;

        public GameObject mainBox, box2lik, box3lik;
        public GameObject correctParticle;
        public LevelSO level;
        public List<Sprite> numbers;
        public QuestionMaker question;
        public UnityEvent finishEvent;


        private void Awake()
        {
            Input.multiTouchEnabled = false;
            Application.targetFrameRate = 100;
            CheckLevel();
        }



        private void Start()
        {
            StartGame();
        }


        /// <summary>
        /// Qaysi levelda ekanligini aniqlaydi.
        /// </summary>
        public void CheckLevel()
        {
            
            if (level.level == 1)
            {
                mainBox = box2lik;
            }
            else if (level.level == 2)
            {
                mainBox = box3lik;
            }
            mainBox.SetActive(true);
        }



        public void StartGame()
        {
            if (currentStateNumber < maxStateNumber)
            {
                UpdateStateNum();
                
                question.GenerateRandom2Digit();
                unlik = 0;
            }
            else
            {
                FinishGame();
            }
        }


        /// <summary>
        /// Savol berish kerak yoki yo'qligini tekshiruvchi method.
        /// </summary>
        public void CheckQuestionNumber()
        {
            
            if (unlik.Equals(questionNumber))
            {
                
                if (level.level.Equals(1))
                {
                    mainBox.GetComponent<BoxScript>().Animation();
                }
                else
                {
                    mainBox.GetComponent<BoxScript>().Animation();
                }
            }
        }




        /// <summary>
        /// O'yinni tugatuvchi method.
        /// </summary>
        public void FinishGame()
        {
            finishEvent.Invoke();

        }


        /// <summary>
        /// StateNumberni yangilovchi mathod.
        /// </summary>
        public void UpdateStateNum()
        {
            currentStateNumber++;
            stateText.text = currentStateNumber.ToString() + "/" + maxStateNumber.ToString();
        }



        public void SaveAndLoadEvent()
        {
            SaveGame.Save<string>(saveLoad.gameName + saveLoad.levels[level.level - 1], medalImg.sprite.name.ToString());
        }

    }
}
