using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BingoMul
{


    public class BingoOperation : MonoBehaviour
    {

        [EnumPaging]
        public OperationType operation;

        const string key = "EnumValue";


        private void Awake()
        {
            
        }


        public void SaveEnum()
        {
            string saveString = operation.ToString();

            PlayerPrefs.SetString(key, saveString);
            PlayerPrefs.Save();
        }


    }

}

