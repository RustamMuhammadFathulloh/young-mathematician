using BayatGames.SaveGameFree;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SectionColor
{
    public class GameManager : MonoBehaviour
    {
        public Image medalImg;
        public SaveLoadSO saveLoad;
        public LevelSO levelSO;
        public int level;
        public CalculationManager calManager;

        public List<GameObject> shakllar;
        GameObject shaklObject;

        public List<GameObject> parentSquares;
        public List<GameObject> maxraj2Prefab, maxraj3Prefab, maxraj4Prefab;
        public List<GameObject> mainPrefab;
                

        public TMP_Text stateText;
        public int maxStateNumber;
        public int currentStateNumber;

        public Sprite parentChangeSprite, parentInitialSprite;

        private void Awake()
        {
            level = levelSO.level;
            calManager.level = level;
            parentInitialSprite = parentSquares[0].GetComponent<SpriteRenderer>().sprite;
        }



        void Start()
        {
            switch (level)
            {
                case 1:
                    mainPrefab = maxraj2Prefab;
                    break;
                case 2:
                    mainPrefab = maxraj3Prefab;
                    break;
                case 3:
                    mainPrefab = maxraj4Prefab;
                    break;
                default:
                    break;
            }

            foreach (GameObject item in mainPrefab)
            {
                item.transform.DOScale(0, 0);
            }

            CreatObj();            
        }


        /// <summary>
        /// Markazda joylashgan uchta square ichiga shakl larni creat qiladi.
        /// </summary>
        public void CreatObj()
        {
            shakllar.Clear();
            IncreaseStateNumber();
            
            foreach (GameObject item in parentSquares)
            {                             
                int tasodifiyRaqam = Random.Range(0, mainPrefab.Count);
                
                shaklObject = Instantiate(mainPrefab[tasodifiyRaqam], item.transform.localPosition, Quaternion.identity, item.transform);
                shaklObject.transform.DOScale(0, 0);
                shakllar.Add(shaklObject);
            }
            SwitchBoxCollider(shakllar, true);
            AddCalculationManager(shakllar);
            StartCoroutine(FirstAnimation(shakllar));
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="obyektlar"></param>
        /// <param name="ft"></param>
        public void SwitchBoxCollider(List<GameObject> obyektlar, bool ft)
        {   
            foreach (GameObject item in obyektlar)
            {
                for (int i = 0; i < item.transform.childCount; i++)
                {
                    item.transform.GetChild(i).GetComponent<Collider2D>().enabled = ft;
                }                
            }
        }



        public IEnumerator SwitchCollider2D()
        {
            yield return new WaitForSeconds(0.2f);
            foreach (GameObject item in shakllar)
            {
                for (int i = 0; i < item.transform.childCount; i++)
                {
                    item.transform.GetChild(i).GetComponent<Collider2D>().enabled = false;
                }
            }
            yield return new WaitForSeconds(0.7f);
            foreach (GameObject item in shakllar)
            {
                for (int i = 0; i < item.transform.childCount; i++)
                {
                    item.transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
                }
            }
        }


        /// <summary>
        /// ForEach object scriptidagi camanager nomli classga qiymat berish.
        /// </summary>
        /// <param name="obyektlar"></param>
        public void AddCalculationManager(List<GameObject> obyektlar)
        {
            foreach (GameObject item in obyektlar)
            {
                for (int i = 0; i < item.transform.childCount; i++)
                {
                    item.transform.GetChild(i).GetComponent<ForEachObject>().calManager = calManager;
                }
            }
        }


        public List<int> sonlar;

        /// <summary>
        /// Har bir shaklni tekshirib beruvchi kod.
        /// </summary>
        public void CheckHowManyClicked()
        {            
            int son;
            for (int i = 0; i < shakllar.Count; i++) 
            {
                son = 0;
                for (int j = 0; j < shakllar[i].transform.childCount; j++)
                {
                    son += shakllar[i].transform.GetChild(j).GetComponent<ForEachObject>().pushing;
                }
                sonlar.Add(son);
            }
            calManager.CheckAnswer();
        }



        /// <summary>
        /// shakllarni kattalashtirib kichraytiruvchi method.
        /// </summary>
        public IEnumerator AnimationMinimizeMaximize()
        {
            StartCoroutine(SwitchCollider2D());
            yield return new WaitForSeconds(0.2f);
            foreach (GameObject item in shakllar)
            {
                item.transform.DOScale(1.3f, 0.5f);
            }
            yield return new WaitForSeconds(0.5f);
            foreach (GameObject item in shakllar)
            {
                item.transform.DOScale(0, 0.7f);
            }
            yield return new WaitForSeconds(0.6f);
            for(int i=0; i<parentSquares.Count; i++)
            {
                Destroy(parentSquares[i].transform.GetChild(0).gameObject);
            }
            sonlar.Clear();
            CreatObj();
        }


        public IEnumerator FirstAnimation(List<GameObject> objs)
        {
            foreach (GameObject item in objs)
            {
                item.transform.DOScale(1.2f, 0.7f);
            }
            yield return new WaitForSeconds(0.7f);
            foreach (GameObject item in objs)
            {
                item.transform.DOScale(1f, 0.2f);
            }
        }



        /// <summary>
        /// Yana nechta savol qolganini ko'rsatuvchi state number uchun method.
        /// </summary>
        public void IncreaseStateNumber()
        {
            if (currentStateNumber < maxStateNumber) 
            {
                calManager.MakeQuestion();
                currentStateNumber++;
                stateText.text = currentStateNumber.ToString() + "/" + maxStateNumber.ToString();
            }
            else
            {
                calManager.finishEvent.Invoke();
            }
            
        }



        /// <summary>
        /// Medallarni level scene dagi buttonlar oldiga saqlashga yordam beruvchi method.
        /// </summary>
        public void SaveAndLoadEvent()
        {
            SaveGame.Save<string>(saveLoad.gameName + saveLoad.levels[levelSO.level - 1], medalImg.sprite.name.ToString());
        }


        public void ReturnSizeOfPrefabs()
        {
            foreach (GameObject item in mainPrefab)
            {
                item.transform.DOScale(1, 0);
            }
        }


    }
}
