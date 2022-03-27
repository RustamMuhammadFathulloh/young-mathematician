using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace PiecesWhole
{
    public class CalculationManager : MonoBehaviour
    {
        public int n = 16;
        public GameManager gameManager;
        public GameObject parda;
        public GameObject animObjectLeft, animObjectRight;
        public Vector3 initialPosLeft, initialPosRight;
        public List<GameObject> clickedObjects;

        public string spriteNameL, spriteNameR;
        public UnityEvent clickEvent, wrongEvent, correctEvent;
        public UnityEvent finishEvent;

        public int initialOrder;


        private void Start()
        {
            initialPosLeft = animObjectLeft.transform.position;
            initialPosRight = animObjectRight.transform.position;
        }


        /// <summary>
        /// Bosilganlarni harakatga keltiruvchi kod.
        /// </summary>
        public void MoveClicked()
        {
            if (clickedObjects.Count.Equals(2))
            {
                StartCoroutine(MoveClickAnswers());
            }           
            
        }



        /// <summary>
        /// Bosilgan squarelarni belgilangan pozitsiyaga eltadi.
        /// </summary>
        public IEnumerator MoveClickAnswers()
        {            
            initialOrder = clickedObjects[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder;
            ChangeOrderInLayer(5);
            MaximizeScaleAnimation(1.3f);
            parda.SetActive(true);
            gameManager.BoxColliderSwitching(false);

            clickedObjects[0].transform.GetChild(0).DOMove(initialPosLeft, 0.8f);
            clickedObjects[1].transform.GetChild(0).DOMove(initialPosRight, 0.8f);
            yield return new WaitForSeconds(0.8f);
            clickedObjects[0].transform.GetChild(0).DOMoveX(0, 0.2f);
            clickedObjects[1].transform.GetChild(0).DOMoveX(0, 0.2f);
            yield return new WaitForSeconds(0.4f);
            StartCoroutine(CheckAnswers());

        }


        /// <summary>
        /// Parda paydo bo'lishi bilan bosilgan square lar ichidagi spriteni kattalashtiruvchi kod.
        /// </summary>
        public void MaximizeScaleAnimation(float floatNum)
        {
            for (int i = 0; i < clickedObjects.Count; i++)
            {
                clickedObjects[i].transform.GetChild(0).DOScale(floatNum, 0.2f);
            }
        }


        /// <summary>
        /// Click qilingan obyektlarni to'g'ri yoki noto'g'ri ekanligini tekshiruvchi Method().
        /// </summary>
        public IEnumerator CheckAnswers()
        {            
            spriteNameL = clickedObjects[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
            spriteNameR = clickedObjects[1].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;

            int javob = ReadNumber(spriteNameL) + ReadNumber(spriteNameR);
            if ((javob == 100) && (spriteNameL[2] == spriteNameR[2]))
            {
                MaximizeScaleAnimation(2.0f);
                yield return new WaitForSeconds(0.6f);
                correctEvent.Invoke();                
            }
            else
            {
                wrongEvent.Invoke();
            }       
            
        }


        /// <summary>
        /// Sprite biriktirilgan gameObyektni yo'q qiluvchi kod. Bu kod CorrectEvent uchun ishlatiladi.
        /// </summary>
        public void CorrectAns()
        {
            clickedObjects[0].GetComponent<PiecesSquare>().ReturnSprite(false);
            clickedObjects[1].GetComponent<PiecesSquare>().ReturnSprite(false);
            
            Destroy(clickedObjects[0].transform.GetChild(0).gameObject);            
            Destroy(clickedObjects[1].transform.GetChild(0).gameObject);
            

            clickedObjects.Clear();
            parda.SetActive(false);

            n -= 2;
            if (n == 0)            {
                finishEvent.Invoke();
            }
        }


        /// <summary>
        /// Click qilingan obyektlarni avvalgi o'rniga qaytaradi. WrongEvent ga qo'shilgan.
        /// </summary>
        public void WrongAns()
        {
            clickedObjects[0].GetComponent<PiecesSquare>().ReturnSprite(true);
            clickedObjects[1].GetComponent<PiecesSquare>().ReturnSprite(true);
            clickedObjects[0].transform.GetChild(0).DOMove(clickedObjects[0].GetComponent<PiecesSquare>().inialPosSprite, 0.8f);
            clickedObjects[1].transform.GetChild(0).DOMove(clickedObjects[1].GetComponent<PiecesSquare>().inialPosSprite, 0.8f);
            StartCoroutine(ClearClickedList());
            
        }


        /// <summary>
        /// Return tipli method. Sprite nomidagi sonlarni o'qib beradi.
        /// </summary>
        /// <param name="name1">spritenomi</param>
        /// <returns></returns>
        public int ReadNumber(string name1)
        {
            string unlik;       //0 va 1  index dagi sonni aniqlab beradi - Substring(0, 2)
            unlik = name1.Substring(0, 2);            
            int son = Int32.Parse(unlik);
            
            return son; // Return tipli method
        }


        /// <summary>
        /// Sprite Renderer dagi order in layer variableni o'zgartirishda ishlatiladigan method.
        /// </summary>
        /// <param name="orderNumber">Order In Layerning yangi qiymati.</param>
        public void ChangeOrderInLayer(int orderNumber)
        {
            for (int i = 0; i < clickedObjects.Count; i++)
            {
                clickedObjects[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = orderNumber;
            }
        }



        /// <summary>
        /// Listni tozalab beradi.
        /// </summary>
        public IEnumerator ClearClickedList()
        {
            MaximizeScaleAnimation(1);            
            yield return new WaitForSeconds(0.6f);
            ChangeOrderInLayer(initialOrder);
            clickedObjects.Clear();
            parda.SetActive(false);
        }

        
    }

}

