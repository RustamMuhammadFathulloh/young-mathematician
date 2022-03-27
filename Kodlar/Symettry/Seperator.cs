using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Linq;
using ActionManager;

namespace Symmetry
{
    public class Seperator : MonoBehaviour
    {
        public SpriteForSimmetriyaSO sprites;
        public GameManager gameManager;
        public GameEventSO clickEvent;
        public Background background;
        public GameObject parent;
        public GameObject squareAnimPrefab;
        public GameObject rombAnimPrefab;
        //[HideInInspector]
        public List<GameObject> questionGroup;
        //[HideInInspector]
        public List<GameObject> answerGroup;
        public string side;

        public void InitialCondition(Sprite sprite, Color color)
        {
            foreach (GameObject obj in parent.Child("Squares (1)").Children())
            {
                obj.GetComponent<SpriteRenderer>().sprite = sprite;
                obj.GetComponent<SpriteRenderer>().color = color; 
            }
            foreach (GameObject obj in parent.Child("Squares (2)").Children())
            {
                obj.GetComponent<SpriteRenderer>().sprite = sprite;
                obj.GetComponent<SpriteRenderer>().color = color;
            }
            foreach (GameObject obj in parent.Child("Squares (3)").Children())
            {
                obj.GetComponent<SpriteRenderer>().sprite = sprite;
                obj.GetComponent<SpriteRenderer>().color = color;
            }
            foreach (GameObject obj in parent.Child("Squares (4)").Children())
            {
                obj.GetComponent<SpriteRenderer>().sprite = sprite;
                obj.GetComponent<SpriteRenderer>().color = color;
            }
        }
        public void SeperateBothSide()
        {
            questionGroup.Clear();
            answerGroup.Clear();
            switch (background.backgroundCode)
            {
                case 1: // Answer 1,3   
                    questionGroup = new List<GameObject>(parent.Child("Squares (2)").Children());
                    questionGroup.AddRange(parent.Child("Squares (4)").Children());
                    answerGroup = new List<GameObject>(parent.Child("Squares (1)").Children());
                    answerGroup.AddRange(parent.Child("Squares (3)").Children());
                    MoveFromRight(parent.Child("Squares (2)"));
                    MoveFromRight(parent.Child("Squares (4)"));
                    MoveFromLeft(parent.Child("Squares (1)"));
                    MoveFromLeft(parent.Child("Squares (3)"));
                    side = "Horizontal";
                    break;
                case 2: // Answer 2,4    
                    questionGroup = new List<GameObject>(parent.Child("Squares (1)").Children());
                    questionGroup.AddRange(parent.Child("Squares (3)").Children());
                    answerGroup = new List<GameObject>(parent.Child("Squares (2)").Children());
                    answerGroup.AddRange(parent.Child("Squares (4)").Children());
                    MoveFromRight(parent.Child("Squares (2)"));
                    MoveFromRight(parent.Child("Squares (4)"));
                    MoveFromLeft(parent.Child("Squares (1)"));
                    MoveFromLeft(parent.Child("Squares (3)"));
                    side = "Horizontal";
                    break;
                case 3: // Answer 1,2   
                    questionGroup = new List<GameObject>(parent.Child("Squares (3)").Children());
                    questionGroup.AddRange(parent.Child("Squares (4)").Children());
                    answerGroup = new List<GameObject>(parent.Child("Squares (1)").Children());
                    answerGroup.AddRange(parent.Child("Squares (2)").Children());
                    MoveFromTop(parent.Child("Squares (1)"));
                    MoveFromTop(parent.Child("Squares (2)"));
                    MoveFromBottom(parent.Child("Squares (3)"));
                    MoveFromBottom(parent.Child("Squares (4)"));
                    side = "Vertical";
                    break;
                case 4: // Answer 3,4   
                    questionGroup = new List<GameObject>(parent.Child("Squares (1)").Children());
                    questionGroup.AddRange(parent.Child("Squares (2)").Children());
                    answerGroup = new List<GameObject>(parent.Child("Squares (3)").Children());
                    answerGroup.AddRange(parent.Child("Squares (4)").Children());
                    MoveFromTop(parent.Child("Squares (1)"));
                    MoveFromTop(parent.Child("Squares (2)"));
                    MoveFromBottom(parent.Child("Squares (3)"));
                    MoveFromBottom(parent.Child("Squares (4)"));
                    side = "Vertical";
                    break;               
            }
            AddComponentToAnswerGroup();
            AddComponentToQuestionGroup();
        }

        void MoveFromLeft(GameObject obj)
        {
            obj.transform.position = GeneralPos.GetLeftPointOnScreen();
            StartCoroutine(Actions.MoveOverSeconds(obj, new Vector3(0,0,0), 0.2f));           
        }

        void MoveFromRight(GameObject obj)
        {
            obj.transform.position = GeneralPos.GetRightPointOnScreen();
            StartCoroutine(Actions.MoveOverSeconds(obj, new Vector3(0, 0, 0), 0.2f));           
        }

        void MoveFromTop(GameObject obj)
        {
            obj.transform.position = GeneralPos.GetTopPointOnScreen();
            StartCoroutine(Actions.MoveOverSeconds(obj, new Vector3(0, 0, 0), 0.2f));
        }

        void MoveFromBottom(GameObject obj)
        {
            obj.transform.position = GeneralPos.GetBottomPointOnScreen();
            StartCoroutine(Actions.MoveOverSeconds(obj, new Vector3(0, 0, 0), 0.2f));
        }

        void AddComponentToAnswerGroup()
        {
            foreach (GameObject obj in answerGroup)
            {
                obj.AddComponent<Square>();
                obj.AddComponent<BoxCollider2D>();
                obj.GetComponent<Square>().squarePrefab = squareAnimPrefab;
                obj.GetComponent<Square>().rombPrefab = rombAnimPrefab;
                obj.GetComponent<Square>().clickEvent = clickEvent;
                obj.GetComponent<Square>().gameManager = gameManager;
                obj.GetComponent<Square>().questionSpawner = gameManager.questionSpawner;
            }
        }


        void AddComponentToQuestionGroup()
        {
            foreach (GameObject obj in questionGroup)
            {
                obj.AddComponent<SquareQuest>();                
                obj.GetComponent<SquareQuest>().squarePrefab = squareAnimPrefab;               
            }
        }

        public void ChangeSpritesForQuestionGroup()
        {
            foreach (GameObject obj in questionGroup)
            {               
                obj.GetComponent<SpriteRenderer>().sprite = sprites.dot;
            }

        }

        public void HideSquares()
        {           
            parent.Child("Squares (1)").SetActive(false);
            parent.Child("Squares (2)").SetActive(false);
            parent.Child("Squares (3)").SetActive(false);
            parent.Child("Squares (4)").SetActive(false);
        }
    }
}

