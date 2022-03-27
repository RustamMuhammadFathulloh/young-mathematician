using BayatGames.SaveGameFree;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Multiplication
{
    public class GameManager : MonoBehaviour
    {
        public Image medalImg;
        public SaveLoadSO saveLoad;
        public LevelSO levelSO;
        [HideInInspector]
        public int level;
        int limitMax, limitMin;

        public List<GameObject> squares;

        /// <summary>
        ///  Raqamlar uchun sprite lardan iborat list.
        /// </summary>
        public List<Sprite> numberSprites;
        //[HideInInspector]
        public List<int> numbers;

        public float duration;
        public GameObject parent;
        public Calculation calculation;

        public UnityEvent startEvent;
        public UnityEvent squareMaximizeEvent;
        

        public GameObject fingerCursor;
        public int sonIndex1, sonIndex2;

        public int sonVal1, sonVal2;

        private void Awake()
        {
            level = levelSO.level;
            RandomByLevel();
        }

                
        void Start()
        {            
            GenerateRandomNumbers();            
            StartCoroutine(Hurricane());
        }



        /// <summary>
        /// Level bo'yicha sonlarni Random orqali tanlaydi.
        /// </summary>
        void GenerateRandomNumbers()
        {
            var random = new System.Random();
            limitMin = 1;

            foreach (GameObject obj in squares)
            {               
                int num = random.Next(limitMin, limitMax);             
                numbers.Add(num);
                obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = numberSprites[num];
                obj.GetComponent<Square>().squareNumber = num;
                obj.GetComponent<SpriteRenderer>().enabled = true;
            }
        }
        
        

        /// <summary>
        /// Levelga qarab maxLimitni tanlash.
        /// </summary>
        public void RandomByLevel()
        {
            if (level == 1 || level != 2 && level != 3) 
            {
                limitMax = 6;
            }
            else if (level == 2)
            {
                limitMax = 8;
            }
            else if (level == 3) 
            {
                limitMax = 10;
            }
        }


        /// <summary>
        /// Random yordamida sonlarni berish.
        /// </summary>
        public IEnumerator Hurricane()
        {
            foreach (GameObject obj in squares)
            {
                yield return new WaitForSeconds(duration);
                squareMaximizeEvent.Invoke();
                obj.GetComponent<Square>().MaximazeSquare();        
                
            }            
            
            startEvent.Invoke();            
            StartCoroutine(FingerCursorAnim());
        }


        /// <summary>
        /// Square larga Box Collider2d ni true qiymatini beradi.
        /// </summary>
        public void ReSetBoxCollider()
        {            
            foreach (GameObject obj in squares)
            {
                obj.GetComponent<BoxCollider2D>().enabled = true;

            }
        }


        /// <summary>
        /// Box Colliderni yoqib o'chirish.
        /// </summary>
        /// <param name="val"></param>
        public void BoxColliderEnable(bool val)
        {
            foreach (GameObject obj in squares)
            {
                obj.GetComponent<BoxCollider2D>().enabled = val;
            }
        }


        public void InitialSprite()
        {
            foreach (GameObject obj in squares)
            {
                obj.GetComponent<Square>().ReturnSprite();
            }
            
        }


        /// <summary>
        /// obyektni va sonni listdan o'chirish 1-usul
        /// </summary>
        public void CleanList()
        {
            if (calculation.clickedNumbers.Count >= 2)
            {
                for (int i = 0; i < calculation.clickedNumbers.Count; i++)
                {
                    numbers.Remove(calculation.clickedNumbers[i]);
                    squares.Remove(calculation.clickedObjects[i]);
                    Destroy(calculation.clickedObjects[i]);
                    
                } 
            }
        }


        Vector3 vec1, vec2;
        public int j;
        /// <summary>
        /// Multiplication Game uchun FingerCursor Animatsiyasi.
        /// </summary>
        /// <returns></returns>
        public IEnumerator FingerCursorAnim()
        {
            BoxColliderEnable(false);
            Vector3 initialPosFinger = fingerCursor.transform.position;
            float vaqt = 1.6f;
            yield return new WaitForSeconds(1.5f);

            
            for (int i = 0; i < squares.Count; i++)
            {
                if (squares[i].GetComponent<Square>().squareNumber == sonVal1)
                {
                    vec1 = squares[i].transform.position;
                    j = i;
                    break;
                }
            }

            for (int i = 0; i < squares.Count; i++)
            {
                if ((squares[i].GetComponent<Square>().squareNumber == sonVal2) && (j != i))
                {
                    vec2 = squares[i].transform.position;
                    break;
                }
            }




            fingerCursor.transform.DOMove( vec1, vaqt);
            yield return new WaitForSeconds(vaqt);

            fingerCursor.transform.DOScale(0.8f, 0.5f);
            yield return new WaitForSeconds(0.5f);
            fingerCursor.transform.DOScale(1f, 0.5f);
            yield return new WaitForSeconds(0.5f);

            fingerCursor.transform.DOMove(vec2, vaqt - 0.6f);
            yield return new WaitForSeconds(vaqt - 0.6f);
            
            fingerCursor.transform.DOScale(0.8f, 0.4f);
            yield return new WaitForSeconds(0.4f);
            fingerCursor.transform.DOScale(1f, 0.4f);
            yield return new WaitForSeconds(0.4f);

            BoxColliderEnable(true);
            fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.5f);
            yield return new WaitForSeconds(1f);
            fingerCursor.transform.DOMove(initialPosFinger, 0);
            fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.8f);            
            
            Debug.Log(sonIndex1 + " " + sonIndex2);
        }


        

        public void SaveAndLoadEvent()
        {
            SaveGame.Save<string>(saveLoad.gameName + saveLoad.levels[levelSO.level - 1], medalImg.sprite.name.ToString());
        }


    }
}

