using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

namespace ActionManager
{
    public class Actions : MonoBehaviour
    {
        


        public static List<GameObject> ShuffleList(List<GameObject> list)
        {
            list = list.OrderBy(x => Random.value).ToList();
            return list;
        }

        public static float GetPercentageRelativeToMain(float main, float piecePos)
        {
            float per = 0;
            per = piecePos * 100 / main;
            return per;
        }


        public static float GetScreenToWorldHeight
        {
            get
            {
                Vector2 topRightCorner = new Vector2(1, 1);
                Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
                var height = edgeVector.y * 2;
                return height;
            }
        }


        public static float GetScreenToWorldWidth
        {
            get
            {
                Vector2 topRightCorner = new Vector2(1, 1);
                Vector2 edgeVector = Camera.main.ViewportToWorldPoint(topRightCorner);
                var width = edgeVector.x * 1.65f;
                return width;
            }
        }

       


        public static Texture2D ConvertSpriteToTexture(SpriteRenderer spriteObj)
        {
            Texture2D source;

            var croppedTexture = new Texture2D((int)spriteObj.sprite.rect.width, (int)spriteObj.sprite.rect.height);
            var pixels = spriteObj.sprite.texture.GetPixels((int)spriteObj.sprite.textureRect.x,
                                                    (int)spriteObj.sprite.textureRect.y,
                                                    (int)spriteObj.sprite.textureRect.width,
                                                    (int)spriteObj.sprite.textureRect.height);
            croppedTexture.SetPixels(pixels);
            croppedTexture.Apply();
            source = croppedTexture;
            return source;
        }


        public static float AnimationTime(Animator animator, string animationName)
        {
            float time = 0;
            RuntimeAnimatorController ac = animator.runtimeAnimatorController;    //Get Animator controller
            for (int i = 0; i < ac.animationClips.Length; i++)                 //For all animations
            {
                if (ac.animationClips[i].name == animationName)        //If it has the same name as your clip
                {
                    time = ac.animationClips[i].length;
                }
            }

            Debug.Log("time of the aniamtion: " + time);
            return time;
        }

        public static IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
        {
            float elapsedTime = 0;
            Vector3 startingPos = objectToMove.transform.position;
            while (elapsedTime < seconds)
            {
                if (!objectToMove) yield break;

                objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            objectToMove.transform.position = end;
        }

        public static IEnumerator MoveOverSpeed(GameObject objectToMove, Vector3 end, float speed = 20f)
        {
            
            while (objectToMove.transform.position != end)
            {
                if (!objectToMove) yield break;
                objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }

        public static List<GameObject> ChildrenOfGameobject(GameObject obj)
        {
            List<GameObject> children = new List<GameObject>();
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                children.Add(obj.transform.GetChild(i).gameObject);
            }
            return children;
        }

        public static IEnumerator ScaleOverSeconds(GameObject objectToScale, Vector3 scaleTo, float seconds)
        {
            float elapsedTime = 0;
            Vector3 startingScale = objectToScale.transform.localScale;
            while (elapsedTime < seconds)
            {
                if (!objectToScale) yield break;
                objectToScale.transform.localScale = Vector3.Lerp(startingScale, scaleTo, (elapsedTime / seconds));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            objectToScale.transform.localScale = scaleTo;
        }

        public static IEnumerator RotateOverSeconds(GameObject objectToRotate, float z, float seconds)
        {
            float elapsedTime = 0;
            Quaternion startingScale = objectToRotate.transform.localRotation;
            while (elapsedTime < seconds)
            {
                if (!objectToRotate) yield break;
                objectToRotate.transform.localRotation = Quaternion.Lerp(startingScale, Quaternion.Euler(0, 0, z), (elapsedTime / seconds));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            objectToRotate.transform.localRotation = Quaternion.Euler(0, 0, z);
        }

        public static IEnumerator RotateOverSecondsInX(GameObject objectToRotate, float x, float seconds)
        {
            float elapsedTime = 0;
            Quaternion startingScale = objectToRotate.transform.localRotation;
            while (elapsedTime < seconds)
            {
                if (!objectToRotate) yield break;
                objectToRotate.transform.localRotation = Quaternion.Lerp(startingScale, Quaternion.Euler(x, 0, 0), (elapsedTime / seconds));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            objectToRotate.transform.localRotation = Quaternion.Euler(x, 0,0);
        }


        public static IEnumerator RotateOverSecondsInYAxis(GameObject objectToRotate, float y, float seconds)
        {
            float elapsedTime = 0;
            Quaternion startingScale = objectToRotate.transform.localRotation;
            while (elapsedTime < seconds)
            {
                if (!objectToRotate) yield break;
                objectToRotate.transform.localRotation = Quaternion.Lerp(startingScale, Quaternion.Euler(0, y, 0), (elapsedTime / seconds));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            objectToRotate.transform.localRotation = Quaternion.Euler(0, y, 0);
        }

        public static IEnumerator RotateOverSeconds(GameObject objectToRotate, Quaternion rotateTo, float seconds)
        {
            float elapsedTime = 0;
            Quaternion startingScale = objectToRotate.transform.localRotation;
            while (elapsedTime < seconds)
            {
                if (!objectToRotate) yield break;
                objectToRotate.transform.localRotation = Quaternion.Lerp(startingScale, rotateTo, (elapsedTime / seconds));
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            objectToRotate.transform.localRotation = rotateTo;
        }

        public static IEnumerator ChangeColorOverTime(Image image, Color end, float duration)
        {
            Color start = image.GetComponent<Image>().color;
            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                if (!image) yield break;
                float normalizedTime = t / duration;
                
                image.GetComponent<Image>().color = Color.Lerp(start, end, normalizedTime);
                yield return null;
            }
            image.GetComponent<Image>().color = end; 
        }

        public static IEnumerator FadeOverTime(GameObject image, float opacity, float duration)
        {
            Color start = image.GetComponent<SpriteRenderer>().color;
            Color end = new Color(1f, 1f, 1f, opacity);
            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                if (!image) yield break;
                float normalizedTime = t / duration;
                
                image.GetComponent<SpriteRenderer>().color = Color.Lerp(start, end, normalizedTime);
                yield return null;
            }
            image.GetComponent<SpriteRenderer>().color = end; 
        }

        public static IEnumerator FadeOverTime(Image image, float opacity, float duration)
        {
            Color start = image.color;
            Color end = new Color(1f, 1f, 1f, opacity);
            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                if (!image) yield break;
                float normalizedTime = t / duration;
                
                image.color = Color.Lerp(start, end, normalizedTime);
                yield return null;
            }
            image.color = end; 
        }

        public static IEnumerator FadeOverTime(SpriteRenderer sprite, float opacity, float duration)
        {
            Color start = sprite.color;
            Color end = new Color(1f, 1f, 1f, opacity);
            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                if (!sprite) yield break;
                float normalizedTime = t / duration;
                
                sprite.color = Color.Lerp(start, end, normalizedTime);
                yield return null;
            }
            sprite.color = end; 
        }

        public static IEnumerator ChangeColorOverTime(GameObject gameObject, Color end, float duration)
        {
            Color start = gameObject.GetComponent<SpriteRenderer>().color;
            for (float t = 0f; t < duration; t += Time.deltaTime)
            {
                if (!gameObject) yield break;
                float normalizedTime = t / duration;
                
                gameObject.GetComponent<SpriteRenderer>().color = Color.Lerp(start, end, normalizedTime);
                yield return null;
            }
            gameObject.GetComponent<SpriteRenderer>().color = end; 
        }

        public static void LoadScene(string nameOfScene)
        {
            SceneManager.LoadScene(nameOfScene);
        }

        public static Vector2 getScreenSizeInPoints(float percentage = 1.0f)
        {
            return Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * percentage, Screen.height * percentage, 0));
        }

        public static Vector2 ConvertV3ToV2(Vector3 position)
        {
            return new Vector3(position.x, position.y, 0);
        }

        public static bool IsObjectInCollider(GameObject obj, GameObject colliderObject)
        {
            return colliderObject.GetComponent<Collider2D>() == Physics2D.OverlapPoint(new Vector3(obj.transform.position.x, obj.transform.position.y, 0));
        }

        public static IEnumerator ActionWrong(MonoBehaviour monoBehaviour, GameObject obj)
        {
            if (!obj) yield return null;

            monoBehaviour.StartCoroutine(RotateOverSeconds(obj, 15, 0.1f));
            yield return new WaitForSeconds(0.1f);
            monoBehaviour.StartCoroutine(RotateOverSeconds(obj, -15, 0.2f));
            yield return new WaitForSeconds(0.2f);
            monoBehaviour.StartCoroutine(RotateOverSeconds(obj, 15, 0.2f));
            yield return new WaitForSeconds(0.2f);
            monoBehaviour.StartCoroutine(RotateOverSeconds(obj, 0, 0.1f));
            yield return new WaitForSeconds(0.1f);
        }

        public static IEnumerator ActionWrong(MonoBehaviour monoBehaviour, GameObject obj, float zAxis)
        {
            if (!obj) yield return null;

            monoBehaviour.StartCoroutine(RotateOverSeconds(obj, zAxis + 15, 0.1f));
            yield return new WaitForSeconds(0.1f);
            monoBehaviour.StartCoroutine(RotateOverSeconds(obj, zAxis - 15, 0.2f));
            yield return new WaitForSeconds(0.2f);
            monoBehaviour.StartCoroutine(RotateOverSeconds(obj, zAxis + 15, 0.2f));
            yield return new WaitForSeconds(0.2f);
            monoBehaviour.StartCoroutine(RotateOverSeconds(obj, zAxis, 0.1f));
            yield return new WaitForSeconds(0.1f);
        }

    }
}