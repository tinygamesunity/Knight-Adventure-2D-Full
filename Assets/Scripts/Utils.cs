using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


namespace DP.Utils {

    /*
     * Various assorted utilities functions
     * */


    public static class Utils {

        public enum PickUpType {
            GoldCoin,
            Heart
        };

        // Generate random normalized direction
        public static Vector3 GetRandomDir() {
            return new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        }

        public static Vector3 GetGameObjectScreenPoint(Transform transform) {
            return Camera.main.WorldToScreenPoint(transform.position);
        }

        public static bool AnimatorHasParameter(Animator animator, string paramName) {
            foreach (AnimatorControllerParameter param in animator.parameters) {
                if (param.name == paramName)
                    return true;
            }
            return false;
        }

        public static IEnumerator FadeRoutine(Image fadeImage, float targetAlpha, float fadeDuration) {
            while (!Mathf.Approximately(fadeImage.color.a, targetAlpha)) {
                float alpha = Mathf.MoveTowards(fadeImage.color.a, targetAlpha, fadeDuration * Time.deltaTime);
                fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
                yield return null;
            }
        }

        public static IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetValue) {
            float elapsedTime = 0f;
            while (elapsedTime < fadeTime) {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, 1 - targetValue, elapsedTime / fadeTime);
                tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
                yield return null;
            }
        }

        public static IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetValue) {
            float elapsedTime = 0f;
            while (elapsedTime < fadeTime) {
                elapsedTime += Time.deltaTime;
                float newAlpha = Mathf.Lerp(startValue, 1 - targetValue, elapsedTime / fadeTime);
                spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
                yield return null;
            }
        }
    }
}
