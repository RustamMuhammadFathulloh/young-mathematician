using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ComposingShape
{
    public class Triangle : MonoBehaviour
    {

        public bool isEnable;
     

        private void Awake()
        {
            isEnable = true;
          
        }


        public Transform one;
       
        public Transform two;
     
        public Transform three;

      
     

    }

}

