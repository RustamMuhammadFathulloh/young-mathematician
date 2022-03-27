using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SectionColor
{
    public class ForEachObject : MonoBehaviour, IPointerClickHandler
    {
        public Sprite spritewasClicked;
        Sprite initialSprite;
        public CalculationManager calManager;
        public int pushing = 0;
        bool changeSprite = true;


        void Start()
        {
            initialSprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            if (changeSprite)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = spritewasClicked;
                changeSprite = false;
                pushing = 1;
            }
            else if (!changeSprite)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = initialSprite;
                changeSprite = true;
                pushing = 0;
            }

            calManager.clickEvent.Invoke();
        }



    }
}

