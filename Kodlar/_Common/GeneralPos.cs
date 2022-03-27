using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GeneralPos : MonoBehaviour
{



    public static void ShufflePosition(List<GameObject> list)
    {
        List<Vector3> newPositionList = new List<Vector3>();
        foreach (GameObject obj in list)
        {
            newPositionList.Add(obj.transform.position);
        }
        newPositionList = newPositionList.OrderBy(x => Random.value).ToList();
        for (int i = 0; i < newPositionList.Count; i++)
        {
            list[i].transform.position = newPositionList[i];
        }
    }

    public static void DisableOrEnableBoxCollider2D(List<GameObject> list, bool val)
    {
        foreach (GameObject obj in list)
        {
            obj.GetComponent<BoxCollider2D>().enabled = val;
        }
    }

    public static float GetRightEdge(SpriteRenderer spriteRenderer, Vector3 gameObjectTransformPos)
    {
        float width = spriteRenderer.sprite.bounds.size.x;
        float edge = gameObjectTransformPos.x + width / 2;
        return edge;
    }

    public static float GetLeftEdge(SpriteRenderer spriteRenderer, Vector3 gameObjectTransformPos)
    {
        float width = spriteRenderer.sprite.bounds.size.x;
        float edge = gameObjectTransformPos.x - width / 2;
        return edge;
    }

    public static string DeviceDetector(float width, float height)
    {
        if (width / height < 1.5f)
        {
            return "tablet";
        }
        else if (width / height > 1.9f)
        {
            return "longPhone";
        }
        else
        {
            return "phone";
        }
    }

    public static Vector3 GetRightPointOnScreen()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0));
        point = new Vector3(point.x, point.y, 0);
        return point;
    }

    public static Vector3 GetTopPointOnScreen()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height, 0));
        point = new Vector3(point.x, point.y, 0);
        return point;
    }

    public static Vector3 GetBottomPointOnScreen()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0));
        point = new Vector3(point.x, -point.y, 0);
        return point;
    }


    public static Vector3 SetOnRightTop()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        point = new Vector3(point.x, point.y, 0);
        return point;

    }

    public static Vector3 SetOnRightMiddle()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        point = new Vector3(point.x, point.y, 0);
        return point;

    }

    public static Vector3 SetOnRightDown()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        point = new Vector3(point.x, -point.y, 0);
        return point;

    }

    public static Vector3 SetOnLeftDown()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        point = new Vector3(-point.x, -point.y, 0);
        return point;

    }

    public static Vector3 GetLeftPointOnScreen()
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2, 0));
        point = new Vector3(-point.x, point.y, 0);
        return point;
    }



    public static Vector3 OverObject(GameObject objBelow, Sprite spAbove, float percent, float objBelowPart)
    {
        float heightBelow = objBelow.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
        float widthBelow = objBelow.GetComponent<SpriteRenderer>().sprite.bounds.size.x;

        float heightAbove = spAbove.bounds.size.y;

        float yPos = objBelow.transform.position.y + heightBelow + heightAbove * percent;
        float xPos = objBelow.transform.position.x - widthBelow * objBelowPart;
        return new Vector3(xPos, yPos, 0);
    }

    public static Vector3 PutUnderObject(GameObject obj, float howFar)
    {
        float y = obj.GetComponent<SpriteRenderer>().sprite.bounds.size.y * howFar;
        Vector3 pos = new Vector3(obj.transform.position.x, obj.transform.position.y - y, 0);
        return pos;
    }


    public static string GetFinalTime(int val)
    {
        if (val / 10 > 0)
        {
            return val.ToString();
        }
        else
        {
            return "0" + val.ToString();
        }
    }

}



