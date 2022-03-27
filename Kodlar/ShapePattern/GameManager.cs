using BayatGames.SaveGameFree;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ShapePattern
{
    public class GameManager : MonoBehaviour
    {
        public MedalSlider medalSlider;        
        public LevelSO level;        
        public Image medalImg;
        public SaveLoadSO saveLoad;
        public TMP_Text stateText;        
        
        public int maxStateNumber;
        public int currentStateNumber;

        public UnityEvent finishEvent;
      


        private void Awake()
        {
            Input.multiTouchEnabled = false;
            Application.targetFrameRate = 100;
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
            SaveGame.Save<string>(saveLoad.gameName + saveLoad.levels[level.level - 1], medalImg.sprite.name.ToString());
        }



        private void Start()
        {
            
            StartCoroutine(FingerCursorAnim());
        }



        public GameObject fingerCursor;
        public GameObject board4;
        public GameObject box;

        public GameObject naqsh;
        public GameObject bushQuti;
        public string bushQutiSpriteName;

        /// <summary>
        /// FingerCursor ning animatsiyasi.
        /// </summary>
        /// <returns></returns>
        public IEnumerator FingerCursorAnim()
        {            
            Vector3 initialPosCursor = fingerCursor.transform.position;
            float vaqt = 0.8f;
            yield return new WaitForSeconds(2f);

            FindNaqshObj();

            fingerCursor.transform.DOMove(naqsh.transform.position, vaqt);
            yield return new WaitForSeconds(vaqt);
            fingerCursor.transform.DOScale(0.8f, vaqt);
            yield return new WaitForSeconds(vaqt);

            fingerCursor.transform.DOMove(bushQuti.transform.position, vaqt + 0.4f);
            yield return new WaitForSeconds(vaqt + 0.4f);
            fingerCursor.transform.DOScale(1f, vaqt-0.2f);
            yield return new WaitForSeconds(vaqt-0.2f);

            fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), vaqt);
            yield return new WaitForSeconds(1.5f);
            fingerCursor.transform.DOMove(initialPosCursor, 0);
            fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.8f);
        }


        
        public void FindNaqshObj()
        {
            for (int i = 0; i < box.transform.childCount; i++)
            {
                Sprite obyekt = box.transform.GetChild(i).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                if (obyekt == null)
                {
                    bushQuti = box.transform.GetChild(i).gameObject;
                    bushQutiSpriteName = box.transform.GetChild(i).GetComponent<Box>().shapeName;
                    Debug.Log(box.transform.GetChild(i).GetComponent<Box>().shapeName);
                    break;
                }                
            }

            Debug.Log("     ---- ---- ---- ----  ");
            for (int i = 0; i < board4.transform.childCount; i++)
            {
                string spriteName = null;
                if (board4.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite != null)
                {
                    if (bushQutiSpriteName == board4.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite.name)
                    {
                        naqsh = board4.transform.GetChild(i).gameObject;
                        spriteName = board4.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite.name;
                        Debug.Log(spriteName);
                        break;
                    }
                    
                }                         
            }
            
        }



    }

}

