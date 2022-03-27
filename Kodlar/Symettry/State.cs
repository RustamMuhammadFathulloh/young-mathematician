using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Symmetry
{
    public class State : MonoBehaviour
    {
        TMP_Text stateText;
        public SimmetriiyaStateSO state;
        

        private void Awake()
        {
            stateText = GetComponent<TMP_Text>();
        }

        public void UpdateText()
        {
            stateText.text = state.runTimeStateNumber.ToString() + "/5";
        }

        public void SetStateToZero()
        {
            state.runTimeStateNumber = state.initialStateNumber;
        }


    }

}


