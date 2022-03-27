using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionManager;
using UnityEngine.EventSystems;
using System.Linq;

namespace Add
{
    public class SquareMovement : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {

        private float _maxSize;
        private float _animDuration;
        public SqState sqState;
        public GameManager gm;


        Vector3 beginPos;

        public bool isRight;
        public bool isLeft;
        public bool isUp;
        public bool isDown;

        private void Awake()
        {            
            _maxSize = sqState.maxSize;
            _animDuration = sqState.animDuration;
            
        }

        public void AnimateSquare(float startingSize)
        {
            StartCoroutine(Animate(startingSize));
        }


        IEnumerator Animate(float startingSize)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            transform.localScale = new Vector3(startingSize, startingSize, 0);
            StartCoroutine(Actions.ScaleOverSeconds(gameObject, new Vector3(_maxSize, _maxSize, 0), _animDuration));
            yield return new WaitForSeconds(_animDuration);
            StartCoroutine(Actions.ScaleOverSeconds(gameObject, new Vector3(1, 1, 0), _animDuration));
            yield return new WaitForSeconds(_animDuration);
            GetComponent<BoxCollider2D>().enabled = true;
        }

        public void SetMoveBorder()
        {
            if (transform.position.x < gm.borderX)
            {
                isRight = true;
            }
            else
            {
                isRight = false;
            }

            if (transform.position.x > -gm.borderX)
            {
                isLeft = true;
            }
            else
            {
                isLeft = false;
            }

            if (transform.position.y < (gm.borderY) * -1)
            {
                isUp = true;
            }
            else
            {
                isUp = false;
            }

            if (transform.position.y > gm.borderY)
            {
                isDown = true;
            }
            else
            {
                isDown = false;
            }
        }

        bool CanMove(Vector3 pos)
        {
            bool isCapableOfMove = false;
            foreach (GameObject obj in gm.questionMaker.squares)
            {
                if (obj.transform.position.Equals(pos))
                {
                    isCapableOfMove = false;
                    break;
                }
                else
                {
                    isCapableOfMove = true;
                }
            }
            return isCapableOfMove;
        }

        void CanAdd(Vector3 pos)
        {            
            foreach (GameObject obj in gm.questionMaker.squares)
            {
                if (obj.transform.position.Equals(pos))
                {
                    if (GetComponent<Square>().isCorrect && obj.GetComponent<Square>().isCorrect)
                    {
                        CorrectAction(obj);                       
                    }
                    else
                    {
                        WrongAction(obj);
                    }
                    break;
                }                
            }            
        }

        void CorrectAction(GameObject obj)
        {
            StartCoroutine(CorrectAnim());
            gm.CorrectEvent();           
            result = obj.GetComponent<SquareNumber>().number + GetComponent<SquareNumber>().number;
            if (gm.addedIntegers.Count.Equals(0))
            {
                gm.addedIntegers.Add(obj.GetComponent<SquareNumber>().initialNumber);
                gm.addedIntegers.Add(GetComponent<SquareNumber>().initialNumber);
                
            }
            else
            {
                if (GetComponent<SquareNumber>().number != GetComponent<SquareNumber>().initialNumber)
                {
                   
                    if (gm.addedIntegers.Count() < gm.questionMaker.unitNumber)
                    {
                       
                        gm.addedIntegers.Add(obj.GetComponent<SquareNumber>().initialNumber);
                    }
                    
                }
                else
                {
                    if (obj.GetComponent<SquareNumber>().number != obj.GetComponent<SquareNumber>().initialNumber)
                    {
                       
                        gm.addedIntegers.Add(GetComponent<SquareNumber>().initialNumber);                       
                    }
                    else
                    {
                      
                        gm.addedIntegers.Add(obj.GetComponent<SquareNumber>().initialNumber);
                        gm.addedIntegers.Add(GetComponent<SquareNumber>().initialNumber);
                    }                    
                }
                         
            }

            gm.canvasNumber.GiveNumber(gm.addedIntegers);
            GetComponent<SquareNumber>().number = result;
            GetComponent<SquareNumber>().GiveSpriteNumber(result, gm.questionMaker.numberSprites[result / 10], gm.questionMaker.numberSprites[result % 10]);
            GetComponent<SpriteRenderer>().color = gm.green;
            gm.questionMaker.squares.Remove(obj);
            Destroy(obj);
        }

        int result;

        void WrongAction(GameObject obj)
        {
            StartCoroutine(WrongAnim());
            gm.WrongEvent();
            result = obj.GetComponent<SquareNumber>().number + GetComponent<SquareNumber>().number;
            GetComponent<SquareNumber>().number = result;
            GetComponent<SquareNumber>().GiveSpriteNumber(result, gm.questionMaker.numberSprites[result / 10], gm.questionMaker.numberSprites[result % 10]);
            GetComponent<SpriteRenderer>().color = gm.red;
            gm.questionMaker.squares.Remove(obj);
            Destroy(obj);
        }

        IEnumerator CorrectAnim()
        {        
            
            yield return new WaitForSeconds(0.2f);
            
            AnimateSquare(1);
            yield return new WaitForSeconds(0.8f);
            if (GetComponent<SquareNumber>().number.Equals(gm.questionMaker.questionNumber))
            {               
                Destroy(gm.grid);
                gm.questionMaker.PassNext();
            }           
        }



        IEnumerator WrongAnim()
        {
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(Actions.ActionWrong(this, gameObject));
            yield return new WaitForSeconds(0.8f);
            Destroy(gm.grid);
            gm.GameOver();
        }


        public void OnBeginDrag(PointerEventData eventData)
        {            
            beginPos = eventData.position;           
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            
            float x1 = beginPos.x;
            float x2 = eventData.position.x;

            float y1 = beginPos.y;
            float y2 = eventData.position.y;

            float distanceX = x1 - x2;
            float distanceY = y1 - y2;

            SetMoveBorder();
            if (Mathf.Abs(distanceX) > Mathf.Abs(distanceY))
            {
                // Chap
                if (distanceX > 0)
                {
                    Move(new Vector3(transform.position.x - gm.offset, transform.position.y, 0), isLeft);
                    CanAdd(new Vector3(transform.position.x - gm.offset, transform.position.y, 0));
                    
                }
                // O'ng
                else
                {
                    Move(new Vector3(transform.position.x + gm.offset, transform.position.y, 0), isRight);
                    CanAdd(new Vector3(transform.position.x + gm.offset, transform.position.y, 0));
                   
                }
            }
            else
            {
                // Pastga
                if (distanceY > 0)
                {
                    if (CanMove(new Vector3(transform.position.x, transform.position.y - gm.offset, 0)))
                    {
                        Move(new Vector3(transform.position.x, transform.position.y - gm.offset, 0), isDown);
                    }                    
                }
                //Tepaga
                else
                {
                    if (CanMove(new Vector3(transform.position.x, transform.position.y + gm.offset, 0)))
                    {
                        Move(new Vector3(transform.position.x, transform.position.y + gm.offset, 0), isUp);
                    }                    
                }
            }
        }


        void Move(Vector3 pos, bool isCapableOfMove)
        {

            if (isCapableOfMove)
            {
                StartCoroutine(Actions.MoveOverSeconds(gameObject, pos, gm.moveSpeed));
                gm.moveEvent.Raise();
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }
    }

}

