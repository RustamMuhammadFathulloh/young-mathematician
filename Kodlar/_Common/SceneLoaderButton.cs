using System.Collections;
using ActionManager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoaderButton : MonoBehaviour
{
    public string sceneName;
    Vector3 initialScale;
    Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        initialScale = new Vector3(1, 1, 0);
        button.onClick.AddListener(TaskOnClick);

    }

    public void TaskOnClick()
    {

        StartCoroutine(AnimateObject());

    }


    IEnumerator AnimateObject()
    {

        StartCoroutine(Actions.ScaleOverSeconds(gameObject, initialScale * 0.5f, 0.1f));
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(Actions.ScaleOverSeconds(gameObject, initialScale, 0.1f));
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(sceneName);
    }

  
}
