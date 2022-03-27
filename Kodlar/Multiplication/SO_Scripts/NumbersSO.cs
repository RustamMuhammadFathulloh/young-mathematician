using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/NumberSprites", order = 8)]
public class NumbersSO : ScriptableObject
{
    /// <summary>
    /// using numbers for future projects. Sonlar uchun list.
    /// </summary>
    public List<Sprite> numbers;
}
