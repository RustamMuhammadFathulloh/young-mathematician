using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using BayatGames.SaveGameFree;
using UnityEngine.UI;

namespace Symmetry
{
    public class GameManager : MonoBehaviour
    {
        public Image medalImg;
        public LevelSO level;
        public SimmetriiyaStateSO state;
        public int correct;
        public int wrong;
        public QuestionSpawner questionSpawner;
        public UnityEvent finishEvent;
        public SaveLoadSO saveLoad;

        private void Awake()
        {
            Input.multiTouchEnabled = false;
            Application.targetFrameRate = 100;

        }


        // Start is called before the first frame update
        void Start()
        {

        }

        public void CorrectChanges(int n)
        {
            correct += n;
            Check();
        }

        public void WrongChanges(int n)
        {
            wrong += n;
            Check();
        }

        void Check()
        {
            switch (level.level)
            {
                case 1:
                    if (correct.Equals(3) && wrong.Equals(0))
                    {
                        FinishOrMove();                                              
                    }
                    break;
                case 2:
                    if (correct.Equals(4) && wrong.Equals(0))
                    {
                        FinishOrMove();
                    }
                    break;
                case 3:
                    if (correct.Equals(5) && wrong.Equals(0))
                    {
                        FinishOrMove();
                    }
                    break;
                case 4:
                    if (correct.Equals(5) && wrong.Equals(0))
                    {
                        FinishOrMove();
                    }
                    break;
                case 5:
                    if (correct.Equals(5) && wrong.Equals(0))
                    {
                        FinishOrMove();
                    }
                    break;
                case 6:
                    if (correct.Equals(5) && wrong.Equals(0))
                    {
                        FinishOrMove();
                    }
                    break;
                case 7:
                    if (correct.Equals(5) && wrong.Equals(0))
                    {
                        FinishOrMove();
                    }
                    break;
                case 8:
                    if (correct.Equals(6) && wrong.Equals(0))
                    {
                        FinishOrMove();
                    }
                    break;
            }
        }


        void FinishOrMove()
        {
            if (state.runTimeStateNumber < 5)
            {
                questionSpawner.InitialStateChanges();
                correct = 0;
            }
            else
            {                
                Invoke("FinishLevel", 0.5f);
            }
        }

        void FinishLevel()
        {
            finishEvent.Invoke();            
            
        }

        public void SaveAndLoadEvent()
        {
            SaveGame.Save<string>(saveLoad.gameName + saveLoad.levels[level.level - 1], medalImg.sprite.name.ToString());
        }

    }

}

