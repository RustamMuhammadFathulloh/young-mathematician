using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


namespace Extension
{
    public static class Extensions 
    {       //Extension Methodlar Method boshidagi parametrlar oldiga "this" qo'yish orqali yasaladi.

        /// <summary>
        /// Shuffle all elements in the List 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> ShuffleList<T>(this List<T> list)
        {
            list = list.OrderBy(x => UnityEngine.Random.value).ToList();
            return list;
        }


        /// <summary>
        /// Get all children of the GameObject
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static List<GameObject> GetChildren(this Transform transform)
        {
            List<GameObject> list = new List<GameObject>();
            for (int i = 0; i < transform.childCount; i++)
            {
                list.Add(transform.GetChild(i).gameObject);
            }
            return list;

        }

        /// <summary>
        /// Set position on the right side of the screen
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="main"></param>
        /// <param name="spRenderer"></param>
        /// <returns></returns>
        public static void DOScreenRightX(this Transform transform, Camera main, SpriteRenderer spRenderer)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            float offset = spRenderer.sprite.bounds.size.x * 0.5f;
            pos = new Vector3(pos.x + offset, transform.position.y, 0);
            transform.position = pos;
        }

        /// <summary>
        /// Set position on the right side of the screen. RectTransform
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="canvas"></param>
        public static void DOScreenRightX(this RectTransform rectTransform, Canvas canvas)
        {
            Vector3 initialPos = rectTransform.localPosition;
            float positionPlus = canvas.GetComponent<RectTransform>().rect.width * 0.5f + rectTransform.rect.width * 0.5f;
            initialPos.x = positionPlus;
            rectTransform.anchoredPosition = initialPos;
        }

        /// <summary>
        /// Set position on the left side of the screen
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="main"></param>
        /// <param name="spRenderer"></param>
        /// <returns></returns>
        public static void DOScreenLeftX(this Transform transform, Camera main, SpriteRenderer spRenderer)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            float offset = spRenderer.sprite.bounds.size.x * 0.5f;
            pos = new Vector3((pos.x + offset) * -1, transform.position.y, 0);
            transform.position = pos;
        }

        /// <summary>
        /// Set position on the left side of the screen. RectTransform
        /// </summary>
        /// <param name="rectTransform"></param>
        /// <param name="canvas"></param>
        public static void DOScreenLeftX(this RectTransform rectTransform, Canvas canvas)
        {
            Vector3 initialPos = rectTransform.localPosition;
            float positionPlus = canvas.GetComponent<RectTransform>().rect.width * 0.5f + rectTransform.rect.width * 0.5f;
            initialPos.x = -positionPlus;
            rectTransform.anchoredPosition = initialPos;
        }


        /// <summary>
        /// Convert numbers into words in Uzbek
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToUzbekWords(this int number)
        {
            if (number == 0)
                return "nol";

            if (number < 0)
                return "manfiy " + number.ToUzbekWords();

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += (number / 1000000).ToUzbekWords() + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += (number / 1000) == 1 ? "ming " : (number / 1000).ToUzbekWords() + " ming ";
                number %= 1000;
            }
            if ((number / 100) == 1)
            {
                if (number == 100)
                    words += "yuz";
                else words += (number / 100) > 1 ? (number / 100).ToUzbekWords() + " yuz " : "yuz ";
                number %= 100;
            }
            if ((number / 100) > 1)
            {
                var hundredMap = new[] { "", "bir", "ikki", "uch", "to‘rt", "besh", "olti", "yetti", "sakkiz", "to‘qqiz" };
                if (number > 199)
                    words += hundredMap[number / 100] + "yuz ";
                else
                {
                    words += (number / 100).ToUzbekWords() + " yuz ";
                }
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += " ";

                var unitsMap = new[] { "nol", "bir", "ikki", "uch", "to‘rt", "besh", "olti", "yetti", "sakkiz", "to‘qqiz", "o‘n", "o‘n bir", "o‘n ikki", "o‘n uch", "o‘n to‘rt", "o‘n besh", "o‘n olti", "o‘n yetti", "o‘n sakkiz", "o‘n to‘qqiz", "yigirma" };
                var tensMap = new[] { "nol", "o‘n", "yigirma", "o‘ttiz", "qirq", "ellik", "oltmish", "yetmish", "sakson", "to‘qson" };

                if (number < 21)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += ((number % 10) > 2 ? "  " : " ") + unitsMap[number % 10];
                }
            }

            return words;
        }






    }

}

