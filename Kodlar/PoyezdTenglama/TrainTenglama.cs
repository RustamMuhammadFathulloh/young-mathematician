using ActionManager;
using DG.Tweening;
using Seperate2Digit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PoyezdTenglama
{
    public class TrainTenglama : MonoBehaviour
    {
        public QuestionManager questionManager;

        Vector3 initialPos;
        public GameManager gm;
        public List<GameObject> numberGroup;
        public float duration;
        public UnityEvent moveEvent;

        public GameObject handCursor;

        private void Awake()
        {
            initialPos = transform.position;
            numberGroup = Actions.ChildrenOfGameobject(transform.GetChild(1).gameObject);
        }


        public void MoveToCenter()
        {
            StartCoroutine(Actions.MoveOverSeconds(gameObject, new Vector3(0, transform.position.y, 0), duration));
            StartCoroutine(StopBalon());

        }

        public void MoveToLeft()
        {
            handCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.5f);
            StartCoroutine(MoveToRight());
        }

        public void StartMove()
        {
            
            StartCoroutine(Move());
        }

        public IEnumerator Move()
        {
            yield return new WaitForSeconds(1);
            DisableCollider();
            SetSprite();
            MoveToCenter();
            moveEvent.Invoke();
            yield return new WaitForSeconds(duration);
            EnableCollider();

        }


        public void ClearVagons()
        {
            foreach (GameObject obj in numberGroup)
            {
                if (!obj.GetComponent<TrainVagons>().isCorrect)
                {
                    obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                    obj.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = null;
                }
            }
        }


        /// <summary>
        /// Collidermi o'chirish.
        /// </summary>
        public void DisableCollider()
        {
            foreach (GameObject obj in numberGroup)
            {
                obj.GetComponent<BoxCollider2D>().enabled = false;
            }
        }


        void EnableCollider()
        {
            foreach (GameObject obj in numberGroup)
            {
                obj.GetComponent<BoxCollider2D>().enabled = true;
            }
        }


        void SetSprite()
        {
            GameObject obj = numberGroup[Random.Range(0, numberGroup.Count)];

            if (questionManager.result / 10 > 0)
            {
                obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = gm.sprites[questionManager.result / 10];
            }
            obj.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = gm.sprites[questionManager.result % 10];
            obj.GetComponent<TrainVagons>().isCorrect = true;
            int random = questionManager.result;
            int n = 1;

            foreach (GameObject anObj in numberGroup)
            {  
                random = questionManager.result;
                if (!anObj.GetComponent<TrainVagons>().isCorrect)
                {
                    if (n % 2 == 0)
                    {
                        random += n;
                        if (random >= 100)
                        {
                            random = random - 10;
                        }
                    }
                    else
                    {
                        //random = questionManager.result;
                        random -= n;
                        if ((random <= 0) && (random +10 !=questionManager.result))
                        {
                            random = random + 10;
                        }
                    }
                    
                    if (random / 10 > 0)
                    {
                        anObj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = gm.sprites[random / 10];  
                    }
                    anObj.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = gm.sprites[random % 10];
                    //random++;
                }
                n++;
            }
        }

        IEnumerator MoveToRight()
        {
            yield return new WaitForSeconds(1);

            moveEvent.Invoke();
            StartCoroutine(Actions.MoveOverSeconds(gameObject, new Vector3(initialPos.x * -1, initialPos.y, 0), duration));
            foreach (Balon balon in GetComponentsInChildren<Balon>())
            {
                balon.Move();
            }
            yield return new WaitForSeconds(duration + 0.1f);
            transform.position = initialPos;
            //yield return new WaitForSeconds(0.1f);
            Next();
        }

        void Next()
        {
            foreach (GameObject obj in numberGroup)
            {
                obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                obj.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = null;
                obj.GetComponent<TrainVagons>().isCorrect = false;
            }
            questionManager.StartGame();
        }


        int finn = 0;
        /// <summary>
        /// Balonlarni aylanishini to'xtatuvchi method.
        /// </summary>
        /// <returns></returns>
        IEnumerator StopBalon()
        {
            yield return new WaitForSeconds(duration);
            foreach (Balon balon in GetComponentsInChildren<Balon>())
            {
                balon.StopBalon();
            }
            if (finn == 0)
            {
                finn = 7;
                StartCoroutine(HandCursorAnim());
            }           
        }



        /// <summary>
        /// O'yin qanaqa o'ynalishini ko'rsatuvchi animatsiya.
        /// </summary>
        /// <returns></returns>
        public IEnumerator HandCursorAnim()
        {
            foreach (GameObject item in numberGroup)
            {
                if (item.GetComponent<TrainVagons>().isCorrect)
                {
                    float vaqt = 2f;
                    yield return new WaitForSeconds(vaqt/2);
                    Vector3 initialPos = handCursor.GetComponent<Transform>().position;
                    Vector3 pos = item.GetComponent<Transform>().position;
                    handCursor.transform.DOMove(pos, vaqt);
                    //handCursor.transform.DOMoveX(pos.x, vaqt);
                    //yield return new WaitForSeconds(vaqt);
                    //handCursor.transform.DOMoveY(pos.y, vaqt/2);
                    yield return new WaitForSeconds(vaqt);
                    handCursor.transform.DOScale(0.8f, 0.4f);                        
                    yield return new WaitForSeconds(0.4f);
                    handCursor.transform.DOScale(1.0f, 0.4f);
                    yield return new WaitForSeconds(0.4f);
                    handCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.5f);
                    yield return new WaitForSeconds(1.5f);
                    handCursor.transform.DOMove(initialPos, 0);
                    handCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.8f);
                }
                
            }
        }



    }
}

