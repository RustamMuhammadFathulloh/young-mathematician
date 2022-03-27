using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionManager;
using UnityEngine.Events;

namespace ShapePattern
{
    public class Car : MonoBehaviour
    {
        public GameManager gm;
        public Pattern pattern;
        public GameObject firstBox;
        public Vector3 startPos;
        public Vector3 initialPos;
        Vector3 rightPos;

        public float sppedInitial;
        public float speedNormal;
        public UnityEvent startEvent;
        public UnityEvent moveEvent;
        public UnityEvent leaveEvent;


        private void Awake()
        {
            InitialPos();

            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0));            
            rightPos = new Vector3(pos.x + GetComponent<SpriteRenderer>().bounds.size.x * 0.5f, transform.position.y, 0);


        }

        private void Start()
        {
            StartCoroutine(GoToStartPos());

        }


        public void InitialPos()
        {
            transform.position = new Vector3(firstBox.transform.position.x + firstBox.GetComponent<SpriteRenderer>().bounds.size.x / 2, transform.position.y, 0);
            startPos = transform.position;
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0));
            transform.position = new Vector3((pos.x * -1) + GetComponent<SpriteRenderer>().bounds.size.x * 0.5f, transform.position.y, 0);
            initialPos = transform.position;
        }


        public void GoToRight(float duration)
        {
            StartCoroutine(MoveToRight(duration));
        }



        void StartGmae()
        {
            if (gm.currentStateNumber < gm.maxStateNumber)
            {                
                pattern.MakePattern();
            }
            else
            {
                
                gm.FinishGame();
            }
        }

        IEnumerator MoveToRight(float duration)
        {
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(Actions.MoveOverSeconds(gameObject, rightPos, duration));
            leaveEvent.Invoke();
            foreach (Whel whel in GetComponentsInChildren<Whel>())
            {
                whel.Move();
            }

            yield return new WaitForSeconds(duration + 0.1f);

            foreach (Whel wheel in GetComponentsInChildren<Whel>())
            {
                wheel.StopBalon();
              
            }

            if (gm.currentStateNumber < gm.maxStateNumber)
            {
                transform.position = initialPos;
                StartCoroutine(GoToStartPos());
                pattern.MakePattern();
            }
            else
            {
                gm.FinishGame();
               
            }

        }

        IEnumerator GoToStartPos()
        {
            startEvent.Invoke();
            yield return new WaitForSeconds(1.25f);
            moveEvent.Invoke();

            foreach (Whel wheel in GetComponentsInChildren<Whel>())
            {
                wheel.Move();
            }
            StartCoroutine(Actions.MoveOverSeconds(gameObject, startPos, sppedInitial));

           
            yield return new WaitForSeconds(sppedInitial);
            foreach (Whel wheel in GetComponentsInChildren<Whel>())
            {
                wheel.StopBalon();
                
            }

        }
    }

}

