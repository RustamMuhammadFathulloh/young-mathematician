using ActionManager;
using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BingoMul
{
    public class Square : MonoBehaviour, IPointerClickHandler
    {
        public bool isDone;
        public GameManager gm;
        public GameObject particle;
        public BingoBugCollectionSO bingoBug;
        public int result;
        public GameEventSO correctEvent;
        public GameEventSO wrongEvent;
        public Sprite correctSprite;

        TMP_Text numberText;
        int maxNumber;
        float degree = 15;
        GameObject bug;
        float val = 0.5f;

        public OperationType operation;
        const string key = "EnumValue";

        private void Awake()
        {
            isDone = false;
            numberText = transform.GetChild(0).gameObject.GetComponent<TMP_Text>();

            string loadString = PlayerPrefs.GetString(key);
            System.Enum.TryParse(loadString, out OperationType loadState);
            operation = loadState;


          
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            CheckResult();
        }

        void CheckResult()
        {
            switch (operation)
            {
                case OperationType.Plus:

                    break;
                case OperationType.Minus:

                    break;
                case OperationType.Multiply:
                    if (result.Equals(GameManager.a * GameManager.b))
                    {
                        CorrectAction();
                    }
                    else
                    {
                        WrongAction();
                    }
                    break;
                case OperationType.Division:
                    if (result.Equals(GameManager.a / GameManager.b))
                    {
                        CorrectAction();
                    }
                    else
                    {
                        WrongAction();
                    }

                    break;
                default:
                    Debug.Log("NOTHING");
                    break;
            }

           
        }

        void CorrectAction()
        {
            isDone = true;
            correctEvent.Raise();
            GetComponent<SpriteRenderer>().sprite = correctSprite;
            numberText.text = "";
            CreateBug();           
        }

        void CreateBug()
        {
            GameObject obj = new GameObject();
            obj.transform.parent = gameObject.transform;
            bug = obj;
            bug.transform.position = transform.position;
            bug.transform.localScale = new Vector3(0, 0, 0);
            bug.AddComponent<SpriteRenderer>().sprite = bingoBug.bugs[Random.Range(0, bingoBug.bugs.Count)];
            bug.GetComponent<SpriteRenderer>().sortingOrder = 1;
            bug.transform.DOScale(0.5f, 0.3f).OnComplete(CreateParticle);       
        }

        void BugAnimation()
        {
            if (val.Equals(0.5f))
            {
                val = 0.4f;
            }
            else
            {
                val = 0.5f;
            }
            bug.transform.DOScale(val, 0.5f).OnComplete(BugAnimation);                   
        }

        void CreateParticle()
        {
            Instantiate(particle, transform.position, Quaternion.identity);
            BugAnimation();
            //NextQuestion();
            if (!gm.IsWin())
            {
                NextQuestion();
            }
            else
            {
                gm.winEvent.Invoke();
            }

        }

        void NextQuestion()
        {           
            gm.questionPanel.ShowCorrect();          
        }

        void WrongAction()
        {
            GameManager.maxChance++;
            wrongEvent.Raise();            
            StartCoroutine(Actions.ActionWrong(this, gameObject));
        }

        void WrongAnim()
        {
            if (maxNumber < 4)
            {
                degree = degree * -1;
                maxNumber++;
                transform.DORotate(new Vector3(0, 0, degree), 0.15f, RotateMode.LocalAxisAdd)
                .SetOptions(true)
                .SetEase(Ease.OutQuint)
                .OnComplete(WrongAnim);
            }

            
        }

    }
}

