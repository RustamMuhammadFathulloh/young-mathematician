using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShelfPositioner : MonoBehaviour
{

    public float longPhone;
    public float phone;
    public float tablet;

    public string device;

    private void Awake()
    {
        device = GeneralPos.DeviceDetector(Screen.width, Screen.height);

        if (device.Equals("longPhone"))
        {
            SetShelfPosition(longPhone);
        }
        else if (device.Equals("phone"))
        {
            SetShelfPosition(phone);
        }
        else
        {
            SetShelfPosition(tablet);
        }
    }
    public void SetShelfPosition(float percentage)
    {
        float width = GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
        transform.position = GeneralPos.GetRightPointOnScreen();
        transform.position = new Vector3(transform.position.x - (width * percentage), transform.position.y, 0);
    }



}
