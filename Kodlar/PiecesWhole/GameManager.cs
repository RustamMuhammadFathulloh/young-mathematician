using BayatGames.SaveGameFree;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Extension;
using System;

namespace PiecesWhole
{
    public class GameManager : MonoBehaviour
    {
        public Image medalImg;
        public SaveLoadSO saveLoad;
        public LevelSO levelSO;
        public int level;
        public GameObject parentObject;
        public List<GameObject> square16;

        public RectTransform savolBoard;

        public List<Sprite> rectSprites;
        public List<Sprite> circleSprite;
        public List<Sprite> mainSprites;

        public UnityEvent printEvent;
        /// <summary>
        /// Cursor Finger nomli gameObject;
        /// </summary>
        public GameObject fingerCursor;


        private void Awake()
        {
            level = levelSO.level;
            savolBoard.GetComponent<RectTransform>().DOAnchorPosY(150, 0);
            switch (level)
            {
                case 1:
                    mainSprites = circleSprite;
                    break;
                case 2:
                    mainSprites = rectSprites;
                    break;
                default:
                    break;
            }
            TakeAllChildrens(parentObject);
        }


        void Start()
        {
            StartCoroutine(SavolBoardMove());
            StartCoroutine( PrintSquaresRandom());
        }


        /// <summary>
        /// Berilgan obyektdagi barcha child larni listga yig'adi.
        /// </summary>
        public void TakeAllChildrens(GameObject gameObj)
        {
            for (int i = 0; i < gameObj.transform.childCount; i++)
            {
                gameObj.transform.GetChild(i).gameObject.transform.DOScale(0, 0);
                square16.Add(gameObj.transform.GetChild(i).gameObject);
            }
            Shuffling();
        }


        /// <summary>
        /// square16 nomli list obyeklarini aralashtirib beradi.
        /// </summary>
        public void Shuffling()
        {
            square16 = square16.ShuffleList();
        }


        /// <summary>
        /// Random artibda shakllarni chiqaruvchi method.
        /// </summary>
        public IEnumerator PrintSquaresRandom()
        {
            yield return new WaitForSeconds(1);
            for (int i = 0; i < square16.Count; i++)
            {       
                square16[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = mainSprites[i];
                
                square16[i].transform.GetChild(0).DOScale(1, 0.2f);
                square16[i].transform.DOScale(1.08f, 0.6f);
            }
            StartCoroutine(FingerCursorAnim());


            yield return new WaitForSeconds(0.6f);
            for (int i = 0; i < square16.Count; i++)
            {
                square16[i].transform.DOScale(1f, 0.2f);
            }
            
        }

                

        /// <summary>
        /// BoxColliderni o'chirib yoquvchi method
        /// </summary>
        /// <param name="trueFalse"></param>
        public void BoxColliderSwitching(bool trueFalse)
        {
            for (int i = 0; i < square16.Count; i++)
            {
                if (square16[i].GetComponent<PiecesSquare>().boxCollider)                
                    square16[i].GetComponent<BoxCollider2D>().enabled = trueFalse;
            }
        }


        /// <summary>
        /// Savol yozilgan boardni harakatga keltiradi.
        /// </summary>
        /// <returns></returns>
        public IEnumerator SavolBoardMove()
        {
            yield return new WaitForSeconds(0.1f);
            savolBoard.GetComponent<RectTransform>().DOAnchorPosY(-35, 0.6f);
        }


        /// <summary>
        /// O'yin qanday o'ynalishini tushuntirib beruvchi barmoq animatsiyasi uchun method.
        /// </summary>
        /// <returns></returns>
        public IEnumerator FingerCursorAnim()
        {
            Vector3 initialPosFinger = fingerCursor.transform.position;
            float vaqt = 1.6f;

            yield return new WaitForSeconds(1.8f);
            for (int j = 0; j < square16.Count; j++)
            {                
                for (int i = 0; i < square16.Count; i++)
                {
                    BoxColliderSwitching(false);    

                    int leftNumber = ReadNumber(square16[j].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name);
                    string leftName = square16[j].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
                    
                    int rightNumber = ReadNumber(square16[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name);
                    string rightName = square16[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite.name;
                    int javob = leftNumber + rightNumber;
                    if ((i != j) && (javob == 100))
                    {
                        char leftHarf = leftName[2];
                        char rightHarf = rightName[2];
                        if (leftHarf == rightHarf)
                        {
                            fingerCursor.transform.DOMove(square16[j].transform.position, vaqt);
                            yield return new WaitForSeconds(vaqt);
                            fingerCursor.transform.DOScale(0.8f, 0.5f);
                            yield return new WaitForSeconds(0.5f);
                            fingerCursor.transform.DOScale(1f, 0.5f);
                            yield return new WaitForSeconds(0.5f);

                            fingerCursor.transform.DOMove(square16[i].transform.position, vaqt);
                            yield return new WaitForSeconds(vaqt);
                            fingerCursor.transform.DOScale(0.8f, 0.5f);
                            yield return new WaitForSeconds(0.5f);
                            fingerCursor.transform.DOScale(1f, 0.5f);
                            yield return new WaitForSeconds(0.5f);

                            fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.8f);
                            yield return new WaitForSeconds(0.7f);

                            BoxColliderSwitching(true);    
                            yield return new WaitForSeconds(0.7f);
                            fingerCursor.transform.DOMove(initialPosFinger, 0);
                            fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.8f);

                            break;
                        }
                    }
                }
                break;
            }

        }



        /// <summary>
        /// Return tipli method. Sprite nomidagi sonlarni o'qib beradi.
        /// </summary>
        /// <param name="name1">spritenomi</param>
        /// <returns></returns>
        public int ReadNumber(string name1)
        {
            string unlik;       //0 va 1  index dagi sonni aniqlab beradi - Substring(0, 2)
            unlik = name1.Substring(0, 2);
            int son = int.Parse(unlik);

            return son; // Return tipli method
        }


        /// <summary>
        /// Medallarni level scene dagi buttonlar oldiga saqlashga yordam beruvchi method.
        /// </summary>
        public void SaveAndLoadEvent()
        {
            SaveGame.Save<string>(saveLoad.gameName + saveLoad.levels[levelSO.level - 1], medalImg.sprite.name.ToString());
        }


    }
}

