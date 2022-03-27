using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace FruitSplat
{
    public class QuestionCreator : MonoBehaviour
    {
        public GameEventSO questionMakeEvent;
        [SerializeField]
        private TMP_Text firstQuestionText;
        [SerializeField]
        private TMP_Text secondQuestionText;
        public GameManager gm;


        int firstLeft;
        int firstRight;
        int firstResult;
        string firstOperation;

        int secondLeft;
        int secondRight;
        int secondResult;
        string secondOperation;

        void GenerateRandomOperation(ref int numberA, ref int numberB, ref  int result, ref string operationVal)
        {
            string operation = NonMono.GetRandomOperator(gm.isPlus, gm.isMinus, gm.isMultiply, gm.isDivide);
            operationVal = operation;
            switch (operation)
            {
                case "+":
                    if (!gm.isOverTen)
                    {
                        NonMono.GenerateRandomQuestionPlus(ref numberA, ref numberB);
                    }
                    else
                    {
                        NonMono.GenerateRandomQuestionPlus(ref numberA, ref numberB, true);
                    }                    
                    result = numberA + numberB;                    
                    break;
                case "-":
                    if (!gm.isOverTen)
                    {
                        NonMono.GenerateRandomQuestionMinus(ref numberA, ref numberB);
                    }
                    else
                    {
                        NonMono.GenerateRandomQuestionMinus(ref numberA, ref numberB, true);
                    }                                   
                    result = numberA - numberB;                  
                    break;
                case "*":

                    break;
                case "/":

                    break;
            }
        }

        void MakeQuestion()
        {
            if (gm.currentStateNumber < 9)
            {
                gm.UpdateStateNum();
                GenerateRandomOperation(ref firstLeft, ref firstRight, ref firstResult, ref firstOperation);
                GenerateRandomOperation(ref secondLeft, ref secondRight, ref secondResult, ref secondOperation);
                questionMakeEvent.Raise();
                UpdateQuestionText();
                SetCorrectText();
            }
            else
            {
                gm.FinishGame();
            }           
        }

        public void MoveToNext()
        {
            Invoke("MakeQuestion", 0.75f);
        }

        void UpdateQuestionText()
        {
            firstQuestionText.text = firstLeft.ToString() + firstOperation + firstRight.ToString();
            secondQuestionText.text = secondLeft.ToString() + secondOperation + secondRight.ToString();
            firstQuestionText.GetComponent<QuestionText>().AnimateText();
            secondQuestionText.GetComponent<QuestionText>().AnimateText();

        }

        void Start()
        {
            MakeQuestion();
        }

        void SetCorrectText()
        {
            if (firstResult > secondResult)
            {
                gm.correctText = "More";
            }
            else if (firstResult < secondResult)
            {
                gm.correctText = "Less";
            }
            else
            {
                gm.correctText = "Equal";
            }
            gm.FindCorrectFruit();
        }       
    }
}

