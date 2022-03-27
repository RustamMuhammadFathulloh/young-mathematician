using ActionManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffects : MonoBehaviour
{
    public GameObject particlePrefab;
    Vector3 pos;
    public int maxStarNumber;
   
   

   
    private void Awake()
    {
        pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        pos = new Vector3(pos.x-2, pos.y-2, 0);
       

    }
   

    public void CreateParticles()
    {
        for (int i = 0; i < maxStarNumber; i++)
        {
            float posX = Random.Range(-pos.x, pos.x);
            float posY = Random.Range(-pos.y, pos.y);
            Instantiate(particlePrefab, new Vector3(posX, posY, 0), Quaternion.identity);
        }
    }

  

}
