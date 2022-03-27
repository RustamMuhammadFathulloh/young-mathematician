using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Multiplication
{
    public class MainCodesForUsing : MonoBehaviour
    {
        /// <summary>
        /// BoxCollider2d ni o'chiruvchi method.
        /// </summary>
        /// <param name="objectList">BoxCollider2D si o'chiriladigan obyektlar listi.</param>
        public static void OffBoxCollider2d(List<GameObject> objectList)
        {
            for (int i = 0; i < objectList.Count; i++)
            {
                objectList[i].GetComponent<BoxCollider2D>().enabled = false;
            }
        }


        /// <summary>
        /// BoxCollider2d ni yoquvchi method.
        /// </summary>
        /// <param name="objectList">Obyektlari uchun BoxCollider2d yoqiladigan list nomi.</param>
        public static void OnBoxCollider2d(List<GameObject> objectList)
        {
            for (int i = 0; i < objectList.Count; i++)
            {
                //objectList[i].AddComponent<BoxCollider2D>();
                objectList[i].GetComponent<BoxCollider2D>().enabled = true;

            }
        }



    }

}