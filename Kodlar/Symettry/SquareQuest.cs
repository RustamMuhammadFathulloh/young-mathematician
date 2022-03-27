using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Symmetry
{
    public class SquareQuest : MonoBehaviour
    {
        public GameObject squarePrefab;
        public string colorStr;


        private void Awake()
        {

        }
        // Start is called before the first frame update
        void Start()
        {

        }



        public IEnumerator CreateAnimForQuestion(GameObject obj, Color color)
        {
            yield return new WaitForSeconds(0.2f);
            GameObject anim = Instantiate(squarePrefab, obj.transform.position, Quaternion.identity);
            anim.GetComponent<SquareAnimation>().Maximize(color);
            yield return new WaitForSeconds(2);
            GameObject.Destroy(anim);
        }


    }

}

