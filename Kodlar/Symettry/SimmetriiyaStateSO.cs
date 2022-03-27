using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Symmetry
{
    [CreateAssetMenu(fileName = "SimmetriyaState", menuName = "ScriptableObjects/SimmetriyaState", order = 9)]
    public class SimmetriiyaStateSO : ScriptableObject, ISerializationCallbackReceiver
    {

        public int initialStateNumber;

        [NonSerialized]
        public int runTimeStateNumber;

        private void OnEnable()
        {
            //runTimeStateNumber = initialStateNumber;
        }

        public void OnBeforeSerialize()
        {
            
        }

        public void OnAfterDeserialize()
        {
            
            runTimeStateNumber = initialStateNumber;
            
        }

        private void OnDisable()
        {
            //Debug.Log(runTimeStateNumber);
        }


    }

}

