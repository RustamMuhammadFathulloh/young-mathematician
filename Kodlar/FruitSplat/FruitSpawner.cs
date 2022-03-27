using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace FruitSplat
{
    public class FruitSpawner : MonoBehaviour
    {
        public Sprite[] operationSprites;
        public GameManager gm;
        public Sprite[] sprites;
        public GameObject[] fruitPrefabs;
        public List<GameObject> fruits;
        public GameEventSO correctEvent;
        public GameEventSO wrongEvent;



        GameObject inActiveFruit;
        int n = 0;

        private void Start()
        {
            n = 0;
            CreateFruits();
        }

        void CreateFruits()
        {            
            foreach (GameObject obj in fruitPrefabs)
            {
                InstantiateFruitPrefab(obj);
                InstantiateFruitPrefab(obj);
            }
            GiveOperationToFruits();
           
        }

        void InstantiateFruitPrefab(GameObject obj)
        {
            GameObject anObj = Instantiate(obj, obj.transform.position, Quaternion.identity);
            anObj.GetComponent<Fruit>().gm = gm;
            anObj.GetComponent<Fruit>().correctEvent = correctEvent;
            anObj.GetComponent<Fruit>().wrongEvent = wrongEvent;    
            fruits.Add(anObj);
            SortOrderLayer(anObj);
        }

        void GiveOperationToFruits()
        {
            int k = 0;            
            for (int i = 0; i < fruits.Count; i++)
            {
                if (k.Equals(3))
                {
                    k = 0;
                }
                fruits[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = operationSprites[k];
                k++;
            }            
        }

        void SortOrderLayer(GameObject obj)
        {
            obj.GetComponent<SpriteRenderer>().sortingOrder = n;
            n++;
            obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sortingOrder = n;
            n++;            
        }

        

        public void DeActivateFruits()
        {
            fruits.ForEach(i => i.SetActive(false));
        }

        


    }
}

