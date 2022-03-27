using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace PoyezdTenglama
{
    public class QuestionManager : MonoBehaviour
    {
        public TMP_Text questionText;
        public GameManager gm;
        public LevelSO levelSO;
        public int level;

        public List<string> tenglamaNote;
        
        public UnityEvent questionEvent;        

        public int a;
        public int b;
        public int result;
        //string operation;

        public int randomSon;
        int chegara;

        private void Awake()
        {
            level = levelSO.level;

            if (levelSO.level == 1)
                chegara = 25;
            else if (levelSO.level == 2)
                chegara = 35;
            else if (levelSO.level == 3)
                chegara = 45;
        }


        private void Start()
        {
            StartGame();            
            Debug.Log("chegara = " + chegara);
        }



        public void StartGame()     
        {
            if (gm.currentStateNumber < gm.maxStateNumber)
            {
                gm.UpdateStateNum();                
                MakeAnsandQues();
            }
            else            {
                gm.FinishGame();
            }
        }
                

        /// <summary>
        /// Savollarni tartib bilan tanlab tuzuvchi method.
        /// </summary>
        public void MakeAnsandQues()
        {
            questionEvent.Invoke();
            int tasodifiyAmal = Random.Range(0, 4);
            
            switch (tasodifiyAmal)
            {
                case 0:
                    MakePlus();
                    break;
                case 1:
                    MakeMinus();
                    break;
                case 2:
                    MakeMultiply();
                    break;
                case 3:
                    MakeDivision();
                    break;
                default:
                    break;
            }

        }


        public void MakePlus()
        {
            int qa, qb, qS;
            qa = Random.Range(1, chegara);
            qb = Random.Range(1, chegara);
            qS = qa + qb;

            a = qa;
            b = qS;
            result = qb;
            //Debug.Log(" ++ ");
            string savolmatn1 = tenglamaNote[0].Replace("qx", a.ToString());
            string savolmatn2 = savolmatn1.Replace("qy", "y");
            string savolmatn3 = savolmatn2.Replace("qR", b.ToString());
            questionText.text = savolmatn3;
        }


        public void MakeMinus()
        {
            int qa, qb, qS;
            qa = Random.Range(1, chegara);
            qb = Random.Range(1, chegara);
            qS = qa + qb;

            result = qS;
            a = qa;
            b = qb;
            //Debug.Log(" -- ");
            string savolmatn1 = tenglamaNote[1].Replace("ax", "y");
            string savolmatn2 = savolmatn1.Replace("ay", b.ToString());
            string savolmatn3 = savolmatn2.Replace("aR", a.ToString());
            questionText.text = savolmatn3;
        }
        


        public void MakeMultiply()
        {
            int qa, qb, qS;
            qa = Random.Range(1, 10);
            qb = Random.Range(1, 10);
            qS = qa * qb;

            result = qa;
            a = qS;
            b = qb;
            
            string savolmatn1 = tenglamaNote[2].Replace("kx", "y");
            string savolmatn2 = savolmatn1.Replace("ky", b.ToString());
            string savolmatn3 = savolmatn2.Replace("kR", a.ToString());
            questionText.text = savolmatn3;
        }



        public void MakeDivision()
        {
            int qa, qb, qS;
            qa = Random.Range(1, chegara / 5 + 1);
            qb = Random.Range(1, chegara / 5 + 1);
            qS = qa * qb;

            result = qS;
            a = qa;
            b = qb;
            
            string savolmatn1 = tenglamaNote[3].Replace("bx", "y");
            string savolmatn2 = savolmatn1.Replace("by", b.ToString());
            string savolmatn3 = savolmatn2.Replace("bR", a.ToString());
            questionText.text = savolmatn3;
        }


    }
}

