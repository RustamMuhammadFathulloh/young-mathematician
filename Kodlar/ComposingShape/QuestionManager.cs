using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionManager;
using System.Linq;
using DG.Tweening;

namespace ComposingShape
{
    public class QuestionManager : SerializedMonoBehaviour
    {

        public GameManager gm;

        public List<ComposingShapeSO> shapeCollectionSO;

        public ComposingShapeSO shapeCollection;
        public GameObject puzzle;

        List<GameObject> prefabShapes = new List<GameObject>();
        public List<GameObject> puzzleShapes = new List<GameObject>();

        public Color [] colors;


        public GameObject fingerCursor;
        public GameObject shelfPos2;

        private void Start()
        {

            GiveProperShapeCollection();
            StartCoroutine(FingerCursorAnim());
        }


        public void GiveProperShapeCollection()
        {           
            shapeCollection = shapeCollectionSO[gm.level.level - 1];
            CreatePuzzle();
        }

        void CreatePuzzle()
        {
            puzzle = Instantiate(shapeCollection.puzzle);
            puzzle.GetComponent<Puzzle>().gm = gm;
            GetShapes();
        }


        void GetShapes()
        {                        
            prefabShapes = shapeCollection.questionShapeDict[gm.currentStateNumber].ToList();           
            CreateShapes();



        }

        void CreateShapes()
        {
            int n = 0;
            foreach (GameObject obj in prefabShapes)
            {
                GameObject prefab = Instantiate(obj);
                prefab.GetComponent<Shape>().shapeColor = colors[n];
                prefab.GetComponent<Shape>().gm = gm;
                prefab.GetComponent<Shape>().puzzle = puzzle.GetComponent<Puzzle>();               
                puzzleShapes.Add(prefab);
                n++;
            }
            RePositionShapes();
            gm.startEvent.Invoke();

        }

      

        void RePositionShapes()
        {
            List<GameObject> posList = Actions.ChildrenOfGameobject(gm.shelf);
            int n = 0;
            foreach (GameObject obj in puzzleShapes)
            {
                float yPos = posList[n].transform.position.y;                
                obj.transform.position = new Vector3(obj.transform.position.x, yPos, 0);
                n++;
            }
        }

        public void CountCorrect()
        {
           
            if (puzzle.GetComponent<Puzzle>().puzzleShapes.Count.Equals(0))
            {
                StartCoroutine(MoveToNext());
                foreach (GameObject obj in puzzleShapes)
                {
                    obj.GetComponent<PolygonCollider2D>().enabled = false;
                }

            }
        }


        IEnumerator MoveToNext()
        {
            yield return new WaitForSeconds(1.3f);
            if (gm.currentStateNumber.Equals(gm.maxStateNumber))
            {
                gm.FinishGame();
            }
            else
            {               
                Destroy(puzzle);
                foreach (GameObject obj in puzzleShapes)
                {
                    Destroy(obj);
                }
                puzzleShapes.Clear();
                prefabShapes.Clear();

                gm.UpdateStateNum();
                GiveProperShapeCollection();
               

            }
           

        }


        /// <summary>
        /// Barmoq animatsiyasini qilib beruvchi method.
        /// </summary>
        /// <returns></returns>
        public IEnumerator FingerCursorAnim()
        {
            int daraja = gm.level.level;
            //Debug.Log("daraja = " + daraja);

            Vector3 initialPosFinger = fingerCursor.transform.position;
            Vector3 pos = shelfPos2.transform.position;
            float vaqt = 0.7f;

            yield return new WaitForSeconds(vaqt + 0.4f);
            fingerCursor.transform.DOMove(pos, vaqt-0.2f);
            yield return new WaitForSeconds(vaqt-0.2f);
            fingerCursor.transform.DOScale(0.8f, vaqt-0.2f);
            yield return new WaitForSeconds(vaqt-0.2f);            

            if (daraja == 1)
            {
                fingerCursor.transform.DOMove(puzzle.transform.GetChild(1).transform.position, vaqt + 0.4f);
            }
            else if (daraja == 2)
            {
                fingerCursor.transform.DOMove(puzzle.transform.GetChild(0).transform.position, vaqt + 0.4f);
            }
            else if (daraja == 3)
            {
                fingerCursor.transform.DOMove(puzzle.transform.GetChild(2).transform.position, vaqt + 0.4f);
            }
            else if (daraja == 4)
            {
                fingerCursor.transform.DOMove(puzzle.transform.GetChild(7).transform.position, vaqt + 0.4f);
            }
            else if (daraja == 5)
            {
                fingerCursor.transform.DOMove(puzzle.transform.GetChild(13).transform.position, vaqt + 0.4f);
            }
            else if (daraja == 6)
            {
                Vector3 pozitsiya = puzzle.transform.GetChild(7).transform.position;
                pozitsiya.x = -2;
                pozitsiya.y = -1.8f;
                fingerCursor.transform.DOMove(pozitsiya, vaqt + 0.4f);
            }
            else if (daraja == 7)
            {
                fingerCursor.transform.DOMove(puzzle.transform.GetChild(12).transform.position, vaqt + 0.4f);
            }
            else if (daraja == 8)
            {
                Vector3 pozitsiya = puzzle.transform.GetChild(15).transform.position;
                pozitsiya.y = -1;
                fingerCursor.transform.DOMove(pozitsiya, vaqt + 0.4f);
            }
            yield return new WaitForSeconds(vaqt + 0.5f);
            fingerCursor.transform.DOScale(1f, 0.5f);
            yield return new WaitForSeconds(0.5f);

            fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.5f);
            yield return new WaitForSeconds(1f);
            fingerCursor.transform.DOMove(initialPosFinger, 0);
            fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.8f);

        }



    }
}

