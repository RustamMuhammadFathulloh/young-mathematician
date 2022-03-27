using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionManager;


namespace Symmetry
{
    public class SquareAnimation : MonoBehaviour
    {
        public ColorSO colors;
        public float maxScale;
        public float animDuration;
        public float fadeValue;
        public float initialScale;
        public float angle;


        IEnumerator coroutine;

        private void Awake()
        {
            coroutine = MaximizeToWithRepeat();
        }



        public void Maximize(Color color)
        {
            
            StartCoroutine(MaximizeTo(color));

        }


        IEnumerator MaximizeTo(Color color)
        {
            transform.localRotation = Quaternion.Euler(0, 0, angle);
            transform.localScale = new Vector3(initialScale, initialScale, 0);
            GetComponent<SpriteRenderer>().color = color;
            StartCoroutine(Actions.ScaleOverSeconds(gameObject, new Vector3(maxScale, maxScale, 0), animDuration));
            StartCoroutine(Actions.FadeOverTime(GetComponent<SpriteRenderer>(), fadeValue, animDuration));
            yield return new WaitForSeconds(0);
        }



        IEnumerator MaximizeToWithRepeat()
        {
            transform.localRotation = Quaternion.Euler(0, 0, angle);
            transform.localScale = new Vector3(initialScale, initialScale, 0);
            GetComponent<SpriteRenderer>().color = colors.black;
            StartCoroutine(Actions.ScaleOverSeconds(gameObject, new Vector3(maxScale, maxScale, 0), animDuration));
            StartCoroutine(Actions.FadeOverTime(GetComponent<SpriteRenderer>(), fadeValue, animDuration));
            yield return new WaitForSeconds(2);
            coroutine = MaximizeToWithRepeat();
            StartCoroutine(coroutine);
        }


        public void ShowAnswer()
        {
            coroutine = MaximizeToWithRepeat();
            StartCoroutine(coroutine);
        }


        public void StopAnim()
        {           
            StopCoroutine(coroutine);            
            transform.localScale = new Vector3(0, 0, 0);
        }



    }

}

