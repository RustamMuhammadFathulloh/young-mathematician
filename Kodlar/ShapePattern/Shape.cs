using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ActionManager;

namespace ShapePattern
{
    public class Shape : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {

        public Pattern pattern;
        Vector3 initialPos;
        Vector3 initialScale;


        private void Start()
        {
            initialPos = transform.position;
            initialScale = transform.localScale;
        }



        public void OnBeginDrag(PointerEventData eventData)
        {
           
            
            
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(pos.x, pos.y, 0);
            GetComponent<SpriteRenderer>().sortingOrder = 2;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Check();


        }


        IEnumerator CorrectAtion(GameObject box)
        {
            box.GetComponent<Box>().trafficCone.GetComponent<SpriteRenderer>().sprite = null;
            GetComponent<BoxCollider2D>().enabled = false;
            transform.position = box.transform.position;
            StartCoroutine(Actions.ScaleOverSeconds(gameObject, new Vector3(1, 1, 1), 0.25f));
            yield return new WaitForSeconds(0.27f);
            box.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = GetComponent<SpriteRenderer>().sprite;            
            transform.position = initialPos;
            GetComponent<BoxCollider2D>().enabled = true;
            transform.localScale = initialScale;
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            pattern.correct++;
            if (pattern.correct.Equals(pattern.random))
            {
                pattern.car.GoToRight(6);
            }
        }

        IEnumerator WrongAction()
        {
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(Actions.ActionWrong(this , gameObject));
            yield return new WaitForSeconds(0.6f);
            StartCoroutine(Actions.MoveOverSeconds(gameObject, initialPos, 0.25f));
            yield return new WaitForSeconds(0.25f);
            GetComponent<BoxCollider2D>().enabled = true;
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            //transform.localScale = initialScale;

        }

        IEnumerator GoHome()
        {
            GetComponent<BoxCollider2D>().enabled = false;                       
            StartCoroutine(Actions.MoveOverSeconds(gameObject, initialPos, 0.25f));
            yield return new WaitForSeconds(0.25f);
            GetComponent<BoxCollider2D>().enabled = true;         
            GetComponent<SpriteRenderer>().sortingOrder = 0;
            //transform.localScale = initialScale;
        }

        void Check()
        {
            int n = 0;
            foreach (GameObject obj in Actions.ChildrenOfGameobject(pattern.boxParent))
            {
                if (Vector3.Distance(gameObject.transform.position, obj.transform.position) <= 1 
                    && obj.GetComponent<Box>().shapeName.Equals(GetComponent<SpriteRenderer>().sprite.name)
                    && obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite == null)
                {                    
                    // Correct
                    pattern.correctEvent.Invoke();
                    StartCoroutine(CorrectAtion(obj));
                    break;                  
                }
                else if (Vector3.Distance(gameObject.transform.position, obj.transform.position) <= 1 
                    && obj.GetComponent<Box>().shapeName != GetComponent<SpriteRenderer>().sprite.name
                    && obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite == null)
                {
                   
                    // Wrong
                    pattern.wrongEvent.Invoke();
                    StartCoroutine(WrongAction());
                    break;
                }
                else
                {
                    n++;                   
                }

                //if (obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite == null)
                //{
                    
                //}
                //else
                //{
                //    Debug.Log("Boxni ichida rasm bo'lsa");
                //    // Go Home
                //    //pattern.goHomeEvent.Invoke();
                //    //StartCoroutine(GoHome());
                //}

            }

            if (n.Equals(Actions.ChildrenOfGameobject(pattern.boxParent).Count))
            {
                Debug.Log("Box bilan orasi uzoq");
                // Go Home
                pattern.goHomeEvent.Invoke();
                StartCoroutine(GoHome());
            }
        }
    }

}

