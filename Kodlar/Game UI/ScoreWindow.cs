using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ActionManager;

public class ScoreWindow : MonoBehaviour
{
    
    
    public GameObject scoreWindow;
    public TMP_Text scoreText;
    public Image medal;
    public Image sadEmoji;

    Vector3 initialScale;

    private void Awake()
    {
        
       
        initialScale = new Vector3(1, 1, 0);
    }


    private void Start()
    {
        scoreWindow.SetActive(false);

    }

    public void DisplayScoreWindow()
    {
        scoreWindow.SetActive(true);       
        StartCoroutine(AnimateObject(medal.gameObject));
    }

    IEnumerator AnimateObject(GameObject obj)
    {

        StartCoroutine(Actions.ScaleOverSeconds(obj, initialScale * 1.25f, 0.5f));
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(Actions.ScaleOverSeconds(obj, initialScale, 0.5f));
        yield return new WaitForSeconds(1.5f);
        
    }


    public void GameOver()
    {
        scoreWindow.SetActive(true);
        sadEmoji.gameObject.SetActive(true);
        StartCoroutine(AnimateObject(sadEmoji.gameObject));
    }


}
