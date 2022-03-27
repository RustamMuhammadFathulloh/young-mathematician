using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionManager;

namespace FruitSplat
{
    public class QuestionText : MonoBehaviour
    {

        public float maxSize;


        public void AnimateText()
        {
            StartCoroutine(Animate());
            

        }


        IEnumerator Animate()
        {
            StartCoroutine(Actions.ScaleOverSeconds(gameObject, new Vector3(maxSize, maxSize, 0), 0.25f));
            yield return new WaitForSeconds(0.25f);
            StartCoroutine(Actions.ScaleOverSeconds(gameObject, new Vector3(1, 1, 0), 0.25f));
            yield return new WaitForSeconds(0.25f);
        }

    }

}

