using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class ScrollPage : MonoBehaviour
{
    public ScrollRect scrollRect;
    Scrollbar scrollBar;
    public Image page1;
    public Image page2;
    public Image page3;
    public Button rightButton;
    public Button leftButton;
    public bool isFirst;
    public bool isSecond;
    public bool isThird;

    public UnityEvent ButtonClick;
    public UnityEvent ButtonClickError;

    public Sprite spriteON;
    public Sprite spriteOFF;




    private void Awake()
    {
        //scrollRect.enabled = false;
        isFirst = true;
        isSecond = false;
        isThird = false;
        
        scrollBar = GetComponent<Scrollbar>();
        rightButton.onClick.AddListener(TaskOnClickRight);
        leftButton.onClick.AddListener(TaskOnClickLeft);

    }

  


    public void TaskOnClickRight()
    {
        StartCoroutine(MoveToRight());
    }

    public void TaskOnClickLeft()
    {
        StartCoroutine(MoveToLeft());
    }

    IEnumerator MoveToLeft()
    {
        scrollRect.enabled = true;
        if (isFirst)
        {
            ButtonClickError.Invoke();
            scrollRect.enabled = false;
        }
        else if (isSecond)
        {
            page1.sprite = spriteON;
            page2.sprite = spriteOFF;
            page3.sprite = spriteOFF;
            ButtonClick.Invoke();
            StartCoroutine(Move(0.5f, 0));
            yield return new WaitForSeconds(0.3f);
            isFirst = true;
            isThird = false;
            isSecond = false;
            scrollRect.enabled = false;           
        }
        else
        {
            page1.sprite = spriteOFF;
            page2.sprite = spriteON;
            page3.sprite = spriteOFF;
            ButtonClick.Invoke();
            StartCoroutine(Move(1, 0.5f));
            yield return new WaitForSeconds(0.3f);
            isFirst = false;
            isThird = false;
            isSecond = true;
            scrollRect.enabled = false;

        }

    }

    IEnumerator MoveToRight()
    {
        scrollRect.enabled = true;
        if (isFirst)
        {
            page1.sprite = spriteOFF;
            page2.sprite = spriteON;
            page3.sprite = spriteOFF;
            ButtonClick.Invoke();
            StartCoroutine(Move(0, 0.5f));
            yield return new WaitForSeconds(0.3f);
            isFirst = false;
            isThird = false;
            isSecond = true;
            scrollRect.enabled = false;
           

        }
        else if (isSecond)
        {
            page1.sprite = spriteOFF;
            page2.sprite = spriteOFF;
            page3.sprite = spriteON;
            ButtonClick.Invoke();
            StartCoroutine(Move(0.5f, 1));
            yield return new WaitForSeconds(0.3f);
            isFirst = false;
            isThird = true;
            isSecond = false;
            scrollRect.enabled = false;
        }
        else
        {
            ButtonClickError.Invoke();
            scrollRect.enabled = false;
        }
        
    }


    public IEnumerator Move(float fromValue, float toValue)
    {
        float elapsedTime = 0;        
        while (elapsedTime < 0.3f)
        {            
            scrollBar.value = Mathf.Lerp(fromValue, toValue, (elapsedTime / 0.3f));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }        
    }


   

    
}
