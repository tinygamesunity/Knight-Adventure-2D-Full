using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : MonoBehaviour {

    [SerializeField] private float duration = 1f;
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float heightY = 3f;
    [SerializeField] private GameObject bombProjectileShadowPrefab;
    [SerializeField] private GameObject bombProjectileSplatterPrefab;

    private void Start() {
        Vector3 endPosition = Player.Instance.transform.position;

        StartCoroutine(ProjectileCurveRoutine(transform.position, endPosition));

        GameObject grapeProjectileShadow = Instantiate(bombProjectileShadowPrefab, transform.position, Quaternion.identity);
        StartCoroutine(MoveGrapeShadowRoutine(grapeProjectileShadow, transform.position, endPosition));
    }

    private IEnumerator ProjectileCurveRoutine(Vector3 startPosition, Vector3 endPosition) {
        float timePassed = 0f;

        while (timePassed < duration) {
            timePassed += Time.deltaTime;

            float linearT = timePassed / duration;
            float heightT = animationCurve.Evaluate(linearT);
            float height = Mathf.Lerp(0f, heightY, heightT);

            transform.position = Vector2.Lerp(startPosition, endPosition, linearT) + new Vector2(0f, height);

            yield return null;
        }

        Instantiate(bombProjectileSplatterPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private IEnumerator MoveGrapeShadowRoutine(GameObject grapeProjectileShadow, Vector3 startPosition, Vector3 endPosition) {
        float timePassed = 0f;

        while (timePassed < duration) {
            timePassed += Time.deltaTime;
            float linearT = timePassed / duration;
            grapeProjectileShadow.transform.position = Vector2.Lerp(startPosition, endPosition, linearT);

            yield return null;
        }

       Destroy(grapeProjectileShadow.gameObject);
    }
}
