using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddCursorAnim : MonoBehaviour
{
    public GameObject fingerCursor;
    public GameObject threeByTwo;
    public GameObject twoByTwo;
    public LevelSO levelSO;
    public GameObject mainPrefab;

    public string sceneName;

    public List<GameObject> qushiladiganlar;
    public List<Sprite> sonlarSprite;
    public TMP_Text yigindiSon;
    public TMP_Text son1, son2, son3, son4;

    private void Awake()
    {
        switch (levelSO.level)
        {
            case 1:
                mainPrefab = twoByTwo;
                yigindiSon.text = "40";
                break;
            case 2:
                mainPrefab = twoByTwo;
                yigindiSon.text = "40";
                break;
            case 3:
                mainPrefab = threeByTwo;
                yigindiSon.text = "96";
                break;
            case 4:
                mainPrefab = threeByTwo;
                yigindiSon.text = "96";
                break;
            default:
                break;
        }

        mainPrefab.SetActive(true);

        if (levelSO.level > 2)
        {
            for (int i = 0; i < 4; i++)
            {
                qushiladiganlar[i].SetActive(true);
            }
        }
        else if (levelSO.level <= 2) 
        {
            for (int i = 0; i < 2; i++)
            {
                qushiladiganlar[i].SetActive(true);
            }
        }
        TakeAllPositions();
    }



    void Start()
    {
        
        if (mainPrefab.transform.GetChild(1).transform.childCount == 4) 
        {
            StartCoroutine(FingerCursorAnim4());
        }
        else if (mainPrefab.transform.GetChild(1).transform.childCount >= 4)
        {
            
            StartCoroutine(FingerCursorAnim6());
        }   

    }


    public List<Vector3> positionOfChild;
    public void TakeAllPositions()
    {
        for (int i = 0; i < mainPrefab.transform.GetChild(1).transform.childCount; i++)
        {
            Vector3 pos = mainPrefab.transform.GetChild(1).transform.GetChild(i).transform.position;
            
            positionOfChild.Add(pos);
        }
    }



    /// <summary>
    /// FingerCursor Animatsiyasini qilib beruvchi method.
    /// </summary>
    /// <returns></returns>
    public IEnumerator FingerCursorAnim4()
    {
        float vaqt = 1.6f;
        float lahza = 0.6f;
        yield return new WaitForSeconds(1);
        Vector3 initialPosFingerCursor = fingerCursor.transform.position;

        fingerCursor.transform.DOMove(positionOfChild[2], vaqt);
        yield return new WaitForSeconds(vaqt);
        fingerCursor.transform.DOScale(0.8f, lahza);
        yield return new WaitForSeconds(lahza);

        fingerCursor.transform.DOMove(positionOfChild[3], lahza);
        fingerCursor.transform.DOScale(1, lahza);
        mainPrefab.transform.GetChild(1).transform.GetChild(2).transform.DOMove(positionOfChild[3], lahza);
        yield return new WaitForSeconds(lahza);

        fingerCursor.transform.DOMove(positionOfChild[0], lahza);
        yield return new WaitForSeconds(lahza);
        fingerCursor.transform.DOScale(0.8f, lahza);
        yield return new WaitForSeconds(lahza);

        fingerCursor.transform.DOMove(positionOfChild[2], lahza);
        fingerCursor.transform.DOScale(1, lahza);
        mainPrefab.transform.GetChild(1).transform.GetChild(0).transform.DOMove(positionOfChild[2], lahza);
        yield return new WaitForSeconds(lahza);

        fingerCursor.transform.DOScale(0.8f, lahza);
        yield return new WaitForSeconds(lahza);

        fingerCursor.transform.DOMove(positionOfChild[3], lahza);
        fingerCursor.transform.DOScale(1, lahza);
        mainPrefab.transform.GetChild(1).transform.GetChild(0).transform.DOMove(positionOfChild[3], lahza);

        mainPrefab.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sonlarSprite[4];
        mainPrefab.transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = sonlarSprite[0];
        mainPrefab.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().DOColor(new Color(0, 1, 0.5f, 1), 0);

        mainPrefab.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(false);
        son3.text = "12";
        son4.text = "28";
        yield return new WaitForSeconds(lahza);

        fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.8f);
        yield return new WaitForSeconds(1.2f);
        fingerCursor.transform.DOMove(initialPosFingerCursor, 0);
        fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.8f);
        yield return new WaitForSeconds(lahza);
        LoadingGameScene(sceneName);
    }



    /// <summary>
    /// FingerCursor Animatsiyasini qilib beruvchi method.
    /// </summary>
    /// <returns></returns>
    public IEnumerator FingerCursorAnim6()
    {
        float vaqt = 1.8f;
        float lahza = 0.6f;
        yield return new WaitForSeconds(1f);
        Vector3 initialPosFingerCursor = fingerCursor.transform.position;


        //Birinchi va ikkinchi sonlarni qo'shish.
        fingerCursor.transform.DOMove(positionOfChild[0], vaqt);
        yield return new WaitForSeconds(vaqt);
        fingerCursor.transform.DOScale(0.8f, lahza);
        yield return new WaitForSeconds(lahza);

        fingerCursor.transform.DOMove(positionOfChild[1], lahza);
        fingerCursor.transform.DOScale(1f, lahza);
        mainPrefab.transform.GetChild(1).transform.GetChild(0).transform.DOMove(positionOfChild[1], lahza);
        // Kerakli spritelarga o'zgaradi.
        mainPrefab.transform.GetChild(1).transform.GetChild(0).transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sonlarSprite[3];
        mainPrefab.transform.GetChild(1).transform.GetChild(0).transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = sonlarSprite[8];

        mainPrefab.transform.GetChild(1).transform.GetChild(0).GetComponent<SpriteRenderer>().DOColor(new Color(0, 1, 0.5f, 1), 0.2f);
        mainPrefab.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(lahza);

        son1.text = "21";
        son2.text = "17";


        // UChinchi va to'rtinchi sonlarni qo'shib beradi.
        fingerCursor.transform.DOMove(positionOfChild[4], lahza);
        yield return new WaitForSeconds(lahza);
        fingerCursor.transform.DOScale(0.8f, lahza);
        yield return new WaitForSeconds(lahza);

        fingerCursor.transform.DOMove(positionOfChild[3], lahza);
        mainPrefab.transform.GetChild(1).transform.GetChild(4).transform.DOMove(positionOfChild[3], lahza);
        fingerCursor.transform.DOScale(1, lahza);

        // Kerakli spritelarga o'zgaradi.
        mainPrefab.transform.GetChild(1).transform.GetChild(4).transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sonlarSprite[5];
        mainPrefab.transform.GetChild(1).transform.GetChild(4).transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = sonlarSprite[8];

        mainPrefab.transform.GetChild(1).transform.GetChild(4).GetComponent<SpriteRenderer>().DOColor(new Color(0, 1, 0.5f, 1), 0.2f);
        mainPrefab.transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(false);
        yield return new WaitForSeconds(lahza);

        son3.text = "10";
        son4.text = "48";


        // Ikkita yig'indini qo'shish uchun tepadagi yig'indini pastga tushuradi.
        fingerCursor.transform.DOMove(positionOfChild[1], lahza);
        yield return new WaitForSeconds(lahza);
        fingerCursor.transform.DOScale(0.8f, lahza);
        yield return new WaitForSeconds(lahza);

        fingerCursor.transform.DOMove(positionOfChild[4], lahza);
        mainPrefab.transform.GetChild(1).transform.GetChild(0).transform.DOMove(positionOfChild[4], lahza);
        fingerCursor.transform.DOScale(0.8f, lahza);
        yield return new WaitForSeconds(lahza);


        //Ikkita yig'indini qo'shib hisoblab beradigan kodlar;
        fingerCursor.transform.DOMove(positionOfChild[3], lahza);
        yield return new WaitForSeconds(lahza);
        fingerCursor.transform.DOScale(0.8f, lahza);
        yield return new WaitForSeconds(lahza);


        fingerCursor.transform.DOMove(positionOfChild[4], lahza);
        mainPrefab.transform.GetChild(1).transform.GetChild(4).transform.DOMove(positionOfChild[4], lahza);
        fingerCursor.transform.DOScale(1, lahza);

        // Kerakli spritelarga o'zgaradi.
        mainPrefab.transform.GetChild(1).transform.GetChild(4).transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = sonlarSprite[9];
        mainPrefab.transform.GetChild(1).transform.GetChild(4).transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = sonlarSprite[6];

        mainPrefab.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
        yield return new WaitForSeconds(lahza);

        fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 0), 0.8f);
        yield return new WaitForSeconds(1.2f);
        fingerCursor.transform.DOMove(initialPosFingerCursor, 0);
        fingerCursor.GetComponent<SpriteRenderer>().DOColor(new Color(1, 1, 1, 1), 0.8f);
        yield return new WaitForSeconds(lahza);

        LoadingGameScene(sceneName);
    }
    

    /// <summary>
    /// Kerakli scene ni yuklab beruvchi kod.
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadingGameScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
