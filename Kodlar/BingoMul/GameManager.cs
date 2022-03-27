using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using BayatGames.SaveGameFree;

namespace BingoMul
{
    public enum OperationType
    {
        Plus,
        Minus,
        Multiply,
        Division,
    }

    public class GameManager : MonoBehaviour
    {
        public Image medalImg;
        public SaveLoadSO saveLoad;
        public SaveLoadSO saveLoadDiv;

        [EnumPaging]
        public OperationType operation;
        const string key = "EnumValue";
        public QuestionPanel questionPanel;
        public GameObject bigSquare;
        public LevelSO level;
        public List<GameObject> bigSquares;
        public List<int> doneSquares = new List<int>();
        public List<Sprite> bingoSprites;

        public static string questionStr;
        public static int a;
        public static int b;      
        public static int maxChance;        
        public int randomIndex;        
        public List<int[]> questionCollection = new List<int[]>();
        public List<int[]> bingoIndexCollection = new List<int[]>();

        public UnityEvent questionEvent;
        public UnityEvent gameOverEvent;
        public UnityEvent winEvent;
        public UnityEvent finishEvent;

        int size;

        private void Awake()
        {
            maxChance = 0;
            a = 0;
            b = 0;
            LoadEnum();

            //Agar FingerAnimatsiyasi olib tashlansa pastdagi kod ham olib tashlanishi kerak.
            DisableColliders(false);        // FingerAnim yuzberayotgan paytda BoxColliderni o'chiradi.
        }

        void LoadEnum()
        {
            string loadString = PlayerPrefs.GetString(key);
            Debug.Log(loadString);
            System.Enum.TryParse(loadString, out OperationType loadState);
            Debug.Log(loadState);
            operation = loadState;
            SetOperation();
        }

        int  MakeLevelObjects()
        {
            if (level.level.Equals(1))
            {
                bigSquare = Instantiate(bigSquares[0]);
                size = 3;
                return 9;
            }
            else if (level.level.Equals(2))
            {
                bigSquare = Instantiate(bigSquares[1]);
                size = 4;
                return 16;
            }
            else
            {
                bigSquare = Instantiate(bigSquares[2]);
                size = 5;
                return 25;
            }
                        
        }

        void SetOperation()
        {
            switch (operation)
            {
                case OperationType.Plus:
                    
                    break;
                case OperationType.Minus:
                    
                    break;
                case OperationType.Multiply:
                    MakeRandomMultiplyQuestions();
                    break;
                case OperationType.Division:
                    MakeRandomDivisionQuestions();
                    break;
                default:
                    Debug.Log("NOTHING");
                    break;
            }
        }

        void MakeRandomMultiplyQuestions()
        {
            int max = MakeLevelObjects();
            MakeBingoIndexCollection();
            MakeMultiplyQuestionCollection(max);
            SetNumbers();
            CallQuestionEvent();
        }

        void MakeRandomDivisionQuestions()
        {
            int max = MakeLevelObjects();
            MakeBingoIndexCollection();
            MakeMultiplyQuestionCollection(max);
            List<int[]> questionCollection1 = new List<int[]>(questionCollection);
            questionCollection.Clear();
            foreach (int [] numbers in questionCollection1)
            {
                int pick = numbers[Random.Range(0, numbers.Length)];
                int[] nums = { numbers[0] * numbers[1], pick };
                questionCollection.Add(nums);                
            }
            SetNumbersDivision();
            CallQuestionEvent();
        }


        void MakeMultiplyQuestionCollection(int max)
        {
            for (int i = 0; i < max; i++)
            {
                int a = Random.Range(1, 9);
                int b = Random.Range(1, 9);
                int[] arrayNum = { a, b };
                questionCollection.Add(arrayNum);
            }
        }

        void SetNumbers()
        {
            int n = 0;
            foreach (GameObject obj in bigSquare.GetComponent<BigSquare>().children)
            {
                int[] array = questionCollection[n]; 
                obj.GetComponent<Square>().result = array[0] * array[1];
                obj.GetComponent<Square>().gm = this;
                obj.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = (array[0] * array[1]).ToString();
                n++;
            }
        }

        void SetNumbersDivision()
        {
            int n = 0;
            foreach (GameObject obj in bigSquare.GetComponent<BigSquare>().children)
            {
                int[] array = questionCollection[n];
                obj.GetComponent<Square>().result = array[0] / array[1];
                obj.GetComponent<Square>().gm = this;
                obj.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = (array[0] / array[1]).ToString();
                n++;
            }
        }


        public void CheckGameOver()
        {
            if (maxChance.Equals(5))
            {
                DisableColliders(false);
                StartCoroutine(WaitForSecond(0.9f));                
            }
        }

        public bool IsWin()
        {
            bool val = false;
            int n = 0;            
            doneSquares.Clear();
            foreach (GameObject obj in bigSquare.GetComponent<BigSquare>().children)
            {
                if (obj.GetComponent<Square>().isDone)
                {
                    doneSquares.Add(n);
                }
                n++;
            }
            if (doneSquares.Count >= size)
            {               
                foreach (int[] collection in bingoIndexCollection)
                {                                       
                    if (!collection.Except(doneSquares).Any())
                    {
                        val = true;
                        FindBingoPositions(collection);
                        break;
                    }                              
                }                
            }
            else
            {
                val = false;                
            }
            return val;
        }


        public void FindBingoPositions(int [] arr)
        {            
            List<GameObject> bingoObjects = new List<GameObject>();
            for (int i = 0; i < arr.Length; i++)
            {
                int k = 0;
                foreach (GameObject obj in bigSquare.GetComponent<BigSquare>().children)
                {
                    if (arr[i].Equals(k))
                    {
                        bingoObjects.Add(obj);
                    }
                    k++;
                }
            }
            List<Vector3> bingoPositions = new List<Vector3>();            
            float t = 0f;
            for (int i = 0; i < 5; i++)
            {
                Vector3 pos = Vector3.Lerp(bingoObjects[0].transform.position, bingoObjects.Last().transform.position, t);
                bingoPositions.Add(pos);
                t += 0.25f;
            }

            int n = size - 1;
            int m = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Equals(n))
                {
                    m++;
                }
                n = n + (size - 1);                
            }
            if (m.Equals(arr.Length))
            {
                bingoPositions.Reverse();
            }
            DisplayBingoSprites(bingoPositions);
           
        }

        void DisplayBingoSprites(List<Vector3> positions)
        {
            int k = 0;
            int order = 2;
            foreach (Vector3 pos in positions)
            {
                GameObject obj = new GameObject();
                obj.transform.position = pos;
                obj.AddComponent<SpriteRenderer>();
                obj.GetComponent<SpriteRenderer>().sortingOrder = order;
                obj.GetComponent<SpriteRenderer>().sprite = bingoSprites[k];
                obj.transform.localScale = new Vector3(0, 0, 0);
                obj.transform.DOScale(0.4f, 1.3f).WaitForCompletion();
                
                k++;
                order++;
            }
            StartCoroutine(WaitFor());
        }

        IEnumerator WaitFor()
        {
            yield return new WaitForSeconds(2.5f);
            finishEvent.Invoke();
        }


        void MakeBingoIndexCollection()
        {
            int aSize = size;
            List<int> collection = new List<int>();
            for (int i = 0; i < bigSquare.GetComponent<BigSquare>().children.Count; i++)
            {
                if (i.Equals(aSize))
                {
                    bingoIndexCollection.Add(collection.ToArray());
                    collection.Clear();
                    aSize = aSize + size;
                }
                collection.Add(i);
            }

            bingoIndexCollection.Add(collection.ToArray());
            collection.Clear();

            for (int i = 0; i < bigSquare.GetComponent<BigSquare>().children.Count; i+=size+1)
            {
                collection.Add(i);               
            }
            bingoIndexCollection.Add(collection.ToArray());
            collection.Clear();

            for (int i = size - 1; i < bigSquare.GetComponent<BigSquare>().children.Count-1; i += size - 1)
            {
                collection.Add(i);
            }
            bingoIndexCollection.Add(collection.ToArray());
            collection.Clear();

            for (int i = 0; i < size; i++)
            {
                for (int j = i; j < bigSquare.GetComponent<BigSquare>().children.Count; j+= size)
                {
                    collection.Add(j);
                }
                bingoIndexCollection.Add(collection.ToArray());
                collection.Clear();
            }

            //foreach (int[] Acollection in bingoIndexCollection)
            //{                
            //    for (int i = 0; i < Acollection.Length; i++)
            //    {
            //        Debug.Log(Acollection[i]);
            //    }
            //    Debug.Log("____________________________");
            //}
        }

        public void DisableColliders(bool val)
        {            
            foreach (GameObject obj in bigSquare.GetComponent<BigSquare>().children)
            {
                obj.GetComponent<BoxCollider2D>().enabled = val;
            }
        }

        IEnumerator WaitForSecond(float duration)
        {
            yield return new WaitForSeconds(duration);
            gameOverEvent.Invoke();

        }

        public void CallQuestionEvent()
        {
            questionEvent.Invoke();
        }

        public void MakeQuestion()
        {
            randomIndex = Random.Range(0, questionCollection.Count); 
            int[] array = questionCollection[randomIndex];  
            a = array[0];
            b = array[1];

            switch (operation)
            {
                case OperationType.Plus:

                    break;
                case OperationType.Minus:

                    break;
                case OperationType.Multiply:
                    questionStr = array[0].ToString() + " x " + array[1].ToString() + " =";
                    break;
                case OperationType.Division:
                    questionStr = array[0].ToString() + " ÷ " + array[1].ToString() + " =";
                    break;
                default:
                    Debug.Log("NOTHING");
                    break;
            }

            StartCoroutine(FingerCursorAnim());
        }

        public void RemoveItemFromDictionary()
        {            
            questionCollection.RemoveAt(randomIndex);            
        }



        public GameObject fingerCursor;
        int finn=0;

        /// <summary>RMF
        /// Bingo uchun finger Cursor animatsiyasi.
        /// </summary>
        /// <returns></returns>
        public IEnumerator FingerCursorAnim()
        {
            if (finn == 0)
            {
                //DisableColliders(false);        // FingerAnim yuzberayotgan paytda BoxColliderni o'chiradi.
                finn = 5;
                float vaqt = 1.6f;
                yield return new WaitForSeconds(vaqt);
                Vector3 initialPosFinger = fingerCursor.GetComponent<Transform>().position;
                Vector3 pos = bigSquare.GetComponent<BigSquare>().children[randomIndex].GetComponent<Transform>().position;
                Debug.Log(pos);
                fingerCursor.transform.DOMove(pos, vaqt);
                yield return new WaitForSeconds(vaqt);
                fingerCursor.transform.DOScale(0.8f, 0.5f);
                yield return new WaitForSeconds(0.5f);
                fingerCursor.transform.DOScale(1f, 0.5f);
                yield return new WaitForSeconds(0.5f);

                fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.8f);
                yield return new WaitForSeconds(0.5f);
                fingerCursor.transform.DOMove(initialPosFinger, 0);
                fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.8f);

                DisableColliders(true);  // FingerAnim tugagach BoxColliderni qo'shadi.
            }            
        }



        public void SaveAndLoadEvent()
        {
            switch (operation)
            {
                case OperationType.Plus:

                    break;
                case OperationType.Minus:

                    break;
                case OperationType.Multiply:
                    SaveGame.Save<string>(saveLoad.gameName + saveLoad.levels[level.level - 1], medalImg.sprite.name.ToString());
                    break;
                case OperationType.Division:
                    SaveGame.Save<string>(saveLoadDiv.gameName + saveLoadDiv.levels[level.level - 1], medalImg.sprite.name.ToString());
                    break;
                default:
                    Debug.Log("NOTHING");
                    break;
            }
           
        }

    }
}

