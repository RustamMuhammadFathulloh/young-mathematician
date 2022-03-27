using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ActionManager;
using UnityEngine.UIElements;

namespace ComposingShape
{
    public class Shape : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public GameEventSO correctEvent;
        public GameEventSO wrongEvent;

        public GameManager gm;
        public Puzzle puzzle;
        Vector3 initialScale;
        Vector3 initialPos;
        [HideInInspector]
        public List<GameObject> children;
        public Color shapeColor;

        public List<GameObject> occupiedShapes;


        Vector3 posOnShelf;
       

        
        

        private void Awake()
        {
            children = Actions.ChildrenOfGameobject(gameObject);
          
            initialPos = transform.position;
            initialScale = transform.localScale;
        }



        private void Start()
        {
            SetPosOnShelf();
            posOnShelf = transform.position;
            SetColor();
            StartCoroutine(AnimateObject(gameObject));

        }

        void SetPosOnShelf()
        {
            float xPos = gm.shelf.transform.position.x - gm.shelf.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
            xPos = xPos + transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x * 0.25f;
            transform.position = new Vector3(xPos, transform.position.y, 0);
        }

        void SetColor()
        {
            Color color = shapeColor;
            color.a = 0.75f;
            foreach (GameObject obj in children)
            {
                obj.GetComponent<SpriteRenderer>().color = color;

            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            

        }

        public void OnDrag(PointerEventData eventData)
        {
            

            transform.localScale = new Vector3(1, 1, 1);
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(pos.x, pos.y, 0);

            List<GameObject> childrenOfClone = Actions.ChildrenOfGameobject(gameObject);
            foreach (GameObject obj in childrenOfClone)
            {
                obj.GetComponent<SpriteRenderer>().sortingOrder = 1;

            }

            if (occupiedShapes.Count > 0)
            {
                puzzle.puzzleShapes.AddRange(occupiedShapes);
                occupiedShapes.Clear();
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Check();
           

        }


        void Check()
        {
            int m = 0;
            List<GameObject> puzzleGroup = new List<GameObject>();
            List<GameObject> shapeGroup = new List<GameObject>();

            foreach (GameObject obj in Actions.ChildrenOfGameobject(gameObject))
            {
                int n = 0;
                foreach (GameObject puzzleShape in puzzle.puzzleShapes)
                {
                    if (Vector3.Distance(puzzleShape.transform.position, obj.transform.position) <= 0.5f)
                    {
                        if (Vector3.Distance(puzzleShape.GetComponent<Triangle>().one.transform.position, obj.GetComponent<Triangle>().one.transform.position) <= 1
                            || Vector3.Distance(puzzleShape.GetComponent<Triangle>().two.transform.position, obj.GetComponent<Triangle>().two.transform.position) <= 1
                            || Vector3.Distance(puzzleShape.GetComponent<Triangle>().three.transform.position, obj.GetComponent<Triangle>().three.transform.position) <= 1)
                        {
                            m++;
                            puzzleGroup.Add(puzzleShape);
                            shapeGroup.Add(obj);
                            if (m.Equals(Actions.ChildrenOfGameobject(gameObject).Count))
                            {
                                //transform.parent = puzzle.gameObject.transform;
                                RePosition(puzzleGroup, shapeGroup);
                                //obj.transform.position = puzzleShape.transform.position;
                                //obj.transform.eulerAngles = puzzleShape.transform.eulerAngles;
                               
                                StartCoroutine(CorrectAction());
                                //puzzleShape.GetComponent<Triangle>().isEnable = false;
                                break;
                            }
                            else
                            {
                                
                                
                            }

                        }
                        else
                        {
                            
                            StartCoroutine(GoToShelf());
                        }
                    }
                    else
                    {
                        n++;
                        if (n.Equals(puzzle.puzzleShapes.Count))
                        {
                            StartCoroutine(GoToShelf());
                           

                            break;
                        }
                        else
                        {
                           

                        }
                    }

                }
            }            
        }

        void RePosition(List<GameObject> puzzleList, List<GameObject> shapeList)
        {
            //obj.transform.position = puzzleShape.transform.position;
            //obj.transform.eulerAngles = puzzleShape.transform.eulerAngles;           
            //puzzleShape.GetComponent<Triangle>().isEnable = false;
            for (int i = 0; i < shapeList.Count; i++)
            {
                shapeList[i].transform.position = puzzleList[i].transform.position;
                shapeList[i].transform.eulerAngles = puzzleList[i].transform.eulerAngles;
                occupiedShapes.Add(puzzleList[i]);
                puzzle.puzzleShapes.Remove(puzzleList[i]);
                //puzzleList[i].GetComponent<Triangle>().isEnable = false;
            }
        }


        IEnumerator CorrectAction()
        {
            correctEvent.Raise();
            
            StartCoroutine(AnimateObject(gameObject));
            List<GameObject> childrenOfClone = Actions.ChildrenOfGameobject(gameObject);
            foreach (GameObject obj in childrenOfClone)
            {
                obj.GetComponent<SpriteRenderer>().sortingOrder = -1;

            }
            yield return new WaitForSeconds(0);
        }


        IEnumerator WrongAction()
        {
            wrongEvent.Raise();
            StartCoroutine(Actions.ActionWrong(this, gameObject));
            yield return new WaitForSeconds(0.6f);
            StartCoroutine(Actions.MoveOverSeconds(gameObject, posOnShelf, 0.25f));
            StartCoroutine(Actions.ScaleOverSeconds(gameObject, initialScale, 0.25f));
            yield return new WaitForSeconds(0.3f);          

        }

        IEnumerator GoToShelf()
        {
            wrongEvent.Raise();
            yield return new WaitForSeconds(0);
            StartCoroutine(Actions.MoveOverSeconds(gameObject, posOnShelf, 0.25f));
            StartCoroutine(Actions.ScaleOverSeconds(gameObject, initialScale, 0.25f));

        }

        IEnumerator AnimateObject(GameObject obj)
        {
            Vector3 initialScale = obj.transform.localScale;
            StartCoroutine(Actions.ScaleOverSeconds(obj, initialScale * 1.25f, 0.2f));
            yield return new WaitForSeconds(0.2f);
            StartCoroutine(Actions.ScaleOverSeconds(obj, initialScale, 0.2f));
            yield return new WaitForSeconds(0.2f);

        }



    }

}

