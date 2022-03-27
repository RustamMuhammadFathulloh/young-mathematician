using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelText : MonoBehaviour
{
    TMP_Text levelText;
    public LevelSO level;

    public TMP_Text bosqichText;

    //private void Awake()
    //{
    //    //levelText = GetComponent<TMP_Text>();
    //    //levelText.text = level.level.ToString() + "-Bosqich";
    //    Debug.Log("Awake ishladi.");

    //    levelText = GetComponent<TMP_Text>();
        

    //    if (true)
    //    {
    //        Debug.Log(levelText.text);
    //        string satr1 = levelText.text;
    //        string satr2 = satr1.Replace("**n", level.level.ToString());
    //        bosqichText.text = satr2;
    //        Debug.Log(levelText.text);
    //    }
    //}


    private void Start()
    {       
        levelText = GetComponent<TMP_Text>();

        if (true)
        {
            //Debug.Log(levelText.text);
            string satr1 = levelText.text;
            string satr2 = satr1.Replace("**n", level.level.ToString());
            bosqichText.text = satr2;
            Debug.Log(levelText.text);
        }
    }


}
