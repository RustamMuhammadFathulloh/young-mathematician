using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Symmetry
{
    public class QuestionSpawner : MonoBehaviour
    {
        public List<GameObject> answerAnimGroup;
        public GameManager gameManager;
        public SimmetriiyaStateSO state;
        public GameEventSO stateChanges;
        public LevelSO level;
        public ColorSO colors;
        public List<int> maxNumberPerLevel;               
        int maxNumberOfQuestion;
        List<Color> colorGroup;
        public Seperator seperator;
        public SpriteForSimmetriyaSO sprites;

        public List<GameObject> correctAnswerGroup;
        List<int> indexGroup;
        List<int> indexGroupClone;
        private void Start()
        {
            answerAnimGroup = new List<GameObject>();
            correctAnswerGroup = new List<GameObject>();
            indexGroup = new List<int>();
            colorGroup = new List<Color>();           
            SetGameInitialSettings(level.level);
            InitialStateChanges();
        }
       
        void SetGameInitialSettings(int levelNumber)
        {            
            colorGroup.Clear();
            switch (levelNumber)
            {
                case 1:
                    maxNumberOfQuestion = maxNumberPerLevel[0];
                    colorGroup.Add(colors.green);
                    break;
                case 2:
                    maxNumberOfQuestion = maxNumberPerLevel[1];
                    colorGroup.Add(colors.green);
                    break;
                case 3:
                    maxNumberOfQuestion = maxNumberPerLevel[2];
                    colorGroup.Add(colors.green);
                    break;
                case 4:
                    maxNumberOfQuestion = maxNumberPerLevel[3];
                    colorGroup.Add(colors.green);
                    colorGroup.Add(colors.pink);
                    break;
                case 5:
                    maxNumberOfQuestion = maxNumberPerLevel[4];
                    colorGroup.Add(colors.green);
                    colorGroup.Add(colors.pink);
                    break;
                case 6:
                    maxNumberOfQuestion = maxNumberPerLevel[5];
                    colorGroup.Add(colors.green);
                    colorGroup.Add(colors.pink);
                    break;
                case 7:
                    maxNumberOfQuestion = maxNumberPerLevel[6];
                    colorGroup.Add(colors.green);
                    colorGroup.Add(colors.pink);
                    colorGroup.Add(colors.blue);
                    break;
                case 8:
                    maxNumberOfQuestion = maxNumberPerLevel[7];
                    colorGroup.Add(colors.green);
                    colorGroup.Add(colors.pink);
                    colorGroup.Add(colors.blue);
                    break;
            }
            
        }

        public void InitialStateChanges()
        {
            ClearAll();
            StartCoroutine(MoveTo());
           
        }

        IEnumerator MoveTo()
        {
            yield return new WaitForSeconds(0.5f);
            state.runTimeStateNumber++;
            stateChanges.Raise();            
        }
        


        public void CreateRandomQuestion()
        {
            ColorSort();           
        }

        void ColorSort()
        {                     
            seperator.InitialCondition(sprites.edge, colors.grey);
            seperator.ChangeSpritesForQuestionGroup();
            answerAnimGroup.Clear();
            correctAnswerGroup.Clear();
            indexGroup.Clear();
            indexGroup = new List<int>(PosFinder.GetIndexGroup(maxNumberOfQuestion));
            indexGroupClone = new List<int>(indexGroup).ToList();
            switch (level.level)
            {
                case 1:
                    CoverQuestionSquare(maxNumberOfQuestion, colors.green);
                    break;
                case 2:
                    CoverQuestionSquare(maxNumberOfQuestion, colors.green);
                    break;
                case 3:
                    CoverQuestionSquare(maxNumberOfQuestion, colors.green);
                    break;
                case 4:
                    CoverQuestionSquare(maxNumberOfQuestion - 2, colors.green);
                    CoverQuestionSquare(2, colors.pink);
                    break;
                case 5:
                    CoverQuestionSquare(maxNumberOfQuestion - 2, colors.green);
                    CoverQuestionSquare(2, colors.pink);
                    break;
                case 6:
                    CoverQuestionSquare(maxNumberOfQuestion - 2, colors.green);
                    CoverQuestionSquare(2, colors.pink);
                    break;
                case 7:
                    CoverQuestionSquare(maxNumberOfQuestion - 3, colors.green);
                    CoverQuestionSquare(2, colors.pink);
                    CoverQuestionSquare(1, colors.blue);
                    break;
                case 8:                    
                    CoverQuestionSquare(maxNumberOfQuestion - 4, colors.green);
                    CoverQuestionSquare(2, colors.pink);
                    CoverQuestionSquare(2, colors.blue);
                    break;
            }

        }

        void CoverQuestionSquare(int max, Color color)
        {            
            for (int k = 0; k < max; k++)
            {                
                GameObject randomObj = seperator.questionGroup[indexGroupClone[0]];
                randomObj.GetComponent<SpriteRenderer>().sprite = sprites.cover;
                randomObj.GetComponent<SpriteRenderer>().color = color;
                randomObj.GetComponent<SquareQuest>().colorStr = color.ToString();
                indexGroupClone.RemoveAt(0);
                StartCoroutine(randomObj.GetComponent<SquareQuest>().CreateAnimForQuestion(randomObj, colors.black));
                GameObject correctObj = PosFinder.GetCorrectSquare(randomObj.transform.position, seperator.side, seperator.answerGroup);
                correctObj.GetComponent<Square>().isCorrect = true;
                correctObj.GetComponent<Square>().colorStr = color.ToString();
                correctAnswerGroup.Add(correctObj);
            }
            ShowCorrectAnserGroup();
        }

        void ShowCorrectAnserGroup()
        {           
            if (level.level.Equals(1))
            {
                foreach (GameObject obj in correctAnswerGroup)
                {
                    StartCoroutine(obj.GetComponent<Square>().ShowAnswerAnim(obj, colors.black));
                    //obj.GetComponent<Square>().isCorrect = true;
                }
            }
            else
            {
                foreach (GameObject obj in correctAnswerGroup)
                {
                    //obj.GetComponent<Square>().isCorrect = true;
                }
            }                  
        }


        public void ClearAll()
        {            
            foreach (GameObject obj in answerAnimGroup)
            {
                GameObject.Destroy(obj);
            }

            foreach (GameObject obj in seperator.answerGroup)
            {
                Destroy(obj.GetComponent<Square>());
                Destroy(obj.GetComponent<BoxCollider2D>());

            }
            foreach (GameObject obj in seperator.questionGroup)
            {
                Destroy(obj.GetComponent<SquareQuest>());
            }

            foreach (GameObject obj in correctAnswerGroup)
            {
                Destroy(obj.GetComponent<Square>().clickAnim);
            }

           



        }
    }
}

