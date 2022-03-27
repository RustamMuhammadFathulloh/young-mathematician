using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ActionManager;
using UnityEngine.Events;
using UnityEngine.UI;
using BayatGames.SaveGameFree;

namespace Add
{
    public class GameManager : MonoBehaviour
    {
        public List<int> addedIntegers;
        public Image medalImg;
        public SaveLoadSO saveLoad;       
        public Color green;
        public Color red;
        public CanvasNumbers canvasNumber;
        public GameEventSO moveEvent;
        public float moveSpeed;
        public int maxStateNumber;
        public int currentStateNumber;
        public TMP_Text stateText;
        public QuestionMaker questionMaker;
        public LevelSO level;

       
        public GameObject threeByTwo;
        public GameObject twoByTwo;

        public GameObject grid;
        public float offset;
        public float borderX;
        public float borderY;

        public UnityEvent correctAction;
        public UnityEvent wrongAction;
        public UnityEvent finishEvent;
        public UnityEvent gameOverEvent;


        private void Awake()
        {
            Input.multiTouchEnabled = false;
            Application.targetFrameRate = 100;
            MakeGrid();
        }

        private void Start()
        {
            
        }


        public void MakeGrid()
        {
            switch (level.level)
            {
                case 1:
                    grid = Instantiate(twoByTwo, twoByTwo.transform.position, Quaternion.identity);
                    questionMaker.unitNumber = 2;
                    canvasNumber.ActivateCanvasNumbers(2);
                    break;
                case 2:
                    grid = Instantiate(twoByTwo, twoByTwo.transform.position, Quaternion.identity);
                    questionMaker.unitNumber = 2;
                    canvasNumber.ActivateCanvasNumbers(2);
                    break;
                case 3:
                    grid = Instantiate(threeByTwo, threeByTwo.transform.position, Quaternion.identity);
                    questionMaker.unitNumber = 4;
                    canvasNumber.ActivateCanvasNumbers(4);
                    break;
                case 4:
                    grid = Instantiate(threeByTwo, threeByTwo.transform.position, Quaternion.identity);
                    questionMaker.unitNumber = 3;
                    canvasNumber.ActivateCanvasNumbers(3);
                    break;
              
            }
            questionMaker.squareParent = grid.transform.GetChild(1).gameObject;
            questionMaker.squares = Actions.ChildrenOfGameobject(questionMaker.squareParent);
            offset = questionMaker.squares[1].transform.position.x - questionMaker.squares[0].transform.position.x;
            NonMono.SetBorder(level.level, ref borderX,  ref borderY, questionMaker.squares);
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

        public void GameOver()
        {
            gameOverEvent.Invoke();
        }


        public void CorrectEvent()
        {
            StartCoroutine(Correct());
        }

        public void WrongEvent()
        {
            StartCoroutine(Wrong());
        }




        IEnumerator Wrong()
        {
            yield return new WaitForSeconds(moveSpeed);
            wrongAction.Invoke();
        }

        IEnumerator Correct()
        {
            yield return new WaitForSeconds(moveSpeed);
            correctAction.Invoke();
        }

        public void SaveAndLoadEvent()
        {
            SaveGame.Save<string>(saveLoad.gameName + saveLoad.levels[level.level - 1], medalImg.sprite.name.ToString());
        }

    }
}

