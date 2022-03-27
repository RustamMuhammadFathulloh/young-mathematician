using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;
using BayatGames.SaveGameFree;

namespace PoyezdPlus
{
    public class GameManager : MonoBehaviour
    {
        public MedalSlider medalSlider;
        public GameObject lifeParent;
        public LevelSO level;
        public GameObject loseParticle;
        public Image medalImg;
        public SaveLoadSO saveLoad;
        public TMP_Text stateText;
        public bool isPlus;
        public bool isMinus;
        public List<Sprite> sprites;
        public int maxStateNumber;
        public int currentStateNumber;

        public UnityEvent finishEvent;
        public UnityEvent correctEvent;
        public UnityEvent wrongEvent;
        public UnityEvent gameOverEvent;

        public int wrongChoice;

        private void Awake()
        {
            SetTimerBasedOnLevel();


        }


        void SetTimerBasedOnLevel()
        {
            if (level.level.Equals(1))
            {
                medalSlider.medalPeriodLimit[0] = 90;
                medalSlider.medalPeriodLimit[1] = 40;
                medalSlider.medalPeriodLimit[2] = 30;
                medalSlider.medalPeriodLimit[3] = 20;
            }
            else if (level.level.Equals(2))
            {
                medalSlider.medalPeriodLimit[0] = 75;
                medalSlider.medalPeriodLimit[1] = 30;
                medalSlider.medalPeriodLimit[2] = 30;
                medalSlider.medalPeriodLimit[3] = 15;
            }
            else 
            {
                medalSlider.medalPeriodLimit[0] = 70;
                medalSlider.medalPeriodLimit[1] = 20;
                medalSlider.medalPeriodLimit[2] = 15;
                medalSlider.medalPeriodLimit[3] = 10;
            }
            
        }

        public void FinishGame()
        {
            finishEvent.Invoke();
        }

        public void UpdateStateNum()
        {

            currentStateNumber++;
            stateText.text = currentStateNumber.ToString() + "/" + maxStateNumber.ToString();
        }

        public void SaveAndLoadEvent()
        {
            if (!wrongChoice.Equals(3))
            {
                SaveGame.Save<string>(saveLoad.gameName + saveLoad.levels[level.level - 1], medalImg.sprite.name.ToString());
            }
            //SaveGame.Save<string>(saveLoad.gameName + saveLoad.levels[level.level - 1], medalImg.sprite.name.ToString());
        }


        public void MinusLife()
        {
            lifeParent.transform.GetChild(wrongChoice).GetComponent<Image>().enabled = false;
            wrongChoice++;
            if (wrongChoice.Equals(3))
            {
                StartCoroutine(GameOverInvoke());
            }
        }

        IEnumerator GameOverInvoke()
        {
            yield return new WaitForSeconds(0.7f);
            gameOverEvent.Invoke();
            
        }

    }

}

