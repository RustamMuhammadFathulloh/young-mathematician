using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SectionColor
{
    public class TaskBoardMove : MonoBehaviour
    {
        public RectTransform taskBoard;
        public GameManager gm;

        public Vector3 initialPosition;
        public Vector3 outPosition = new Vector3(0, 120, 0);

        int harakat = 0;

        private void Awake()
        {
            initialPosition = taskBoard.transform.position;
            
            taskBoard.GetComponent<RectTransform>().DOAnchorPos(outPosition, 0);
        }


        
        void Start()
        {
            
            StartCoroutine(MoveQuestionBoard());
        }



        public void TaskBoardAnim()
        {
            StartCoroutine(MoveQuestionBoard());
        }



        public IEnumerator MoveQuestionBoard()
        {
            Debug.Log("Ishladi. harakat = " + harakat);
            if (harakat > 0) 
            {
                
                taskBoard.GetComponent<RectTransform>().DOAnchorPosY(outPosition.y, 0.8f);
                yield return new WaitForSeconds(0.6f);
            }
            yield return new WaitForSeconds(0.4f);
            if (gm.currentStateNumber < gm.maxStateNumber) 
            {
                Debug.Log(gm.currentStateNumber + " " + gm.maxStateNumber);
                taskBoard.GetComponent<RectTransform>().DOAnchorPosY(-40, 0.8f);
            }
            
            yield return new WaitForSeconds(0.2f);


            harakat += 1;
        }

                

        
    }
}
