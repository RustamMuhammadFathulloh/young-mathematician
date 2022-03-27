using ActionManager;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace YangiGame
{
    public class BoxScript : MonoBehaviour
    {
        public GameManager gm;

        public Sprite qizilBoxSprite;
        public Sprite yashilBoxSprite;

        public float duration;
        public Transform firstBoxPos;
        public Transform secondBoxPos;
        public Transform thirdBoxPos;
        public SpriteRenderer firstBoxSp1;
        public SpriteRenderer firstBoxSp2;
        public SpriteRenderer secondBoxSp;
        public SpriteRenderer thirdBoxSp;
        public GameEventSO moveEvent;


        public bool isVagon2;
        public bool isVagon3;

        public UnityEvent soundEvent;


        

        /// <summary>
        /// Animatsiyani qilib beruvchi kod.
        /// </summary>
        public void Animation()
        {
            StartCoroutine(MaximizeAndMinimize());

        }


        /// <summary>
        /// Obyektni avval minimize so'ngra esa maximize qiluvchi method.
        /// </summary>
        /// <returns></returns>
        IEnumerator MaximizeAndMinimize()
        {
            
            float vaqt = 0.6f;
            yield return new WaitForSeconds(vaqt + 0.2f);
            soundEvent.Invoke();
            gameObject.transform.DOScale(0, vaqt);
            
            yield return new WaitForSeconds(vaqt);

            firstBoxSp1.sprite = null;
            firstBoxSp2.sprite = null;
            secondBoxSp.sprite = null;
            if (thirdBoxSp != null)
            {
                thirdBoxSp.sprite = null;
            }

            ChangeSpriteToRed();
            yield return new WaitForSeconds(0.2f);
            
            gameObject.transform.DOScale(1, 0.6f);
            gm.StartGame();
        }


        /// <summary>
        /// Boxlardagi spriteni bir vaqtda almashtirib beruvchi method.
        /// </summary>
        public void ChangeSpriteToRed()
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = qizilBoxSprite;
            }
        }



        /// <summary>
        /// Boxlardagi spriteni bir vaqtda almashtirib beruvchi method.
        /// </summary>
        public void ChangeSpriteToGreen(int i)
        {
            gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>().sprite = yashilBoxSprite;
        }


    }
}
