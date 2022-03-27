using UnityEngine.SceneManagement;
using UnityEngine;


public class ScenesManager : MonoBehaviour
{

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
