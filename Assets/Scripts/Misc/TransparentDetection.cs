using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using DP.Utils;

public class TransparentDetection : MonoBehaviour {

    [Range(0f, 1f)]
    [SerializeField] private float transparencyAmount = 0.4f;
    [SerializeField] private float fadeTime = 0.4f;


    private SpriteRenderer spriteRenderer;
    private Tilemap tilemap;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        tilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Player>()) {
            if (collision is CapsuleCollider2D) {
                if (spriteRenderer) {
                    StartCoroutine(Utils.FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, transparencyAmount));
                    //RoutineManager.Instance.StartFadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, transparencyAmount);
                } else if (tilemap) {
                    StartCoroutine(Utils.FadeRoutine(tilemap, fadeTime, tilemap.color.a, transparencyAmount));
                    //RoutineManager.Instance.StartFadeRoutine(tilemap, fadeTime, tilemap.color.a, transparencyAmount);
                }
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<Player>()) {
            if (collision is CapsuleCollider2D) {
                if (spriteRenderer) {
                    StartCoroutine(Utils.FadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, 0.0f));
                    //RoutineManager.Instance.StartFadeRoutine(spriteRenderer, fadeTime, spriteRenderer.color.a, 0.0f);
                } else if (tilemap) {
                    StartCoroutine(Utils.FadeRoutine(tilemap, fadeTime, tilemap.color.a, 0.0f));
                    //RoutineManager.Instance.StartFadeRoutine(tilemap, fadeTime, tilemap.color.a, 0.0f);
                }
            }
        }
    }

}
