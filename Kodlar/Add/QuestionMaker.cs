using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionManager;
using TMPro;
using UnityEngine.Events;
using System.Linq;

namespace Add
{
    public class QuestionMaker : MonoBehaviour
    {
        public List<int> numbers = new List<int>();

        public List<Sprite> numberSprites;
        public GameManager gm;
        public GameObject squareParent;
        public TMP_Text questionNumberText;
        public List<GameObject> squares;        
        public UnityEvent startEvent;
        public int unitNumber;
        public int questionNumber;

        private void Start()
        {
            StartGameState();            
        }


        void StartGameState()
        {
            if (gm.currentStateNumber < gm.maxStateNumber)
            {                
                MakeState();
            }
            else
            {               
                gm.FinishGame();
            }
        }


        void MakeState()
        {           
            gm.UpdateStateNum();
            questionNumber = Random.Range(50, 99);
            questionNumberText.text = questionNumber.ToString();
            numbers = NonMono.RandomList(unitNumber, questionNumber);            
            foreach (GameObject obj in squares)
            {
                obj.GetComponent<Square>().squareMovementObj.AnimateSquare(0);
                obj.GetComponent<Square>().gm = gm;
                obj.GetComponent<Square>().squareMovementObj.gm = gm;
                obj.GetComponent<Square>().squareMovementObj.SetMoveBorder();
            }
            List<int> indexGroup = NonMono.MakeRandomIndexGroup(unitNumber, squares.Count);

            for (int i = 0; i < numbers.Count; i++)
            {
                squares[indexGroup[i]].GetComponent<Square>().squareNumberObj.GiveSpriteNumber(numbers[i], numberSprites[numbers[i] / 10], numberSprites[numbers[i] % 10]);
                squares[indexGroup[i]].GetComponent<Square>().isCorrect = true;
            }

            foreach (GameObject obj in squares)
            {
                if (!obj.GetComponent<Square>().isCorrect)
                {
                    GetWrong(numbers, obj);                    
                }
            }
            startEvent.Invoke();          
        }



        void GetWrong(List<int> numbers, GameObject obj)
        {
            int random = Random.Range(10, 50);           
            if (numbers.Contains(random))
            {
                GetWrong(numbers, obj);
            }
            else
            {
                obj.GetComponent<Square>().squareNumberObj.GiveSpriteNumber(random, numberSprites[random / 10], numberSprites[random % 10]);              
            }                    
        }

      

        void InitialCondition()
        {
            gm.addedIntegers.Clear();
            numbers.Clear();
            squares.Clear();
            gm.canvasNumber.InitialCOndition();
            //foreach (GameObject obj in squares)
            //{
            //    obj.GetComponent<Square>().InitialCondition();
            //}
        }

        public void PassNext()
        {
            InitialCondition();
            gm.MakeGrid();
            StartGameState();
        }

    }

}

