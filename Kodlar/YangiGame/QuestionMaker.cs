using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace YangiGame
{
    public class QuestionMaker : MonoBehaviour
    {
        public GameManager gm;

        [SerializeField]
        public TMP_Text questionText;

        public int questionNumber;


        /// <summary>
        /// Savol tablo uchun son tanlaydigan method.
        /// </summary>
        public void GenerateRandom2Digit()
        {
            questionNumber = GetRandom2GigitNumber(gm.level.level);
            gm.questionNumber = questionNumber;

            questionText.text = questionNumber.ToString();

        }


        public static int GetRandom2GigitNumber(int level)
        {
            int number = Random.Range(11, 99);
            if (level.Equals(2))
            {   
                if (number % 10 == 1 || number % 10 == 0)
                {
                    number = (number / 10) * 10 + Random.Range(2, 9);

                }
            }
            else
            {
                if (number % 10 == 0)
                {
                    number = (number / 10) * 10 + Random.Range(1, 9);

                }
            }
            return number;
        }
    }
}
