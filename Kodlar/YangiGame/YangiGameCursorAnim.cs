using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace YangiGame
{
    public class YangiGameCursorAnim : MonoBehaviour
    {
        public string sceneName;
        public LevelSO levelSO;
        public TMP_Text questionText;

        public Vector3 initialPosFinger;
        public GameObject fingerCursor;
        public GameObject mainBox, box2, box3;

        public UnityEvent correctEvent;
        public GameObject correctParticle;

        public List<GameObject> numberObjects;
        public List<Vector3> numberObjPos;

        public List<Sprite> Numbers;
        public Sprite qizilSprite;
        public Sprite yashilSprite;

        public int initialOrder1;
        public int initialOrder2;
        public int movingOrder1 = 4;
        public int movingOrder2 = 5;



        public void Awake()
        {
            initialPosFinger = fingerCursor.transform.position;
            switch (levelSO.level)
            {
                case 1:
                    mainBox = box2;
                    questionText.text = "34";
                    break;
                case 2:
                    mainBox = box3;
                    questionText.text = "57";
                    break;
                default:
                    break;
            }
        }


        void Start()
        {
            MainBoxSwitch();

            if (mainBox.transform.childCount == 2)
            {
                StartCoroutine(CursorAnimLevel1());
            }
            else if (mainBox.transform.childCount == 3)
            {
                StartCoroutine(CursorAnimLevel2());
            }
        }

        
        /// <summary>
        /// MainBox dagi obyektlarni yoquvchi method.
        /// </summary>
        void MainBoxSwitch()
        {
            for (int i = 0; i < numberObjects.Count; i++)
            {
                numberObjects[i].GetComponent<BoxCollider2D>().enabled = false;
                Vector3 vecPos = numberObjects[i].transform.position;
                numberObjPos.Add(vecPos);
            }
            mainBox.SetActive(true);
            
        }


        /// <summary>
        /// CursorAnimatsiyasi Level 1 uchun.
        /// </summary>
        /// <returns></returns>
        IEnumerator CursorAnimLevel1()
        {
            float time = 1.2f;
            yield return new WaitForSeconds(1f);

            //  O'nliklarni qutiga teradi.
            for (int i = 0; i < 3; i++)
            {
                fingerCursor.transform.DOMove(numberObjects[0].transform.position, time + 0.2f);
                yield return new WaitForSeconds(time + 0.2f);

                fingerCursor.transform.DOScale(0.8f, 0.4f);
                yield return new WaitForSeconds(0.4f);

                fingerCursor.transform.DOMove(mainBox.transform.GetChild(0).position, time - 0.2f);
                
                initialOrder1 = numberObjects[0].GetComponent<SpriteRenderer>().sortingOrder;
                initialOrder2 = numberObjects[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder;
                numberObjects[0].GetComponent<SpriteRenderer>().sortingOrder = movingOrder1;
                numberObjects[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = movingOrder2;
                numberObjects[0].transform.DOMove(mainBox.transform.GetChild(0).position, time - 0.2f);
                yield return new WaitForSeconds(time - 0.2f);

                mainBox.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Numbers[i + 1];
                mainBox.transform.GetChild(0).transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Numbers[0];

                numberObjects[0].transform.DOMove(numberObjPos[0], 0.1f);
                numberObjects[0].GetComponent<SpriteRenderer>().sortingOrder = initialOrder1;
                numberObjects[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = initialOrder2;
                Instantiate(correctParticle, mainBox.transform.GetChild(0).position, Quaternion.identity);
                fingerCursor.transform.DOScale(1f, 0.3f);
                yield return new WaitForSeconds(0.3f);

            }

            mainBox.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = yashilSprite;


            // Ikkinchi son uchun
            initialOrder1 = numberObjects[7].GetComponent<SpriteRenderer>().sortingOrder;
            initialOrder2 = numberObjects[7].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder;
            fingerCursor.transform.DOMove(numberObjects[6].transform.position, time);
            yield return new WaitForSeconds(time);

            fingerCursor.transform.DOScale(0.8f, 0.4f);
            yield return new WaitForSeconds(0.4f);

            numberObjects[6].GetComponent<SpriteRenderer>().sortingOrder = movingOrder1;
            numberObjects[6].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = movingOrder2;
            fingerCursor.transform.DOMove(mainBox.transform.GetChild(1).position, time - 0.2f);
            numberObjects[6].transform.DOMove(mainBox.transform.GetChild(1).position, time - 0.2f);
            yield return new WaitForSeconds(time - 0.2f);

            numberObjects[6].transform.DOMove(numberObjPos[6], 0.1f);
            numberObjects[6].GetComponent<SpriteRenderer>().sortingOrder = initialOrder1;
            numberObjects[6].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = initialOrder2;
            Instantiate(correctParticle, mainBox.transform.GetChild(1).position, Quaternion.identity);
            fingerCursor.transform.DOScale(1f, 0.3f);
            yield return new WaitForSeconds(0.3f);

            mainBox.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Numbers[4];
            mainBox.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = yashilSprite;

            fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.5f);
            yield return new WaitForSeconds(1f);
            fingerCursor.transform.DOMove(initialPosFinger, 0);
            fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.8f);

            LoadingGameScene(sceneName);
        }


        /// <summary>
        /// Cursor Animatsiyasi Level2 uchun.
        /// </summary>
        /// <returns></returns>
        IEnumerator CursorAnimLevel2()
        {
            float time = 1.2f;
            
            yield return new WaitForSeconds(1.1f);
            
            for (int i = 0; i < 5; i++)
            {
                fingerCursor.transform.DOMove(numberObjects[0].transform.position, time+0.2f);
                yield return new WaitForSeconds(time+0.2f);

                fingerCursor.transform.DOScale(0.8f, 0.4f);
                yield return new WaitForSeconds(0.4f);

                fingerCursor.transform.DOMove(mainBox.transform.GetChild(0).position, time-0.2f);
                initialOrder1 = numberObjects[0].GetComponent<SpriteRenderer>().sortingOrder;
                initialOrder2 = numberObjects[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder;
                numberObjects[0].GetComponent<SpriteRenderer>().sortingOrder = movingOrder1;
                numberObjects[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = movingOrder2;
                numberObjects[0].transform.DOMove(mainBox.transform.GetChild(0).position, time-0.2f);
                yield return new WaitForSeconds(time-0.2f);

                mainBox.transform.GetChild(0).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Numbers[i + 1];
                mainBox.transform.GetChild(0).transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = Numbers[0];

                numberObjects[0].transform.DOMove(numberObjPos[0], 0.1f);
                numberObjects[0].GetComponent<SpriteRenderer>().sortingOrder = initialOrder1;
                numberObjects[0].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = initialOrder2;
                Instantiate(correctParticle, mainBox.transform.GetChild(0).position, Quaternion.identity);
                fingerCursor.transform.DOScale(1f, 0.3f);
                yield return new WaitForSeconds(0.3f);

                correctEvent.Invoke();
            }
            mainBox.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = yashilSprite;


            // Ikkinchi son uchun
            initialOrder1 = numberObjects[7].GetComponent<SpriteRenderer>().sortingOrder;
            initialOrder2 = numberObjects[7].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder;

            fingerCursor.transform.DOMove(numberObjects[7].transform.position, time);
            yield return new WaitForSeconds(time);
            fingerCursor.transform.DOScale(0.8f, 0.4f);
            yield return new WaitForSeconds(0.4f);

            numberObjects[7].GetComponent<SpriteRenderer>().sortingOrder = movingOrder1;
            numberObjects[7].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = movingOrder2;
            fingerCursor.transform.DOMove(mainBox.transform.GetChild(1).position, time - 0.2f);
            numberObjects[7].transform.DOMove(mainBox.transform.GetChild(1).position, time - 0.2f);
            yield return new WaitForSeconds(time - 0.2f);

            correctEvent.Invoke();

            numberObjects[7].transform.DOMove(numberObjPos[7], 0.1f);
            numberObjects[7].GetComponent<SpriteRenderer>().sortingOrder = initialOrder1;
            numberObjects[7].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = initialOrder2;
            Instantiate(correctParticle, mainBox.transform.GetChild(1).position, Quaternion.identity);
            fingerCursor.transform.DOScale(1f, 0.3f);
            yield return new WaitForSeconds(0.3f);

            mainBox.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Numbers[3];
            mainBox.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = yashilSprite;


            // Uchinchi son uchun
            initialOrder1 = numberObjects[6].GetComponent<SpriteRenderer>().sortingOrder;
            initialOrder2 = numberObjects[6].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder;
            fingerCursor.transform.DOMove(numberObjects[6].transform.position, time);
            yield return new WaitForSeconds(time);

            fingerCursor.transform.DOScale(0.8f, 0.4f);
            yield return new WaitForSeconds(0.4f);

            numberObjects[6].GetComponent<SpriteRenderer>().sortingOrder = movingOrder1;
            numberObjects[6].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = movingOrder2;
            fingerCursor.transform.DOMove(mainBox.transform.GetChild(2).position, time - 0.2f);
            numberObjects[6].transform.DOMove(mainBox.transform.GetChild(2).position, time - 0.2f);
            yield return new WaitForSeconds(time - 0.2f);

            correctEvent.Invoke();

            numberObjects[6].transform.DOMove(numberObjPos[6], 0.1f);
            numberObjects[6].GetComponent<SpriteRenderer>().sortingOrder = initialOrder1;
            numberObjects[6].transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = initialOrder2;
            Instantiate(correctParticle, mainBox.transform.GetChild(2).position, Quaternion.identity);
            fingerCursor.transform.DOScale(1f, 0.3f);
            yield return new WaitForSeconds(0.3f);

            mainBox.transform.GetChild(2).transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Numbers[4];
            mainBox.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = yashilSprite;

            fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.5f);
            yield return new WaitForSeconds(1f);
            fingerCursor.transform.DOMove(initialPosFinger, 0);
            fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.8f);

            LoadingGameScene(sceneName);
        }



        /// <summary>
        /// Kerakli scene ni yuklab beruvchi kod.
        /// </summary>
        /// <param name="sceneName"></param>
        public void LoadingGameScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

    }
}
