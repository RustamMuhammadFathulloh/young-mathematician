using BayatGames.SaveGameFree;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ChessGame
{
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// Kordinata chiqadigan tablo.
        /// </summary>
        public RectTransform savolTablo;
        public Vector3 outPos = new Vector3(0, 80, 0);
        public Vector3 initialPos = new Vector3(0, -25, 0);

        public float initialPosChessBoard;
        public Ease easeForBoard;

        public ChessTablo chessTablo;
        public LevelSO levelSO;
        public SaveLoadSO saveLoad;
        public int level;
        public Image medalImg;




        public GameObject square4x4, square6x6, square8x8;
        public List<GameObject> squares;
        /// <summary>
        /// chessBoard bu faqat shaxmat taxtasidan iborat, 
        /// </summary>        
        public GameObject chessBoard, chessBoardAlphabet;
        public Sprite oqSprite;
        public UnityEvent correctEvent, errorEvent, finishEvent;

        void Awake()
        {            
            savolTablo.GetComponent<RectTransform>().DOAnchorPosY(outPos.y, 0.01f);

            level = levelSO.level;
            chessTablo.chessLevel = level;
        }


        void Start()
        {
            SwitchingBoxCollider(false);
            CheckLevel();
            StartCoroutine(MoveChessBoard());
        }


        /// <summary>
        /// Qaysi daraja ekanligini tekshiradi.
        /// </summary>
        public void CheckLevel()
        {
            if (level == 1)    //4 ga 4
            {
                chessBoardAlphabet = Instantiate(square4x4, new Vector3(0, -0.1f, 0), Quaternion.identity);
            }
            else if (level == 2)        // 5 ga 5  
            {
                chessBoardAlphabet = Instantiate(square6x6, new Vector3(0, -0.1f, 0), Quaternion.identity);
            }
            else if (level == 3)        // 6 ga 6         
            {
                chessBoardAlphabet = Instantiate(square8x8, new Vector3(0, -0.1f, 0), Quaternion.identity);
            }
            chessBoard = chessBoardAlphabet.transform.GetChild(0).gameObject;
            //squares = chessBoard.transform.GetChildren();
            //TakeChilds(chessBoard);
            
            initialPosChessBoard = chessBoardAlphabet.transform.position.y;     
            chessBoardAlphabet.transform.DOMoveY(-11, 0);
            
            TakeChilds();
        }


        /// <summary>
        /// Barcha childlarnni yig'ib beruvchi method.
        /// </summary>
        void TakeChilds()
        {
            for (int i = 0; i < chessBoard.transform.childCount; i++)
            {
                chessBoard.transform.GetChild(i).GetComponent<ChessSquare>().chessTablo = chessTablo;
                chessBoard.transform.GetChild(i).GetComponent<ChessSquare>().squareIndex = i;
                chessBoard.transform.GetChild(i).GetComponent<ChessSquare>().gm = this;
                chessBoard.transform.GetChild(i).GetComponent<ChessSquare>().oqSprite = oqSprite;
                squares.Add(chessBoard.transform.GetChild(i).gameObject);
            }
            StartCoroutine(chessTablo.FingerAnim());
        }


        public void SwitchingBoxCollider(bool TF)
        {
            foreach (GameObject item in squares)
            {
                item.GetComponent<BoxCollider2D>().enabled = TF;
            }
        }



        int nl = 0;
        int bl = 0;


        /// <summary>
        /// SavolTablo ni harakatga keltiruvchi method.
        /// </summary>
        /// <returns></returns>
        public IEnumerator MoveChessBoard()
        {
            if (bl == 0) 
            {
                chessBoardAlphabet.transform
                    .DOMoveY(initialPosChessBoard, 0.8f)
                    .SetEase(easeForBoard);   
                bl += 1;
            }

            if (nl != 0)
            {
                SwitchingBoxCollider(false);
                savolTablo.GetComponent<RectTransform>().DOAnchorPosY(outPos.y, 0.5f);                
                yield return new WaitForSeconds(0.5f);
            }
            yield return new WaitForSeconds(0.1f);
            savolTablo.GetComponent<RectTransform>().DOAnchorPosY(initialPos.y, 0.6f);
            yield return new WaitForSeconds(0.6f);
            SwitchingBoxCollider(true);
            nl += 1;
        }



        public void SetActivation()
        {
            chessBoardAlphabet.SetActive( false);
        }



        public void SaveAndLoadEvent()
        {
            SaveGame.Save<string>(saveLoad.gameName + saveLoad.levels[levelSO.level - 1], medalImg.sprite.name.ToString());
        }

    }
}

