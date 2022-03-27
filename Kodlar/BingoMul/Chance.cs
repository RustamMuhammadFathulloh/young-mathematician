using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionManager;
using UnityEngine.UI;

namespace BingoMul
{
    public class Chance : MonoBehaviour
    {
        public Sprite redSprite;

        List<GameObject> children;
        int n = 0;
        public float maxSize;
        public float duration;

        private void Awake()
        {
            children = Actions.ChildrenOfGameobject(gameObject);
        }

        public void IncrementChance()
        {
            GameObject obj = children[n];
            StartCoroutine(WrongAnim(obj));
            obj.GetComponent<Image>().sprite = redSprite;
            n++;            
        }

        IEnumerator WrongAnim(GameObject obj)
        {
            StartCoroutine(Actions.ScaleOverSeconds(obj, new Vector3(maxSize, maxSize, 0), duration));
            yield return new WaitForSeconds(duration);
            StartCoroutine(Actions.ScaleOverSeconds(obj, new Vector3(1, 1, 0), duration));
        }

    }
}

