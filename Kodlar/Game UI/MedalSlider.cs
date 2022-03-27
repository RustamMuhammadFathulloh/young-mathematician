using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BayatGames.SaveGameFree;
using UnityEngine.Events;

public class MedalSlider : MonoBehaviour
{
    public TMP_Text scoreText;
    public Image medalImg;
    public float[] sliderPeriodLimit;
    public float[] medalPeriodLimit;
    public Slider slider;
    public float elapsedTime = 0;   
    public int index;
    public float timer;
    public bool isEnd;
    public Medal[] medals;   
    public UnityEvent saveLoadEvent;


    private void Awake()
    {
        timer = 0;
        index = 0;
        slider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Move(sliderPeriodLimit[index], sliderPeriodLimit[index+1], medalPeriodLimit[index]));
        
    }

   

    public IEnumerator Move(float fromValue, float toValue, float medalPeriod)
    {
       
        if (index < 3)
        {
            medalImg.sprite = medals[index].GetComponent<Medal>().medalAward;
            medals[index].Maximize();            
        }
        elapsedTime = 0;
        while (elapsedTime < medalPeriod)
        {           
            slider.value = Mathf.Lerp(fromValue, toValue, (elapsedTime / medalPeriod));
            elapsedTime += Time.deltaTime;
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        index++;
        yield return new WaitForSeconds(0.1f);
        if (index < 4)
        {
            StartCoroutine(Move(sliderPeriodLimit[index], sliderPeriodLimit[index + 1], medalPeriodLimit[index]));
        }
        else
        {
            StartCoroutine(TimerCount());
        }
    }


    public IEnumerator TimerCount()
    {
        while (!isEnd)
        {           
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

    }

    
    public void StopTimer()
    {
        
        isEnd = true;
        StopAllCoroutines();
        scoreText.text = GeneralPos.GetFinalTime((int)timer / 60) + ":" + GeneralPos.GetFinalTime((int)timer % 60);
        saveLoadEvent.Invoke();
        
    }

   


}
