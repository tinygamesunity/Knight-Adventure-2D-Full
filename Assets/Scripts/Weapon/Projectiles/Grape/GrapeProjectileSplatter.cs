using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeProjectileSplatter : MonoBehaviour {

    private SpriteFade spriteFade;
    [SerializeField] private ProjectileSO projectileSO;

    private void Awake() {
        spriteFade = GetComponent<SpriteFade>();
    }

    private void Start() {
        StartCoroutine(spriteFade.FadeRoutine());
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null && collision.isTrigger) {
            player.TakeDamage(this.transform, projectileSO.projectileDamageAmout);
        }
    }

}
