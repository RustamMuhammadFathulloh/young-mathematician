using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionManager;
using UnityEngine.Events;
using DG.Tweening;

namespace BingoMul
{
    public class BigSquare : MonoBehaviour
    {
        public float duration;

        [HideInInspector]
        public List<GameObject> children;
        Vector3 initialPos;
       


        private void Awake()
        {
            initialPos = transform.position;
            transform.position = new Vector3(initialPos.x, initialPos.y * 20, 0);
            children = Actions.ChildrenOfGameobject(gameObject);
            
        }


        private void Start()
        {
            InitialAnim();
        }

        public void InitialAnim()
        {
            transform.DOMoveY(initialPos.y, duration);
            

        }

       


    }

}

