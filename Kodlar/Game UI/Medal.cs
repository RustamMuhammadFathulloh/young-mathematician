
using UnityEngine;
using UnityEngine.UI;
using ActionManager;

public class Medal : MonoBehaviour
{
    Image img;
    public Medal medal1;
    public Medal medal2;
    public Sprite medalSp;
    public Sprite medalAward;

    private void Awake()
    {
        img = GetComponent<Image>();
        medalSp = img.sprite;
    }




    public void Maximize()
    {       
        StartCoroutine(Actions.ScaleOverSeconds(gameObject, new Vector3(1.4f, 1.4f, 0), 0.5f));
        medal1.Minimize();
        medal2.Minimize();
    }

    public void Minimize()
    {
        StartCoroutine(Actions.ScaleOverSeconds(gameObject, new Vector3(1, 1, 0), 0.5f));
    }

   
}
