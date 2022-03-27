using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Add
{
    public class Square : MonoBehaviour
    {
        public SquareNumber squareNumberObj;
        public SquareMovement squareMovementObj;
        public GameManager gm;
        public bool isCorrect;


        private void Awake()
        {
            squareNumberObj = GetComponent<SquareNumber>();
            squareMovementObj = GetComponent<SquareMovement>();
        }

      
        public void InitialCondition()
        {
            
        }

        public void CorrectAction()
        {
            
        }


    }
}

