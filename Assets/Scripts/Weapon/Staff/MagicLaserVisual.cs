using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicLaserVisual : MonoBehaviour {

    [SerializeField] private ProjectileSO projectileSO;
    [SerializeField] private float laserGrowTime = 2f;

    private SpriteRenderer spriteRenderer;
    private float spriteRendererSizeX;

    private CapsuleCollider2D capsuleCollider2D;
    private float capsuleCollider2DSizeX;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRendererSizeX = spriteRenderer.size.x;

        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        capsuleCollider2DSizeX = capsuleCollider2D.size.x;
    }

    private void Start() {
        StartCoroutine(IncreaseLaserLengthRoutine());
    }

    private IEnumerator IncreaseLaserLengthRoutine() {
        float timePassed = 0f;
        float laserRange = projectileSO.weaponSO.weaponProjectileRange;

        while (spriteRenderer.size.x < laserRange) {
            timePassed += Time.deltaTime;

            float linearTime = timePassed / laserGrowTime;

            spriteRenderer.size = new Vector2(Mathf.Lerp(spriteRendererSizeX, laserRange, linearTime), spriteRenderer.size.y);
            capsuleCollider2D.size = new Vector2(Mathf.Lerp(capsuleCollider2DSizeX, laserRange, linearTime), capsuleCollider2D.size.y);

            yield return null;
        }
    }

}
