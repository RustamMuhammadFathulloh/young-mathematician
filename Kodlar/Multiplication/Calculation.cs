using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Multiplication
{
    public class Calculation : MonoBehaviour
    {
        public GameManager gm;

        public List<int> clickedNumbers = new List<int>();                                     
        public List<GameObject> clickedObjects = new List<GameObject>();
        public Board board;      
        /// <summary>
        /// Event yordamida biz refrence (manba) olmay ishlay olamiz.
        /// </summary>
        public UnityEvent correctEvent;
        public UnityEvent destroyEvent;
        public UnityEvent wrongEvent;
        public UnityEvent finishEvent;



        /// <summary>
        /// Click qilingan square larni tekshirish.
        /// </summary>
        public void CheckClickedNumbers()
        {
            if (clickedNumbers.Count.Equals(2))            {
                CheckTwo();
                
            }
            else if (clickedNumbers.Count == 3)
            {
                CheckThree();
                gm.BoxColliderEnable(false);
            }
        }



        /// <summary>
        /// Ikta click uchun tekshirish
        /// </summary>
        void CheckTwo()
        {
            int result = clickedNumbers[0] * clickedNumbers[1];
            
            if (result == Board.taskValue)
            {
                gm.BoxColliderEnable(false);
                StartCoroutine(CorrectAns());
            }            
            else if (clickedNumbers[0] * clickedNumbers[1] < Board.taskValue)
            {

            }
            else
            {
                gm.BoxColliderEnable(false);
                StartCoroutine(WrongAns());
            }
        }



        void CheckThree()
        {
            int result = clickedNumbers[0] * clickedNumbers[1] * clickedNumbers[2];

            if (result == Board.taskValue)
            {
                gm.BoxColliderEnable(false);
                StartCoroutine(CorrectAns());
            }
            else if (result != Board.taskValue)
            {                            
                StartCoroutine(WrongAns());        
            }
        }



        /// <summary>
        /// Tog'ri javob uchun.
        /// </summary>
        /// <returns></returns>
        IEnumerator CorrectAns()
        {
            yield return new WaitForSeconds(0.3f);
            correctEvent.Invoke();
            foreach (GameObject obj in clickedObjects)
            {
                obj.GetComponent<Square>().CorrectAnimation();
            }
            yield return new WaitForSeconds(0.4f);
            
            
            destroyEvent.Invoke();
            clickedNumbers.Clear();
            clickedObjects.Clear();
            yield return new WaitForSeconds(0.2f);
            gm.BoxColliderEnable(true);
            gm.InitialSprite();
        }



        IEnumerator WrongAns()
        {
            wrongEvent.Invoke();

            foreach (GameObject obj in clickedObjects)
            {
                obj.GetComponent<Square>().ReturnSprite();
                obj.GetComponent<BoxCollider2D>().enabled = true;
            }

            yield return new WaitForSeconds(0.4f);
            clickedNumbers.Clear();
            clickedObjects.Clear();
            
            yield return new WaitForSeconds(0.4f);
            gm.BoxColliderEnable(true);
            gm.InitialSprite();
        }



        /// <summary>
        /// finishEvent ni chaqirish uchun ishlatiladi.
        /// </summary>
        public void FinishSound()
        {
            finishEvent.Invoke();
        }


    }
}
