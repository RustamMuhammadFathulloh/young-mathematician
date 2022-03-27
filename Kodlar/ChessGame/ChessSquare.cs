using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

namespace ChessGame
{
    public class ChessSquare : MonoBehaviour, IPointerClickHandler
    {
        public ChessTablo chessTablo;
        public GameManager gm;

        public Sprite oqSprite, qoraSprite;
        public int squareIndex;
        public int level;
        public Color initialColor;
        public Vector3 initialScale;
        public int initialOrderInLayer;

        public Color yangiRang = new Color(0.8f, 0.3f, 0.3f, 1);

        void Start()
        {
            initialColor = GetComponent<SpriteRenderer>().color;
            initialScale = transform.localScale;
            initialOrderInLayer = GetComponent<SpriteRenderer>().sortingOrder;
            qoraSprite = GetComponent<SpriteRenderer>().sprite;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 7;
            CheckClickedAnswer();
        }

        private int harfRaqam;

        public void CheckClickedAnswer()
        {
            int sonSavol = chessTablo.sonSavol;
            int boardSize = chessTablo.boardSize;
            char harfSavol = chessTablo.harfSavol;            

            switch (harfSavol)
            {
                case 'A':
                    harfRaqam = 1;
                    break;
                case 'B':
                    harfRaqam = 2;
                    break;
                case 'C':
                    harfRaqam = 3;
                    break;
                case 'D':
                    harfRaqam = 4;
                    break;
                case 'E':
                    harfRaqam = 5;
                    break;
                case 'F':
                    harfRaqam = 6;
                    break;
                //case 'G':
                //    harfRaqam = 7;
                //    break;
                //case 'H':
                //    harfRaqam = 8;
                //    break;
                default:
                    break;
            }


            if (((harfRaqam - 1) * boardSize + sonSavol - 1) == squareIndex)
            {
                StartCoroutine(Painting());
                gm.correctEvent.Invoke();
            }
            if (((harfRaqam - 1) * boardSize + sonSavol - 1) != squareIndex) 
            {
                StartCoroutine(ErrorPainting());
                gm.errorEvent.Invoke();
            }
            //bool bolean1 = ((harfRaqam - 1) * boardSize + sonSavol - 1) == squareIndex;
            //bool bolean2 = ((harfRaqam - 1) * boardSize + sonSavol - 1) != squareIndex;
            
        }



        public IEnumerator Painting()
        {
            GetComponent<SpriteRenderer>().sprite = oqSprite;
            GetComponent<Transform>().DOScale(initialScale * 1.5f, 0.3f);
            GetComponent<SpriteRenderer>().color = Color.green;
            yield return new WaitForSeconds(0.3f);

            GetComponent<Transform>().DOScale(initialScale, 0.3f);            
            yield return new WaitForSeconds(0.5f);
            
            yield return new WaitForSeconds(0.3f);
            GetComponent<SpriteRenderer>().sprite = qoraSprite;
            GetComponent<SpriteRenderer>().color = initialColor;
            GetComponent<SpriteRenderer>().sortingOrder = initialOrderInLayer;

            //gm.SwitchingBoxCollider(true);
        }


        public IEnumerator ErrorPainting()
        {
            GetComponent<SpriteRenderer>().sprite = oqSprite;
            GetComponent<SpriteRenderer>().color = yangiRang;
            yield return new WaitForSeconds(0.3f);
            GetComponent<SpriteRenderer>().sprite = qoraSprite;
            GetComponent<SpriteRenderer>().color = initialColor;
            GetComponent<SpriteRenderer>().sortingOrder = initialOrderInLayer;

        }

        
    }
}
