using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PoyezdPlus
{
    public class NonMono 
    {


        public static void GenerateRandomQuestionPlus(ref int first, ref int sec)
        {
            int a = Random.Range(6, 9);
            first = Random.Range(10, 80);
            first = (first / 10) * 10;
            first += a;
            int firstDigit = first / 10;
            int secondDigit = first % 10;

            sec = Random.Range(1, (9 - firstDigit));
            sec = sec * 10;

            a = Random.Range(a, 9);
            sec = sec + a;
        }

        public static void GenerateRandomQuestionMinus(ref int first, ref int sec)
        {
            first = Random.Range(20, 90);
            first = (first / 10) * 10;
            first = first + Random.Range(1, 4);

            int firstDigit = first / 10;
            int secondDigit = first % 10;
            sec = Random.Range(1, firstDigit);
            sec = (sec * 10) + Random.Range(5, 9);
        }

    }

}

