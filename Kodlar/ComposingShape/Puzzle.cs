using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionManager;

namespace ComposingShape
{
    public class Puzzle : MonoBehaviour
    {
        public GameManager gm;
        //[HideInInspector]
        public List<GameObject> puzzleShapes;

        Vector3 initialScale;


        private void Awake()
        {
            initialScale = transform.localScale;
            puzzleShapes = Actions.ChildrenOfGameobject(gameObject);
        }


        private void Start()
        {
            StartCoroutine(AnimateObject());
            
        }


        IEnumerator AnimateObject()
        {
            
            StartCoroutine(Actions.ScaleOverSeconds(gameObject, initialScale * 1.25f, 0.2f));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(Actions.ScaleOverSeconds(gameObject, initialScale, 0.2f));
            yield return new WaitForSeconds(0.2f);

        }



    }

}


