using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Symmetry
{
    public class Square : MonoBehaviour, IPointerClickHandler
    {
        public GameManager gameManager;
        public bool isCorrect;
        public GameEventSO clickEvent;
        public GameObject squarePrefab;
        public GameObject rombPrefab;
        int numberOfClick;
        bool isFirst = true;
        public GameObject clickAnim;
        SpriteForSimmetriyaSO sprites;
        public ColorSO colors;
        LevelSO level;
        public GameObject rombAnim;
        public string colorStr;

        public QuestionSpawner questionSpawner;

        private void Awake()
        {
            colorStr = "";
            sprites = Resources.Load<SpriteForSimmetriyaSO>("SO/Simmetriya/SimmetriyaSprites/SimmetriyaSprite");
            level = Resources.Load<LevelSO>("SO/Level/Level");
            colors = Resources.Load<ColorSO>("SO/Color/ColorAll");
            numberOfClick = 0;
            isFirst = true;
        }


        public void OnPointerClick(PointerEventData eventData)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(EnableBoxCollider2D());
            CheckLevel();            
            StartCoroutine(ClickAnim());
        }


        void NumberOfClickIncrementThree()
        {
            if (numberOfClick.Equals(4))
            {
                numberOfClick = 0;
                numberOfClick++;
            }
            else
            {
                numberOfClick++;
            }
        }

        void NumberOfClickIncrementOne()
        {
            if (numberOfClick.Equals(2))
            {
                numberOfClick = 0;
                numberOfClick++;
            }
            else
            {
                numberOfClick++;
            }
        }

        void NumberOfClickIncrementTwo()
        {
            if (numberOfClick.Equals(3))
            {
                numberOfClick = 0;
                numberOfClick++;
            }
            else
            {
                numberOfClick++;
            }
        }


        void Cover(Color color)
        {
            GetComponent<SpriteRenderer>().sprite = sprites.cover;
            GetComponent<SpriteRenderer>().color = color;
            clickEvent.Raise();
        }

        void Edge()
        {
            GetComponent<SpriteRenderer>().sprite = sprites.edge;
            GetComponent<SpriteRenderer>().color = colors.grey;
            clickEvent.Raise();
        }

        void Wrong()
        {
            GetComponent<SpriteRenderer>().sprite = sprites.wrong;
            GetComponent<SpriteRenderer>().color = colors.black;
            clickEvent.Raise();
        }

        void CheckLevel()
        {            
            int levelNumber = level.level;
            if (levelNumber >= 1 && levelNumber < 4)
            {
                NumberOfClickIncrementOne();
                if (numberOfClick.Equals(1))
                {
                    if (isCorrect)
                    {
                        Cover(colors.green);
                        gameManager.CorrectChanges(1);
                        if (level.level.Equals(1))
                        {
                            if (rombAnim != null)
                            {
                                rombAnim.GetComponent<SquareAnimation>().StopAnim();
                            }                           
                        }                                                
                    }
                    else
                    {
                        Wrong();
                        gameManager.WrongChanges(1);
                    }                                      
                }
                else
                {
                    if (isCorrect)
                    {
                        Edge();
                        gameManager.CorrectChanges(-1);
                    }
                    else
                    {
                        Edge();
                        gameManager.WrongChanges(-1);
                    }
                   
                }
            }
            else if (levelNumber >= 4 && levelNumber < 7)
            {
                NumberOfClickIncrementTwo();
                if (numberOfClick.Equals(1))
                {
                    Cover(colors.green);
                    CheckSquare(colors.green.ToString());
                    
                }
                else if (numberOfClick.Equals(2))
                {
                    Cover(colors.pink);
                    CheckSquare(colors.pink.ToString());
                    
                }
                else
                {
                    Edge();
                    CheckSquare(colors.grey.ToString());
                }
            }
            else
            {
                NumberOfClickIncrementThree();
                if (numberOfClick.Equals(1))
                {
                    Cover(colors.green);
                    CheckSquare(colors.green.ToString());
                }
                else if (numberOfClick.Equals(2))
                {
                    Cover(colors.pink);
                    CheckSquare(colors.pink.ToString());
                }
                else if (numberOfClick.Equals(3))
                {
                    Cover(colors.blue);
                    CheckSquare(colors.blue.ToString());
                }
                else
                {
                    Edge();
                    CheckSquare(colors.grey.ToString());
                }
            }
        }


        public IEnumerator EnableBoxCollider2D()
        {
            yield return new WaitForSeconds(0.2f);
            GetComponent<BoxCollider2D>().enabled = true;
        }
        public IEnumerator ClickAnim()
        {
            if (isFirst)
            {
                clickAnim = Instantiate(squarePrefab, transform.position, Quaternion.identity);
                isFirst = false;
            }
            clickAnim.GetComponent<SquareAnimation>().Maximize(clickAnim.GetComponent<SquareAnimation>().colors.black);
            yield return new WaitForSeconds(0);            
        }

        public IEnumerator ShowAnswerAnim(GameObject obj, Color color)
        {
            yield return new WaitForSeconds(0.5f);
            rombAnim = Instantiate(rombPrefab, obj.transform.position, Quaternion.identity);
            rombAnim.GetComponent<SquareAnimation>().ShowAnswer();
            questionSpawner.answerAnimGroup.Add(rombAnim);            
            yield return new WaitForSeconds(0);
            
        }


        void CheckSquare(string colorCode)
        {
            if (isCorrect)
            {
                if (colorStr.Equals(colors.green.ToString()))
                {
                    if (colorStr.Equals(colorCode))
                    {
                        gameManager.CorrectChanges(1);
                    }
                    else if (colorCode.Equals(colors.pink.ToString()))
                    {

                    }
                    else if (colorCode.Equals(colors.blue.ToString()))
                    {
                        
                    }
                    else
                    {
                        gameManager.CorrectChanges(-1);
                    }
                }
                else if (colorStr.Equals(colors.pink.ToString()))
                {
                    if (colorStr.Equals(colorCode))
                    {
                        gameManager.CorrectChanges(1);
                    }
                    else if (colorCode.Equals(colors.green.ToString()))
                    {

                    }
                    else if (colorCode.Equals(colors.blue.ToString()))
                    {

                    }
                    else
                    {
                        gameManager.CorrectChanges(-1);
                    }
                }
                else
                {
                    if (colorStr.Equals(colorCode))
                    {
                        gameManager.CorrectChanges(1);
                    }
                    else if (colorCode.Equals(colors.green.ToString()))
                    {

                    }
                    else if (colorCode.Equals(colors.pink.ToString()))
                    {

                    }
                    else
                    {
                        gameManager.CorrectChanges(-1);
                    }

                }
            }
            else
            {
                if (colorCode.Equals(colors.green.ToString()))
                {
                    gameManager.WrongChanges(1);
                }
                else if (colorCode.Equals(colors.pink.ToString()))
                {

                }
                else if (colorCode.Equals(colors.blue.ToString()))
                {
                    
                }
                else
                {
                    gameManager.WrongChanges(-1);
                }
            }
                  
        }
    }
}

