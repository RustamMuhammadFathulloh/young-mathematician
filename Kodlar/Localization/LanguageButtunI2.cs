using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageButtunI2 : MonoBehaviour
{
    
    public Button mainTilButton;
    public GameObject tillarTemplete;    
    public List<Button> tillar;



    /// <summary> 
    /// Languages button ni ichidagi Temolete ning setActive ni true yoki false qiluvchi method. 
    /// </summary> 
    public void ButtonActivetor()
    {
        //bool trueOrFalse = tillarTemplete; 
        //Debug.Log(trueOrFalse); 
        if (tillarTemplete.active)
        {
            tillarTemplete.SetActive(false);
        }
        else if (!tillarTemplete.active)
        {
            tillarTemplete.SetActive(true);
        }
    }


    /// <summary> 
    /// Tillar 
    /// </summary> 
    public void SwitchOffTillarTemplete()
    {
        if (tillarTemplete.active)
        {
            tillarTemplete.SetActive(false);
        }
    }



}
