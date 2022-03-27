using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;


namespace PoyezdPlus
{
    public class QuestionMaker : MonoBehaviour
    {
        public TMP_Text questionText;
        public GameManager gm;

        public List<string> questionSamples;
        public List<TMP_Text> savolMatnlariPilus;

        public List<string> questionSamplesMinus;
        public List<TMP_Text> savolMatnlariMinus;

        public UnityEvent questionEvent;

        public int a;
        public int b;
        public int result;
        string operation;

        private void Start()
        {
            StartGame();
        }



        public void StartGame()
        {
            if (gm.currentStateNumber < gm.maxStateNumber)
            {
                gm.UpdateStateNum();
                MakeQuestion();
            }
            else
            {
                gm.FinishGame();
            }
        }

        void MakeQuestion()
        {
            questionEvent.Invoke();
            string operationVal = FruitSplat.NonMono.GetRandomOperator(gm.isPlus, gm.isMinus, false, false);
            operation = operationVal;
            if (operation.Equals("+"))
            {
                NonMono.GenerateRandomQuestionPlus(ref a, ref b);
                result = a + b;
                //DisplayText(questionSamples);
                NewDisplayText(savolMatnlariPilus);
            }
            else
            {
                NonMono.GenerateRandomQuestionMinus(ref a, ref b);
                result = a - b;
                //DisplayText(questionSamplesMinus);
                NewDisplayText(savolMatnlariMinus);
            }            
        }

        void DisplayText(List<string> questions)
        {
            string randomQuestion = questions[Random.Range(0, questions.Count)];
            string newA = "" + a.ToString() + "";
            string newB = "" + b.ToString() + "";

            string val1 = randomQuestion.Replace("'a", newA);
            string val2 = val1.Replace("'b", newB);
            questionText.text = val2;
        }
       


        /// <summary>
        /// TMP_text 
        /// </summary>
        /// <param name="savoltext"></param>
        void NewDisplayText(List<TMP_Text> savoltext)
        {
            string satr = savoltext[Random.Range(0, savoltext.Count)].text;
            string newAA = "" + a.ToString() + "";
            string newBB = "" + b.ToString() + "";

            string satr1 = satr.Replace("'a", newAA);
            string satr2 = satr1.Replace("'b", newBB);
            questionText.text = satr2;
        }


    }

}

