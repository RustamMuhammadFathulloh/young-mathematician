using ActionManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace YangiGame
{
    public class EachNumber : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public GameManager gm;
        public QuestionMaker qm;
        public int number;
        public float duration;
        public BoxScript box2;
        public BoxScript box3;
        public int initialSortingOrder, initialSortingOrderNum;
        public UnityEvent correctEvent;
        public UnityEvent wrongEvent;
        public UnityEvent trueSignal;
        public UnityEvent swooshEvent;
        public Vector3 initialNumPos;


        private void Awake()
        {
            initialNumPos = gameObject.transform.position;
            initialSortingOrder = GetComponent<SpriteRenderer>().sortingOrder;
            initialSortingOrderNum = transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder;
        }



        public void OnBeginDrag(PointerEventData eventData)
        {
            GetComponent<SpriteRenderer>().maskInteraction = SpriteMaskInteraction.None;
        }

        
        public void OnDrag(PointerEventData eventData)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(pos.x, pos.y, 0);
            GetComponent<SpriteRenderer>().sortingOrder = 3;   // sorting orderni 4 dan 2 ga tushdi.
            transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = 4;
        }



        void CorrectAction(Vector3 pos)
        {
            correctEvent.Invoke();
            transform.position = initialNumPos;
            Instantiate(gm.correctParticle, pos, Quaternion.identity);
            SetInitialCondition();
        }


        /// <summary>
        /// Raqam noto'g'ri joyga keltirilganida wrongEvent ni chaqiradigan method().
        /// </summary>
        void WrongAction()
        {
            GetComponent<BoxCollider2D>().enabled = false;
            wrongEvent.Invoke();
            StartCoroutine(Actions.ActionWrong(this, gameObject));
            StartCoroutine(Waiting());
        }


        /// <summary>
        /// Biroz kutib gameObjectni boshlang'ich pozitsiyaga qaytaradi va 
        /// </summary>
        /// <returns></returns>
        IEnumerator Waiting()
        {
            yield return new WaitForSeconds(0.6f);
            StartCoroutine(Actions.MoveOverSeconds(gameObject, initialNumPos, 0.3f));
            yield return new WaitForSeconds(0.3f);
            SetInitialCondition();
        }


        IEnumerator MoveToShelf()
        {
            GetComponent<BoxCollider2D>().enabled = false;
            swooshEvent.Invoke();
            StartCoroutine(Actions.MoveOverSeconds(gameObject, initialNumPos, 0.3f));
            yield return new WaitForSeconds(0.3f);
            SetInitialCondition();
        }


        /// <summary>
        /// GameObjectni uchun Box Colliderni yoqadi va boshlang'ich Order in Layer ga qaytaradi.
        /// </summary>
        void SetInitialCondition()
        {
            GetComponent<SpriteRenderer>().sortingOrder = initialSortingOrder;
            transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = initialSortingOrderNum;
            
            GetComponent<BoxCollider2D>().enabled = true;
        }



        public void OnEndDrag(PointerEventData eventData)
        {
            if (gm.level.level == 1)
            {
                LevelOne();
            }
            else
            {
                LevelTwo();
            }
        }


        /// <summary>
        /// Level 1 uchun ishlaydigan kod.
        /// </summary>
        void LevelOne()
        {
            // 2-vagon
            if (number < 10)
            {
                // vagonga yaqin tursa
                if (Vector3.Distance(transform.position, box2.secondBoxPos.position) <= 1)
                {
                    int num = gm.questionNumber % 10;
                    if ((num.Equals(number)) && (gm.unlik % 10 != number)) 
                    {
                        gm.unlik += num;
                        CorrectAction(box2.secondBoxPos.position);
                        box2.secondBoxSp.sprite = gm.numbers[number];
                        StartCoroutine(TrainSignal());
                        box2.ChangeSpriteToGreen(1);       
                        box2.GetComponent<BoxScript>().isVagon2 = true;
                    }
                    else
                    {
                        WrongAction();
                    }
                }
                // vagondan uzoq tursa
                else if (Vector3.Distance(transform.position, box2.firstBoxPos.position) <= 1)
                {
                    WrongAction();
                }
                else
                {
                    StartCoroutine(MoveToShelf());
                }
            }
            // 1-vagon
            else
            {
                // vagonga yaqin tursa
                if (Vector3.Distance(transform.position, box2.firstBoxPos.position) <= 1)
                {                    
                    if (10.Equals(number))
                    {
                        gm.unlik += 10;
                        int num = gm.questionNumber;
                        if (!box2.GetComponent<BoxScript>().isVagon2)
                        {
                            num = gm.questionNumber - (gm.questionNumber % 10);
                        }
                        if (num >= gm.unlik)
                        {
                            CorrectAction(box2.firstBoxPos.position);
                            box2.firstBoxSp1.sprite = gm.numbers[gm.unlik / 10];
                            box2.firstBoxSp2.sprite = gm.numbers[0];
                            
                            int unlikQuestion = gm.questionNumber / 10;
                            int unlikGmUnlik = gm.unlik / 10;
                            if (num.Equals(gm.unlik) || (unlikGmUnlik == unlikQuestion)) 
                            {                                
                                StartCoroutine(TrainSignal());
                                box2.ChangeSpriteToGreen(0);    // bu yangi kod qizilSpprite ni yashilga o'zgartirib beradi.
                            }
                        }
                        else
                        {
                            gm.unlik -= 10;
                            WrongAction();
                        }
                    }
                    else
                    {
                        WrongAction();
                    }
                }
                // vagonga uzoq tursa
                else if (Vector3.Distance(transform.position, box2.secondBoxPos.position) <= 1)
                {
                    WrongAction();
                }
                else
                {
                    StartCoroutine(MoveToShelf());
                }
            }
        }


        /// <summary>
        /// Level 2 uchun ishlaydigan kod.
        /// </summary>
        void LevelTwo()
        {
            // 2-vagon yoki 3-vagon
            if (number < 10)
            {
                //2- vagonga yaqin tursa
                if (Vector3.Distance(transform.position, box3.secondBoxPos.position) <= 1)
                {
                    if (gm.unlik == 0)
                    {
                        int num = gm.questionNumber % 10;
                        if (num > number)
                        {
                            gm.unlik += number;
                            CorrectAction(box3.secondBoxPos.position);
                            box3.secondBoxSp.sprite = gm.numbers[number];
                            StartCoroutine(TrainSignal());
                            box3.ChangeSpriteToGreen(1);.
                            box3.GetComponent<BoxScript>().isVagon2 = true;
                        }
                        else
                        {
                            WrongAction();
                        }
                    }
                    else if (gm.unlik / 10 <= 0)
                    {
                        if (gm.unlik + number == (gm.questionNumber % 10))
                        {
                            gm.unlik += number;
                            CorrectAction(box3.secondBoxPos.position);
                            box3.secondBoxSp.sprite = gm.numbers[number];
                            StartCoroutine(TrainSignal());
                            box3.ChangeSpriteToGreen(1);
                            box3.GetComponent<BoxScript>().isVagon2 = true;
                        }
                        else
                        {
                            WrongAction();
                        }
                    }
                    else if (gm.unlik / 10 > 0)
                    {
                        if (gm.unlik % 10 == 0)
                        {
                            if ((gm.unlik + number) < gm.questionNumber && number < (gm.questionNumber % 10))
                            {
                                gm.unlik += number;
                                CorrectAction(box3.secondBoxPos.position);
                                box3.secondBoxSp.sprite = gm.numbers[number];
                                StartCoroutine(TrainSignal());
                                box3.ChangeSpriteToGreen(1);
                                box3.GetComponent<BoxScript>().isVagon2 = true;
                            }
                            else
                            {
                                WrongAction();
                            }
                        }
                        else
                        {
                            if ((gm.unlik + number) == gm.questionNumber)
                            {
                                gm.unlik += number;
                                CorrectAction(box3.secondBoxPos.position);
                                box3.secondBoxSp.sprite = gm.numbers[number];
                                StartCoroutine(TrainSignal());
                                box3.ChangeSpriteToGreen(1);
                                box3.GetComponent<BoxScript>().isVagon2 = true;
                            }
                            else if ((gm.unlik + number) < gm.questionNumber && (gm.unlik % 10) + number == (gm.questionNumber % 10))
                            {
                                gm.unlik += number;
                                CorrectAction(box3.secondBoxPos.position);
                                box3.secondBoxSp.sprite = gm.numbers[number];
                                StartCoroutine(TrainSignal());
                                box3.ChangeSpriteToGreen(1);
                                box3.GetComponent<BoxScript>().isVagon2 = true;
                            }
                            else
                            {
                                WrongAction();
                            }
                        }
                    }
                }
                //1- vagonga yaqin tursa
                else if (Vector3.Distance(transform.position, box3.firstBoxPos.position) <= 1)
                {
                    WrongAction();
                }
                // 3- vagonga yaqin tursa
                else if (Vector3.Distance(transform.position, box3.thirdBoxPos.position) <= 1)
                {
                    if (gm.unlik == 0)
                    {
                        int num = gm.questionNumber % 10;
                        if (num > number)
                        {
                            gm.unlik += number;
                            CorrectAction(box3.thirdBoxPos.position);
                            box3.thirdBoxSp.sprite = gm.numbers[number];
                            StartCoroutine(TrainSignal());
                            box3.ChangeSpriteToGreen(2);
                            box3.GetComponent<BoxScript>().isVagon3 = true;
                        }
                        else
                        {
                            WrongAction();
                        }
                    }
                    else if (gm.unlik / 10 <= 0)
                    {
                        if (gm.unlik + number == (gm.questionNumber % 10))
                        {
                            gm.unlik += number;
                            CorrectAction(box3.thirdBoxPos.position);
                            box3.thirdBoxSp.sprite = gm.numbers[number];
                            StartCoroutine(TrainSignal());
                            box3.ChangeSpriteToGreen(2);
                            box3.GetComponent<BoxScript>().isVagon3 = true;
                        }
                        else
                        {
                            WrongAction();
                        }
                    }
                    else if (gm.unlik / 10 > 0)
                    {
                        if (gm.unlik % 10 == 0)
                        {
                            if ((gm.unlik + number) < gm.questionNumber && number < (gm.questionNumber % 10))
                            {
                                gm.unlik += number;
                                CorrectAction(box3.thirdBoxPos.position);
                                box3.thirdBoxSp.sprite = gm.numbers[number];
                                StartCoroutine(TrainSignal());
                                box3.ChangeSpriteToGreen(2);
                                box3.GetComponent<BoxScript>().isVagon3 = true;
                            }
                            else
                            {
                                WrongAction();
                            }
                        }
                        else
                        {
                            if ((gm.unlik + number) == gm.questionNumber)
                            {
                                gm.unlik += number;
                                CorrectAction(box3.thirdBoxPos.position);
                                box3.thirdBoxSp.sprite = gm.numbers[number];
                                StartCoroutine(TrainSignal());
                                box3.ChangeSpriteToGreen(2);
                                box3.GetComponent<BoxScript>().isVagon3 = true;
                            }
                            else if ((gm.unlik + number) < gm.questionNumber && (gm.unlik % 10) + number == (gm.questionNumber % 10))
                            {
                                gm.unlik += number;
                                CorrectAction(box3.thirdBoxPos.position);
                                box3.thirdBoxSp.sprite = gm.numbers[number];
                                StartCoroutine(TrainSignal());
                                box3.ChangeSpriteToGreen(2);
                                box3.GetComponent<BoxScript>().isVagon3 = true;
                            }
                            else
                            {
                                WrongAction();
                            }
                        }
                    }
                }
                // Hech qaysi vagonga yaqin turmasa shelf tomon surilsin
                else
                {
                    StartCoroutine(MoveToShelf());
                }
            }
            // 1-vagon
            else
            {
                // 1-vagonga yaqin tursa
                if (Vector3.Distance(transform.position, box3.firstBoxPos.position) <= 1)
                {
                    if (10.Equals(number))
                    {
                        gm.unlik += 10;
                        int num = gm.questionNumber;
                        if (!box3.GetComponent<BoxScript>().isVagon2 && !box3.GetComponent<BoxScript>().isVagon3)
                        {
                            num = gm.questionNumber - (gm.questionNumber % 10);
                        }
                        if (num >= gm.unlik)
                        {
                            CorrectAction(box3.firstBoxPos.position);
                            box3.firstBoxSp1.sprite = gm.numbers[gm.unlik / 10];
                            box3.firstBoxSp2.sprite = gm.numbers[0];
                            if (gm.unlik >= (gm.questionNumber - (gm.questionNumber % 10)))  // if (num.Equals(gm.unlik))
                            {
                                StartCoroutine(TrainSignal());
                                box3.ChangeSpriteToGreen(0);
                            }
                        }
                        else
                        {
                            gm.unlik -= 10;
                            WrongAction();
                        }
                    }
                    else
                    {
                        WrongAction();
                    }
                }
                // 2-vagonga uzoq tursa
                else if (Vector3.Distance(transform.position, box3.secondBoxPos.position) <= 1)
                {
                    WrongAction();
                }
                // 3-vagonga yaqin tursa
                else if (Vector3.Distance(transform.position, box3.thirdBoxPos.position) <= 1)
                {
                    WrongAction();
                }
                else
                {
                    StartCoroutine(MoveToShelf());
                }
            }
        }



        /// <summary>
        /// Agar kerakli son yozib bo'linsa ishga tushuvchi eventni chaqiradi.
        /// </summary>
        /// <returns></returns>
        IEnumerator TrainSignal()
        {
            yield return new WaitForSeconds(0.5f);
            trueSignal.Invoke();
        }



    }
}
