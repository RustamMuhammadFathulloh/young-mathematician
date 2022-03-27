using ActionManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Multiplication
{
    public class Square : MonoBehaviour, IPointerClickHandler
    {
        public GameEventSO clickEventSO;
        public Calculation calculation;        
        public int squareNumber;

        [HideInInspector]
        public Vector3 initialScale;

        public float maximizeDuration;
        public Sprite greenSprite;
        public Sprite blueSprite;


        private void Awake()
        {
            initialScale = transform.localScale;
            transform.localScale = new Vector3(0, 0, 0);
            blueSprite = GetComponent<SpriteRenderer>().sprite;            
        }
               


        /// <summary>
        /// O'yin boshlanishida obyektni kattalashtiradi.
        /// </summary>
        public void MaximazeSquare()
        {
            StartCoroutine(Actions.ScaleOverSeconds(gameObject, initialScale, maximizeDuration));
        }



        /// <summary>
        /// Clickni detekt qiladi.
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            GetComponent<SpriteRenderer>().sprite = greenSprite;
            clickEventSO.Raise(); // Click event ishlasin degani
            GetComponent<BoxCollider2D>().enabled = false;
            //GetComponent<SpriteRenderer>().sprite = greenSprite;
            GetNumbers();
        }



        /// <summary>
        /// Agar click qilishda xato ketgan bo'lsa obyektga birinchi sprite ni qaytaradi.
        /// </summary>
        public void ReturnSprite()
        {
            GetComponent<SpriteRenderer>().sprite = blueSprite;            
        }



        /// <summary>
        /// click qilinganlar soni.
        /// </summary>
        void GetNumbers()
        {
            calculation.clickedNumbers.Add(squareNumber);
            calculation.clickedObjects.Add(gameObject);
            
            calculation.CheckClickedNumbers();
        }



        public void CorrectAnimation()
        {
            StartCoroutine(AnimateSquare());
        }



        /// <summary>
        /// Agar click qilingan obyektlar ko'paytmasi Task valuega teng bo'lsa. Ushbu animatsiya ishga tushadi.
        /// </summary>
        /// <returns></returns>
        IEnumerator AnimateSquare()
        {
            Vector3 scale = transform.localScale;
            StartCoroutine(Actions.ScaleOverSeconds(gameObject, initialScale * 0.7f, 0.15f));
           
            //yield return new WaitForSeconds(0.2f);
            //StartCoroutine(Actions.ScaleOverSeconds(gameObject, initialScale * 1.1f, 0.2f));
            yield return new WaitForSeconds(0.15f);
            StartCoroutine(Actions.ScaleOverSeconds(gameObject, initialScale , 0.15f));
            yield return new WaitForSeconds(0.15f);
        }

    }
}

