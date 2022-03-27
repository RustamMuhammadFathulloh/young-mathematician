using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace SectionColor
{
    public class CalculationManager : MonoBehaviour
    {
        public int level;
        public int surat, maxraj;

        public GameManager gameManger;
        public TMP_Text questionText;

        public List<TMP_Text> savollarText;//lokalizatsiya uchun
        public List<string> SavollarString;//lokalizatsiya uchun

        public List<string> questionWords;
        public UnityEvent clickEvent, correctEvent, errorEvent, questionEvent;
        public UnityEvent finishEvent;

        public List<int> parentCheck;
        public int BBB;

        string maxrajString, suratString;

        public GameObject fingerCursor;

        List<string> asosiyRaqamTili;
        public List<string> UzbRaqam, KaaRaqam, KazRaqam, KirRaqam, TajRaqam, TurmRaqam, EngRaqam, RusRaqam;
        int finn = 0;

        
        /// <summary>
        /// Lokalizatsiya uchun mo'ljallangan savol tuzadigan method.
        /// </summary>
        public void I2MakeQuestion()
        {
            surat = 0;
            maxraj = 0;
            maxrajString = null;
            suratString = null;

            int countmainObject = gameManger.mainPrefab.Count;
            string randomQuestion = savollarText[Random.Range(0, questionWords.Count)].text;
            surat = Random.Range(1, level + 1);
            maxraj = level + 1;
            char belgi = randomQuestion[0];
            randomQuestion = randomQuestion.Substring(1);

            switch (belgi)
            {
                case '1':
                    asosiyRaqamTili = UzbRaqam;
                    break;
                case '2':
                    asosiyRaqamTili = KaaRaqam;
                    break;
                case '3':
                    asosiyRaqamTili = KazRaqam;
                    break;
                case '4':
                    asosiyRaqamTili = KirRaqam;
                    break;
                case '5':
                    asosiyRaqamTili = TajRaqam;
                    break;
                case '6':
                    asosiyRaqamTili = TurmRaqam;
                    break;
                case '7':
                    asosiyRaqamTili = EngRaqam;
                    break;
                case '8':
                    asosiyRaqamTili = RusRaqam;
                    break;
                default:
                    break;
            }

            if ((maxraj == 2) )
            {
                suratString = asosiyRaqamTili[0];
            }
            else if ((maxraj == 3) && (surat == 1))
            {
                suratString = asosiyRaqamTili[1];
            }
            else if ((maxraj ==3) && (surat==2))
            {
                suratString = asosiyRaqamTili[2];
            }
            else if ((maxraj == 4) && (surat == 1))
            {
                suratString = asosiyRaqamTili[3];
            }
            else if ((maxraj == 4) && (surat == 2))
            {
                suratString = asosiyRaqamTili[4];
            }
            else if ((maxraj == 4) && (surat == 3))
            {
                suratString = asosiyRaqamTili[5];
            }

            questionEvent.Invoke();
            Debug.Log(suratString);
            string kasr = randomQuestion.Replace("+taqsim", suratString);
                        
            questionText.text = kasr;
            if (finn == 0)
            {
                StartCoroutine(FingerCursorAnim(surat));
                finn += 1;
            }
        }



        /// <summary>
        /// Savollarni tuzuvchi method.
        /// </summary>
        public void MakeQuestion()
        {
            I2MakeQuestion();

            
        }


        /// <summary>
        /// Javobni tekshiruvchi method.
        /// </summary>
        public void CheckAnswer()
        {
            BBB = 0;
            for (int i = 0; i < gameManger.sonlar.Count; i++)
            {                
                if (gameManger.sonlar[i] == surat)
                {
                    BBB+=1;
                }
                else if (gameManger.sonlar[i] != surat) 
                {
                    gameManger.parentSquares[i].GetComponent<SpriteRenderer>().sprite = gameManger.parentChangeSprite;
                }
            }
            if (BBB==3)
            {
                StartCoroutine(gameManger.AnimationMinimizeMaximize());
                correctEvent.Invoke();
            }
            else
            {                
                errorEvent.Invoke();
                StartCoroutine(ReturnParentSprite());                
            }
            BBB = 0;
        }


        /// <summary>
        /// Barcha parnetSquare larga dastlabki spriteni qaytarish.
        /// </summary>
        public IEnumerator ReturnParentSprite()
        {
            StartCoroutine(gameManger.SwitchCollider2D());
            yield return new WaitForSeconds(0.5f);  //0.7f
            for (int i = 0; i < gameManger.parentSquares.Count; i++)
            {
                gameManger.parentSquares[i].GetComponent<SpriteRenderer>().sprite = gameManger.parentInitialSprite;
            }
            gameManger.sonlar.Clear();
        }



        /// <summary>
        /// Barmoq animatsiyasini takrorlab beruvchi method.
        /// </summary>
        /// <returns></returns>
        public IEnumerator FingerCursorAnim(int surat)
        {
            yield return new WaitForSeconds(0.5f);
            Vector3 initialPosFingerCursor = fingerCursor.transform.position;
            float vaqt = 0.8f;
            
            for (int n = 2; n > -1; n--) 
            {
                for (int i = 0; i < surat; i++)
                {
                    Vector3 pozitsiya = gameManger.parentSquares[n].transform.GetChild(0).transform.GetChild(i).position;
                    fingerCursor.transform.DOMove(pozitsiya, vaqt);
                    yield return new WaitForSeconds(0.8f);
                    fingerCursor.transform.DOScale(0.8f, 0.4f);
                    yield return new WaitForSeconds(0.4f);
                    fingerCursor.transform.DOScale(1f, 0.4f);
                    yield return new WaitForSeconds(0.4f);
                }
            }

            fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.7f);
            yield return new WaitForSeconds(0.8f);
            fingerCursor.transform.DOMove(initialPosFingerCursor, 0);
            fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.8f);
            yield return new WaitForSeconds(0.8f);

        }


    }
}
