using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class ButtonListener : MonoBehaviour
{
    public Button[] buttons;
    public GameEventSO gmEvent;

    


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            Button button = buttons[i];
            
            button.onClick.AddListener(() => OnClick());
        }
    }

    public void Test()
    {
        

    }




    private void OnClick()
    {
        gmEvent.Raise();        
    }

   


    
}
