using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SaveLoad_GameName", menuName = "ScriptableObjects/SaveLoad", order = 6)]
public class SaveLoadSO : ScriptableObject
{
    public string gameName;
    public string[] levels;


}
