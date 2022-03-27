using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using ActionManager;
using UnityEngine.UI;

namespace FruitSplat
{
    public class Fruit : MonoBehaviour, IPointerClickHandler
    {
        public GameManager gm;
        public bool isCorrect;
        public GameEventSO correctEvent;
        public GameEventSO wrongEvent;
        
        public Vector3 lastDestination;
        Vector3 pos;

        Color operationColor;
     


        private void Awake()
        {
            pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            pos = new Vector3(pos.x-1, pos.y-2, 0);
            transform.position = new Vector3(Random.Range(-pos.x, pos.x), Random.Range(-pos.y, pos.y), 0);
            operationColor = transform.GetChild(0).GetComponent<SpriteRenderer>().color;

        }

        private void Start()
        {            
            StartCoroutine(MoveAround());            
        }

        public IEnumerator MoveAround()
        {            
            float posX = Random.Range(-pos.x, pos.x-1);
            float posY = Random.Range(-pos.y -1, pos.y);
            lastDestination = new Vector3(posX, posY, 0);
            StartCoroutine(Actions.MoveOverSeconds(gameObject, lastDestination,  gm.fruitSpeed));
            yield return new WaitForSeconds(gm.fruitSpeed);
            StartCoroutine(MoveAround());
        }

      



        public void OnPointerClick(PointerEventData eventData)
        {           
            CheckIsCorrect();
        }


        void CheckIsCorrect()
        {
            if (isCorrect)
            {
                correctEvent.Raise();
                CreateParticle(gm.correctParticle);
                StartCoroutine(CorrectAction());               
            }
            else
            {
                gm.life.transform.GetChild(gm.wrong).GetComponent<Image>().enabled = false;
                gm.wrong++;
                wrongEvent.Raise();
                CreateParticle(gm.wrongParticle);                
            }
        }


        void CreateParticle(GameObject obj)
        {
            Instantiate(obj, transform.position, Quaternion.identity);
        }

        IEnumerator CorrectAction()
        {
            StartCoroutine(Actions.FadeOverTime(GetComponent<SpriteRenderer>(), 0, 0));
            StartCoroutine(Actions.FadeOverTime(transform.GetChild(0).GetComponent<SpriteRenderer>(), 0, 0));            
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Actions.FadeOverTime(GetComponent<SpriteRenderer>(), 1, 0));
            StartCoroutine(Actions.FadeOverTime(transform.GetChild(0).GetComponent<SpriteRenderer>(), 1, 0));
            transform.GetChild(0).GetComponent<SpriteRenderer>().color = operationColor;
        }
       

    }
}

