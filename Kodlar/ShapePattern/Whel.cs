using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whel : MonoBehaviour
{
    public float speed;
  

    IEnumerator coroutine;
  

    private void Awake()
    {
      

        //coroutine = RotateMe(-Vector3.forward * 90, speed);
        //Move();
    }


    public void Move()
    {
        coroutine = RotateMe(-Vector3.forward * 90, speed);
        StartCoroutine(coroutine);
    }


    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        coroutine = RotateMe(-Vector3.forward * 90, speed);
        StartCoroutine(coroutine);
    }

    public void StopBalon()
    {
        StopCoroutine(coroutine);
    }
}
