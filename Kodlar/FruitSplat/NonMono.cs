using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FruitSplat
{
    public class NonMono
    {

        /// <summary>
        /// Get Plus or Minus
        /// </summary>
        /// <returns></returns>
        public static string GetRandomOperator(bool Plus, bool Minus, bool Multiply, bool Divide)
        {
            if (Plus && !Minus && !Multiply && !Divide)
            {
                return "+";
            }
            else if (Plus && Minus && !Multiply && !Divide)
            {
                int ran = Random.Range(1, 99);
                if (ran % 2 == 0)
                {
                    return "+";
                }
                else
                {
                    return "-";                    
                }
            }
            else if (Plus && Minus && Multiply && !Divide)
            {
                int ran = Random.Range(1, 3);
                if (ran.Equals(1))
                {
                    return "+";
                }
                else if (ran.Equals(2))
                {
                    return "-";
                }
                else
                {
                    return "*";
                }
            }
            else if (Plus && Minus && Multiply && Divide)
            {
                int ran = Random.Range(1, 4);
                if (ran.Equals(1))
                {
                    return "+";
                }
                else if (ran.Equals(2))
                {
                    return "-";
                }
                else if (ran.Equals(3))
                {
                    return "*";
                }
                else
                {
                    return "/";
                }
            }
            else if (!Plus && !Minus && Multiply && Divide)
            {
                int ran = Random.Range(1, 99);
                if (ran % 2 == 0)
                {
                    return "*";
                }
                else
                {
                    return "/";
                }
            }
            else if (!Plus && Minus && !Multiply && !Divide)
            {
                return "-";
            }
            else if (!Plus && !Minus && Multiply && !Divide)
            {
                return "*";
            }
            else if (!Plus && !Minus && !Multiply && Divide)
            {
                return "/";
            }
            else
            {
                return "+";                
            }
        }

        public static void GenerateRandomQuestionPlus(ref int first, ref int sec)
        {
            first = Random.Range(10, 90);            
            int firstDigit = first / 10;
            int secondDigit = first % 10;
            sec = (Random.Range(10, (9 - firstDigit) * 10)) / 10;                                   
            sec = (sec * 10) + Random.Range(0, 9 - secondDigit); 
        }

        public static void GenerateRandomQuestionPlus(ref int first, ref int sec, bool isOverTen)
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
            //if (firstDigit >= 8)
            //{
            //    sec = secondDigit - 2;
            //}
            //else
            //{
            //    sec = sec + (secondDigit - 2);                
            //}            
        }

        public static void GenerateRandomQuestionMinus(ref int first, ref int sec)
        {
            first = Random.Range(10, 90);
            int firstDigit = first / 10;
            int secondDigit = first % 10;
            sec = Random.Range(1, firstDigit);
            sec = (sec * 10) + Random.Range(0, secondDigit);
        }

        public static void GenerateRandomQuestionMinus(ref int first, ref int sec, bool isOverTen)
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

