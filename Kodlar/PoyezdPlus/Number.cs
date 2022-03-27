using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace PoyezdPlus
{
    public class Number : MonoBehaviour, IPointerClickHandler
    {

        public GameManager gm;
        public Train train;
        public bool isCorrect;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (isCorrect)
            {
                gm.correctEvent.Invoke();
                train.DisableCollider();
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

