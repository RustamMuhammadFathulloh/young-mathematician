using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ChessGame
{
    public class ChessTablo : MonoBehaviour
    {
        private int totalTaskCount = 0;
        public int boardSize;
        public int chessLevel;
        public string harflar;
        public TMP_Text boardText;
        public TMP_Text taskText;
        public GameManager gm;

        public GameObject fingerCursor;


        void Start()
        {
            CheckBorderTask();
        }


        public void CheckBorderTask()
        {
            switch (chessLevel)
            {
                case 1:                    
                    boardSize = 4;
                    harflar = "ABCD";
                    break;
                case 2:                    
                    boardSize = 5;
                    harflar = "ABCDE";
                    break;
                case 3:                    
                    boardSize = 6;
                    harflar = "ABCDEF";
                    break;
                default:
                    break;
            }

            TaskMaker();
        }


        public int sonSavol;
        public int harfIndex;
        public char harfSavol;


        public void TaskMaker()
        {
            totalTaskCount += 1;
            harfIndex = Random.Range(0, boardSize);
            harfSavol = harflar[harfIndex];
            sonSavol = Random.Range(1, boardSize + 1);

            //Debug.Log("harfSavol = " + harfSavol + " sonSavol = " + sonSavol);
            boardText.text = harfSavol.ToString() + sonSavol.ToString();
            taskText.text = totalTaskCount.ToString() + "/" + 8.ToString();
            
        }


        public void CheckFinishing()
        {            
            StartCoroutine(CallFinishEvent());            
        }


        /// <summary>
        /// FinishEvent ni yoki keyingi savolni chaqiradi.
        /// </summary>
        /// <returns></returns>
        public IEnumerator CallFinishEvent()
        {
            yield return new WaitForSeconds(0.8f);
            
            if (totalTaskCount != 8)            {
                StartCoroutine(gm.MoveChessBoard());
                yield return new WaitForSeconds(0.2f);
                TaskMaker();
            }
            else if (totalTaskCount == 8 )           {
                yield return new WaitForSeconds(0.5f);
                gm.finishEvent.Invoke();
            }
        }

               

        /// <summary>
        /// ChessGame uchun o'yinni tushunturuvchi Cursor Anim Methodi.
        /// </summary>
        /// <returns></returns>
        public IEnumerator FingerAnim()
        {
            float vaqt = 2.5f;
            yield return new WaitForSeconds(0.8f);

            if (true)
            {                
                gm.SwitchingBoxCollider(false);     
            }

            yield return new WaitForSeconds(0.6f);
            Vector3 initialPosFinger = fingerCursor.GetComponent<Transform>().position;
            Vector3 pos = gm.squares[(harfIndex  * boardSize + sonSavol - 1)].GetComponent<Transform>().position;
            
            fingerCursor.transform.DOMove(pos, 1.6f);
            yield return new WaitForSeconds(1.6f);
            fingerCursor.transform.DOScale(0.8f, 0.4f);
            yield return new WaitForSeconds(0.4f);
            fingerCursor.transform.DOScale(1f, 0.5f);
            yield return new WaitForSeconds(0.5f);
            fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.8f);
            yield return new WaitForSeconds(0.8f);

            gm.SwitchingBoxCollider(true);     

            yield return new WaitForSeconds(0.8f);
            fingerCursor.transform.DOMove(initialPosFinger, 0);
            fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.8f);

        }



    }
}

