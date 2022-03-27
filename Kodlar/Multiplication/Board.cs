using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ActionManager;
using UnityEngine.Events;

namespace Multiplication
{
    public class Board : MonoBehaviour
    {

        /// <summary>
        /// Tablodagi ko'paytmaning son qiymati.
        /// </summary>
        public static int taskValue;
        public GameObject objRotate;

        public TMP_Text boardText;
        public GameManager gameManager;
        public Calculation calculate;
        public float durationAnim;
        public List<int> numbersForTask;

        

        /// <summary>
        /// Tablodagi ko'paytmani chiqaruvchi kod.
        /// </summary>
        public void MultiplyTask()
        {            
            int val1, val2;
            
            numbersForTask = new List<int>(gameManager.numbers);

            if (numbersForTask.Count > 1)
            {                
                int randomIndex = Random.Range(0, numbersForTask.Count);
                val1 = numbersForTask[randomIndex];
                numbersForTask.Remove(val1);
                gameManager.sonVal1 = val1;//qiymatni uzatadi
                gameManager.sonIndex1 = randomIndex;
                Debug.Log("index = " + randomIndex + " val1 = " + val1);      //

                randomIndex = Random.Range(0, numbersForTask.Count);
                val2 = numbersForTask[randomIndex];
                numbersForTask.Remove(val2);
                gameManager.sonVal2 = val2; // qiymatni uztadi
                gameManager.sonIndex2 = randomIndex;
                Debug.Log("index = " + randomIndex + " val1 = " + val2);

                
                taskValue = val1 * val2;              
                StartCoroutine(Rotate());
            }
            else
            {
                
                calculate.FinishSound();
            }            
        }


        /// <summary>
        /// Board qismidagi son uchun animatsiya.
        /// </summary>
        /// <returns></returns>
        IEnumerator Rotate()
        {
            StartCoroutine(Actions.RotateOverSecondsInX(objRotate, 90, durationAnim));
            yield return new WaitForSeconds(durationAnim * 1.1f);
            boardText.text = taskValue.ToString();
            StartCoroutine(Actions.RotateOverSecondsInX(objRotate, 0, durationAnim));
        }



    }

}
