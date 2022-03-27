
using UnityEngine;

public class GameOrder : MonoBehaviour
{
    public string [] sceneNames;
    public SceneLoaderButton[] buttons;

    private void Awake()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].sceneName = sceneNames[i];
        }
    }

    
}
