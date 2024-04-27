using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class RoutineManager : MonoBehaviour {

    public static RoutineManager Instance { get; private set; }

    public bool isAlive;

    private void Awake() {
        Instance = this;
        SetIsAlive(true);
    }

    //private IEnumerator FadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetValue) {
    //    float elapsedTime = 0f;
    //    while (elapsedTime < fadeTime) {
    //        elapsedTime += Time.deltaTime;
    //        float newAlpha = Mathf.Lerp(startValue, 1 - targetValue, elapsedTime / fadeTime);
    //        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, newAlpha);
    //        yield return null;
    //    }
    //}

    //private IEnumerator FadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetValue) {
    //    float elapsedTime = 0f;
    //    while (elapsedTime < fadeTime) {
    //        elapsedTime += Time.deltaTime;
    //        float newAlpha = Mathf.Lerp(startValue, 1 - targetValue, elapsedTime / fadeTime);
    //        tilemap.color = new Color(tilemap.color.r, tilemap.color.g, tilemap.color.b, newAlpha);
    //        yield return null;
    //    }
    //}

    //private IEnumerator FadeRoutine(Image fadeImage, float targetAlpha, float fadeDuration) {
    //    while (!Mathf.Approximately(fadeImage.color.a, targetAlpha)) {
    //        float alpha = Mathf.MoveTowards(fadeImage.color.a, targetAlpha, fadeDuration * Time.deltaTime);
    //        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
    //        yield return null;
    //    }
    //}

    //public void StartFadeRoutine(Image fadeImage, float targetAlpha, float fadeDuration) {
    //    if (isAlive) {
    //        StartCoroutine(FadeRoutine(fadeImage, targetAlpha, fadeDuration));
    //    }
    //}

    //public void StartFadeRoutine(Tilemap tilemap, float fadeTime, float startValue, float targetValue) {
    //    if (isAlive) {
    //        StartCoroutine(FadeRoutine(tilemap, fadeTime, startValue, targetValue));
    //    }
    //}


    //public void StartFadeRoutine(SpriteRenderer spriteRenderer, float fadeTime, float startValue, float targetValue) {
    //    if (isAlive) {
    //        StartCoroutine(FadeRoutine(spriteRenderer, fadeTime, startValue, targetValue));
    //    }
    //}

    public void SetIsAlive(bool isAlive) {
        this.isAlive = isAlive;
    }

    //public void StopAllSceneCoroutines() {
    //    StopAllCoroutines();

    //    //TransparentDetection f = FindObjectOfType<TransparentDetection>();
    //    //f.StopAllCoroutines();

    //}

}
