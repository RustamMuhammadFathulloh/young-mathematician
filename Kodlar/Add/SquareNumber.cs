using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Add
{
    public class SquareNumber : MonoBehaviour
    {

        SpriteRenderer firstSpriteRender;
        SpriteRenderer secondSpriteRender;
        SpriteRenderer centerSpriteRender;
        public int number;
        public int initialNumber;
        int n = 0;


        private void Awake()
        {
            firstSpriteRender = transform.GetChild(1).GetComponent<SpriteRenderer>();
            secondSpriteRender = transform.GetChild(2).GetComponent<SpriteRenderer>();
            centerSpriteRender = transform.GetChild(3).GetComponent<SpriteRenderer>();

        }



        public void GiveSpriteNumber(int aNumber, Sprite number1Sp, Sprite number2Sp)
        {
            n++;
            if (n.Equals(1))
            {
                initialNumber = aNumber;
            }
            number = aNumber;
            
            int firstNum = number / 10;
            if (firstNum.Equals(0))
            {
                centerSpriteRender.sprite = number2Sp;
            }
            else
            {
                firstSpriteRender.sprite = number1Sp;
                secondSpriteRender.sprite = number2Sp;
                centerSpriteRender.sprite = null;
            }
        }

       

    }

}

