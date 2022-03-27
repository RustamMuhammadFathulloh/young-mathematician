using ActionManager;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Events;


namespace BingoMul
{
    public class QuestionPanel : MonoBehaviour
    {
        public RectTransform rectTransform;

        
        public float duration;
        public TMP_Text questionText;
        public TMP_Text answerText;
        public Color beginColor;
        public Color endColor;
        public UnityEvent nextQuestionEvent;
        public OperationType operation;
        const string key = "EnumValue";

       

        private void Awake()
        {
            string loadString = PlayerPrefs.GetString(key);
            System.Enum.TryParse(loadString, out OperationType loadState);
            operation = loadState;

            switch (operation)
            {
                case OperationType.Plus:

                    break;
                case OperationType.Minus:

                    break;
                case OperationType.Multiply:
                  
                    break;
                case OperationType.Division:
                    answerText.alignment = TextAlignmentOptions.Center;
                    answerText.alignment = TextAlignmentOptions.Capline;

                    break;
                default:
                    Debug.Log("NOTHING");
                    break;
            }


        }



       


        public void ShowQuestionAnim()
        {            
            questionText.text = GameManager.questionStr;
            answerText.text = "?";
            rectTransform.DOAnchorPosY(-25, duration);
        }

        public void HideQuestionAnim()
        {
            if (GameManager.maxChance < 5)
            {
                rectTransform.DOAnchorPosY(50, duration).OnComplete(NextQuestion);
            }
            
        }

        public void ShowCorrect()
        {
            switch (operation)
            {
                case OperationType.Plus:

                    break;
                case OperationType.Minus:

                    break;
                case OperationType.Multiply:
                    int result = GameManager.a * GameManager.b;
                    questionText.text = GameManager.a.ToString() + " x " + GameManager.b.ToString() + " =";
                    answerText.text = result.ToString();
                    answerText.color = beginColor;
                    answerText.DOColor(endColor, 1.5f).OnComplete(HideQuestionAnim);
                    break;
                case OperationType.Division:
                    int resultD = GameManager.a / GameManager.b;
                    questionText.text = GameManager.a.ToString() + " ÷ " + GameManager.b.ToString() + " =";
                    answerText.text = resultD.ToString();
                    answerText.color = beginColor;
                    answerText.DOColor(endColor, 1.5f).OnComplete(HideQuestionAnim);
                    break;
                default:
                    Debug.Log("NOTHING");
                    break;
            }
            
        }

        void NextQuestion()
        {
            nextQuestionEvent.Invoke();            
        }
    }

}

