using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionManager;
using UnityEngine.Events;

namespace ShapePattern
{
    public class Pattern : MonoBehaviour
    {
        public Car car;
       
        public GameObject parent;
        public List<Color> colors;
        public GameManager gm;
        public Sprite trafficCone;
        public List<Sprite> shapes;
        public GameObject boxParent;
        public GameObject boardParent;
        public GameObject trafficConeParent;        
        public string currentPattern;
        public List<string> patternLevelOne;
        public List<string> patternLevelTwo;
        public List<string> patternLevelThree;
        public UnityEvent correctEvent;
        public UnityEvent wrongEvent;
        public UnityEvent goHomeEvent;
        public int random;
        public int correct;


        private void Awake()
        {
            MakePattern();

        }

        void ClearAll()
        {
            foreach (GameObject obj in Actions.ChildrenOfGameobject(parent))
            {
                Destroy(obj);
            }
            
        }

        public void MakePattern()
        {
            random = 0;
            correct = 0;
            ClearAll();
            random = Random.Range(2, 4);
            if (gm.level.level.Equals(1))
            {
                CreatePattern(patternLevelOne, 2);
            }
            else if (gm.level.level.Equals(2))
            {
                CreatePattern(patternLevelTwo, 3);
            }
            else
            {
                CreatePattern(patternLevelThree, 4);
            }
            gm.UpdateStateNum();
            GetOffShapes(random);
        }



        void CreatePattern(List<string> patternCollection, int maxNum)
        {
            currentPattern = patternCollection[Random.Range(0, patternCollection.Count)];
            List<Sprite> shapeSprites = new List<Sprite>(shapes);
            List<Color> shapeColors = new List<Color>(colors);

            List<Color> newColorGroup = new List<Color>();
            for (int i = 0; i < maxNum; i++)
            {
                int colorIndexNum = Random.Range(0, shapeColors.Count);
                newColorGroup.Add(shapeColors[colorIndexNum]);
                shapeColors.RemoveAt(colorIndexNum);
            }

            List<Sprite> newSpriteGroup = new List<Sprite>();
            for (int i = 0; i < maxNum; i++)
            {
                int indexSprite = Random.Range(0, shapeSprites.Count);
                newSpriteGroup.Add(shapeSprites[indexSprite]);
                shapeSprites.RemoveAt(indexSprite);
            }

            List<GameObject> boardShapes = Actions.ChildrenOfGameobject(boardParent);
            if (maxNum.Equals(4))
            {
                for (int i = 0; i < maxNum; i++)
                {
                    boardShapes[i].GetComponent<SpriteRenderer>().sprite = newSpriteGroup[i];
                    boardShapes[i].GetComponent<SpriteRenderer>().color = newColorGroup[i];
                }
            }
            else
            {
                for (int i = 1; i <= maxNum; i++)
                {
                    boardShapes[i].GetComponent<SpriteRenderer>().sprite = newSpriteGroup[i - 1];
                    boardShapes[i].GetComponent<SpriteRenderer>().color = newColorGroup[i - 1];
                }
            }
           

            int n = 0;
            foreach (GameObject obj in Actions.ChildrenOfGameobject(boxParent))
            {
                if (maxNum < 4)
                {
                    if (n.Equals(3))
                    {
                        n = 0;
                    }
                }
                else
                {
                    if (n.Equals(4))
                    {
                        n = 0;
                    }

                }
               
                char foo = currentPattern[n];
                int ind = int.Parse(foo.ToString());

                if (ind.Equals(0))
                {
                    obj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = newColorGroup[0];
                }
                else if (ind.Equals(1))
                {
                    obj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = newColorGroup[1];
                }
                else if(ind.Equals(2))
                {
                    obj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = newColorGroup[2];
                }
                else if (ind.Equals(3))
                {
                    obj.transform.GetChild(0).GetComponent<SpriteRenderer>().color = newColorGroup[3];
                }

                obj.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = newSpriteGroup[ind];
                obj.GetComponent<Box>().shapeName = newSpriteGroup[ind].name;
                n++;
            }
        }








       
        void GetOffShapes(int random)
        {
            List<GameObject> boxShapeList = Actions.ChildrenOfGameobject(boxParent);
            boxShapeList.RemoveAt(0);
            boxShapeList.RemoveAt(0);
            boxShapeList = Actions.ShuffleList(boxShapeList);
            for (int i = 0; i < boxShapeList.Count; i++)
            {
                boxShapeList[i].GetComponent<Box>().trafficCone.GetComponent<SpriteRenderer>().sprite = null;
            }

            for (int i = 0; i < random; i++)
            {
                boxShapeList[i].transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = null;
                boxShapeList[i].GetComponent<Box>().trafficCone.GetComponent<SpriteRenderer>().sprite = trafficCone;
            }
        }

      
    }
}

