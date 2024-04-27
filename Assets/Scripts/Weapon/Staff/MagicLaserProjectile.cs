using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class MagicLaserProjectile : BaseProjectile {

    [SerializeField] private Staff staff;
    [SerializeField] private ProjectileSO projectileSO;
    [SerializeField] private float laserGrowTime = 1f;

    private SpriteRenderer spriteRenderer;
    private float spriteRendererSizeX;

    private CapsuleCollider2D capsuleCollider2D;
    private float capsuleCollider2DSizeX;
    private float capsuleCollider2DOffsetX;

    private Vector3 startPoint;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRendererSizeX = spriteRenderer.size.x;

        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        capsuleCollider2DSizeX = capsuleCollider2D.size.x;
        capsuleCollider2DOffsetX = capsuleCollider2D.offset.x;
    }


    private void Start() {
        LaserFaceMouse();
        startPoint = transform.position;
        StartCoroutine(IncreaseLaserLengthRoutine());
    }

    private void Update() {
        DetectFireDistance(projectileSO, startPoint);
    }

    protected override void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity)) {
            enemyEntity.TakeDamage(projectileSO.projectileDamageAmout);
        }

        Indestructable indestructable = collision.gameObject.GetComponent<Indestructable>();
        if (indestructable != null && !collision.isTrigger) {
            Destroy(gameObject);
        }
    }

    private void LaserFaceMouse() {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector2 direction = Camera.main.WorldToScreenPoint(transform.position) - mousePos;
        transform.right = -direction;
    }

    private IEnumerator IncreaseLaserLengthRoutine() {
        float timePassed = 0f;
        float laserRange = projectileSO.weaponSO.weaponProjectileRange;

        while (spriteRenderer.size.x < laserRange) {
            timePassed += Time.deltaTime;

            float linearTime = timePassed / laserGrowTime;

            spriteRenderer.size = new Vector2(Mathf.Lerp(spriteRendererSizeX, laserRange, linearTime), spriteRenderer.size.y);
            capsuleCollider2D.size = new Vector2(Mathf.Lerp(capsuleCollider2DSizeX, laserRange, linearTime), capsuleCollider2D.size.y);
            capsuleCollider2D.offset = new Vector2(Mathf.Lerp(capsuleCollider2DOffsetX, laserRange, linearTime) / 2, capsuleCollider2D.offset.y);

            yield return null;
        }

        StartCoroutine(GetComponent<SpriteFade>().FadeRoutine());

    }

}
