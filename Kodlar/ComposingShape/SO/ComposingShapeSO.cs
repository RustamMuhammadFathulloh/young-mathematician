using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShapeCollection", menuName = "ScriptableObjects/ComposeShape", order = 10)]
public class ComposingShapeSO : ScriptableObject
{

    public GameObject puzzle;

    [SerializeField]
    public Dictionary<int, List<GameObject>> questionShapeDict = new Dictionary<int, List<GameObject>>(5);

    [SerializeField]
    public List<GameObject> list1 = new List<GameObject>();
    [SerializeField]
    public List<GameObject> list2 = new List<GameObject>();
    [SerializeField]
    public List<GameObject> list3 = new List<GameObject>();
    [SerializeField]
    public List<GameObject> list4 = new List<GameObject>();
    [SerializeField]
    public List<GameObject> list5 = new List<GameObject>();



    private void OnEnable()
    {
        questionShapeDict.Add(1, list1);
        questionShapeDict.Add(2, list2);
        questionShapeDict.Add(3, list3);
        questionShapeDict.Add(4, list4);
        questionShapeDict.Add(5, list5);


    }



}
