using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using DP.Utils;

public class FadeScreenUI : Singleton<FadeScreenUI> {

    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;

    public void FadeScreen(float targetAlpha) {
        StartCoroutine(Utils.FadeRoutine(fadeImage, targetAlpha, fadeDuration));

       // RoutineManager.Instance.StartFadeRoutine(fadeImage, targetAlpha, fadeDuration);
    }

}
