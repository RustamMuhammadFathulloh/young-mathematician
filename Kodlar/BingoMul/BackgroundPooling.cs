using UnityEngine;


namespace BingoMul
{
    public class BackgroundPooling : MonoBehaviour
    {

        public GameObject topObj;
        
        private Vector3 _moveVector;
        private float _backgroundOriginalSizeX = 0;
        private float _backgroundOriginalSizeY = 0;
        Vector3 topPos;


        private void Awake()
        {
            topPos = topObj.transform.position;
            _moveVector = new Vector3(0, 0.5f, 0);
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            var originalSize = sr.size;
            _backgroundOriginalSizeX = originalSize.x;
            _backgroundOriginalSizeY = originalSize.y;
        }


        
        private void Update()
        {
            transform.Translate(-_moveVector.x * Time.deltaTime, -_moveVector.y * Time.deltaTime, 0);
            if (transform.position.y <= -_backgroundOriginalSizeY)
            {                
                transform.Translate(0, topPos.y *2, 0);
            }
        }
    }

}

