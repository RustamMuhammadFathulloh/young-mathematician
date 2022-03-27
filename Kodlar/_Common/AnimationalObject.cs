using System.Collections;
using ActionManager;
using UnityEngine;


public class AnimationalObject : MonoBehaviour
{
    Vector3 initialScale;

    public float repeatAfterPeriod;

    private void Awake()
    {

        initialScale = transform.localScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AnimateObject());

    }


    IEnumerator AnimateObject()
    {
        yield return new WaitForSeconds(1);
        StartCoroutine(Actions.ScaleOverSeconds(gameObject, initialScale * 0.5f, 0.2f));
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(Actions.ScaleOverSeconds(gameObject, initialScale * 1.5f, 0.2f));
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(Actions.ScaleOverSeconds(gameObject, initialScale, 0.2f));

        yield return new WaitForSeconds(repeatAfterPeriod);

        StartCoroutine(AnimateObject());

    }
}
