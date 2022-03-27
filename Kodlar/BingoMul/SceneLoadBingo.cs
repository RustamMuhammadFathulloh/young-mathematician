using BingoMul;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ActionManager;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoadBingo : MonoBehaviour
{

    public OperationType operation;
    const string key = "EnumValue";
    public string levelSceneName;
    public string sceneName;
    
    Vector3 initialScale;

    private void Awake()
    {
        string loadString = PlayerPrefs.GetString(key);
        System.Enum.TryParse(loadString, out OperationType loadState);
        operation = loadState;
        initialScale = new Vector3(1, 1, 0);
        switch (operation)
        {
            case OperationType.Plus:

                break;
            case OperationType.Minus:

                break;
            case OperationType.Multiply:
                levelSceneName = "BingoMul";
                sceneName = "BingoMul_1";
                break;
            case OperationType.Division:
                levelSceneName = "BingoDiv";
                sceneName = "BingoMul_1";
                break;
            default:
                Debug.Log("NOTHING");
                break;
        }

    }

    public void LoadLevelScene()
    {
        StartCoroutine(AnimateObject(levelSceneName));
    }

    public void RePlay()
    {
        StartCoroutine(AnimateObject(sceneName));
    }

    IEnumerator AnimateObject(string scene)
    {

        StartCoroutine(Actions.ScaleOverSeconds(gameObject, initialScale * 0.5f, 0.1f));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Actions.ScaleOverSeconds(gameObject, initialScale, 0.1f));
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(scene);
    }


}
