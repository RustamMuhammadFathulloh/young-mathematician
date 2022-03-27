using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShelfPos : MonoBehaviour
{
    public float tabletX;
    public float longPhoneX;
    public float phoneX;


    float x;
    public float y;
    public string deviceType;


    private void Awake()
    {
        deviceType = GeneralPos.DeviceDetector(Screen.width, Screen.height);
        if (deviceType.Equals("tablet"))
        {
            x = tabletX;
        }
        else if (deviceType.Equals("longPhone"))
        {
            x = longPhoneX;
        }
        else
        {
            x = phoneX;
        }
        SetShelfPosition();



    }

    public void SetShelfPosition()
    {
        float width = GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        float height = GetComponent<SpriteRenderer>().sprite.bounds.size.y;
        transform.position = GeneralPos.SetOnRightTop();

        transform.position = new Vector3(transform.position.x - (width * x), transform.position.y - (height * y), 0);
    }
    
}
