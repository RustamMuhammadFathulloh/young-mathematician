using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PoyezdTenglama
{
    public class TrainVagons : MonoBehaviour, IPointerClickHandler
    {

        public GameManager gm;
        public TrainTenglama trainTenglama;
        public bool isCorrect;
        public bool chiqish;
        public void OnPointerClick(PointerEventData eventData)
        {
            chiqish = true;
            if (isCorrect)
            {
                gm.correctEvent.Invoke();
                trainTenglama.DisableCollider();
            }
            else
            {
                gm.wrongEvent.Invoke();
                Instantiate(gm.loseParticle, transform.position, Quaternion.identity);
                gm.MinusLife();
            }
        }

    }
}
