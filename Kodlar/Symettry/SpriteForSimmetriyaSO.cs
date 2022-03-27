using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Symmetry
{
    [CreateAssetMenu(fileName = "SimmetriyaSprite", menuName = "ScriptableObjects/SimmetriyaSprites", order = 7)]
    public class SpriteForSimmetriyaSO : ScriptableObject
    {
        public Sprite wrong;
        public Sprite cover;
        public Sprite edge;
        public List<Sprite> backgroundGroup;
        public Sprite bigRectangle;
        public Sprite dot;

    }


}

