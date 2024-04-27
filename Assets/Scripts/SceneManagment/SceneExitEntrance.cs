using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneExitEntrance : MonoBehaviour {

    [SerializeField] private ExitEntranceSO sceneTransitionRule;
    [SerializeField] private Transform entrancePoint;

    private float waitToLoadNewScene = 1f;

    private void Start () {
        if (sceneTransitionRule.sceneExitName == MySceneManagement.Instance.SceneTransitionName) {
            Player.Instance.transform.position = entrancePoint.transform.position;
            CameraController.Instance.SetPlayerCameraFollow();
            RoutineManager.Instance.SetIsAlive(true);
            FadeScreenUI.Instance.FadeScreen(targetAlpha: 0f);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Player>()) {
            MySceneManagement.Instance.SetSceneTransitonName(sceneTransitionRule.sceneLeadToExitName);
            FadeScreenUI.Instance.FadeScreen(targetAlpha: 1f);
            StartCoroutine(LoadSceneRoutine());
        }
    }

    private IEnumerator LoadSceneRoutine() {
        yield return new WaitForSeconds(waitToLoadNewScene);
        RoutineManager.Instance.SetIsAlive(false); 
      //  RoutineManager.Instance.StopAllSceneCoroutines();
        SceneManager.LoadScene(sceneTransitionRule.sceneLeadToInd);
    }
}
