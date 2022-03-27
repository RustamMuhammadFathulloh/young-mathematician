using ActionManager;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Add
{
    public class CanvasNumbers : MonoBehaviour
    {

        public GameObject[] numbers;


        private void Awake()
        {
            //for (int i = 0; i < numbers.Length; i++)
            //{
            //    //numbers[i].SetActive(false);
            //}
        }

        public void DeActivate()
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i].SetActive(false);
            }

        }

        public void ActivateCanvasNumbers(int unitNumber)
        {
            if (unitNumber.Equals(2))
            {
                Activate(2);
            }
            else if (unitNumber.Equals(3))
            {
                Activate(1);
            }
            else
            {
                Activate(0);
            }
        }


        void Activate(int num)
        {
            for (int i = num; i < numbers.Length; i++)
            {
                numbers[i].SetActive(true);
               
            }
        }


        public void GiveNumber(List<int> addedNumbers)
        {           
            int n = numbers.Length-1;
            for (int i = 0; i < addedNumbers.Count; i++)
            {
                numbers[n].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = addedNumbers[i].ToString();
                n--;
            }           
        }

        public void DisplayNumbersOnCanvas(int num, List<int> integerNumbers)
        {
            
            
        }

        public void InitialCOndition()
        {
            for (int i = 0; i < numbers.Length; i++)
            {
                numbers[i].transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "";
            }
        }



    }
}

