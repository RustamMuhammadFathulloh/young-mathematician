using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace PiecesWhole
{
    public class PiecesSquare : MonoBehaviour, IPointerClickHandler
    {
        public GameManager gm;
        public CalculationManager cm;
        public bool buttonIsOn = true, boxCollider = true;
        public Sprite clickedSprite;
        public Sprite noClickedSprite;
        public Vector3 inialPosSprite;

        


        void Start()
        {
            noClickedSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
            inialPosSprite = gameObject.transform.GetChild(0).transform.position;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (cm.clickedObjects.Count != 2) 
            {
                if (buttonIsOn)
                {
                    GetComponent<SpriteRenderer>().sprite = clickedSprite;
                    buttonIsOn = false;
                    cm.clickedObjects.Add(gameObject);
                }
                else if (!buttonIsOn)
                {
                    GetComponent<SpriteRenderer>().sprite = noClickedSprite;
                    buttonIsOn = true;
                    cm.clickedObjects.Remove(gameObject);
                }
                cm.clickEvent.Invoke();
            }
                       
        }


        public void ReturnSprite(bool tf)
        {
            GetComponent<BoxCollider2D>().enabled = tf;
            GetComponent<SpriteRenderer>().sprite = noClickedSprite;
            if (tf)            {
                buttonIsOn = true;
                boxCollider = true;
            }
            else if (!tf)            {
                boxCollider = false;
            }

            
        }



    }
}

