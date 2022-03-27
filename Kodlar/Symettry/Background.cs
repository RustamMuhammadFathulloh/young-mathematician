using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Symmetry
{
    public class Background : MonoBehaviour
    {

        SpriteRenderer background;
        [HideInInspector]
        public int backgroundCode;
        public SpriteForSimmetriyaSO sprites;


        private void Awake()
        {
            background = GetComponent<SpriteRenderer>();
        }


        public void ChangeBackground()
        {
            backgroundCode = Random.Range(1, 4);
            background.sprite = sprites.backgroundGroup[backgroundCode-1];           
        }

    }

}

