using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Shooter : MonoBehaviour, IEnemyShooter {

    [SerializeField] private GameObject bulletPrefab;
 //   [SerializeField] private Transform projectileSpawnPosition;
    [SerializeField] private float bulletMovingSpeed = 5f;
    [SerializeField] private int burstCount = 3;
    [SerializeField] private int projectilePerBurst = 5;
    [SerializeField][Range(0, 359)] private float angleSpread;
    [SerializeField] private float startingBulletSpawnDistance = 0.1f;
    [SerializeField] private bool stagger;
    [Tooltip("Stagger has to be enabled for oscillate to work properly.")]
    [SerializeField] private bool oscillate;

    private float fireRate;
    private EnemyAI enemyAI;

    private void OnValidate() {
        if (oscillate) { stagger = true; }
        if (!oscillate) { stagger = false; }
        if (projectilePerBurst < 1) { projectilePerBurst = 1; }
        if (burstCount < 1) { burstCount = 1; }
        if (startingBulletSpawnDistance < 0.1f) { startingBulletSpawnDistance = 0.1f; }
        if (angleSpread == 0) { projectilePerBurst = 1; }
        if (bulletMovingSpeed <= 0.1f) { bulletMovingSpeed = 0.1f; }
    }

    private void Awake() {
        enemyAI = GetComponent<EnemyAI>();
    }

    private void Start() {
        fireRate = enemyAI.GetFireRate();
    }

    public void Attack() {
        StartCoroutine(ShootRoutine());
    }

    private IEnumerator ShootRoutine() {
        float startAngle, currentAngle, angleStep, endAngle;
        CalculateConeOfBurst(out startAngle, out currentAngle, out angleStep, out endAngle);

        float timeBetweenProjectiles = 0f;
        if (stagger) {
            timeBetweenProjectiles = fireRate / projectilePerBurst;
        }

        for (int i = 0; i < burstCount; i++) {

            if (!oscillate) {
                CalculateConeOfBurst(out startAngle, out currentAngle, out angleStep, out endAngle);
            }

            if (oscillate && i % 2 == 1) {
                CalculateConeOfBurst(out startAngle, out currentAngle, out angleStep, out endAngle);
            } else if (oscillate) {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }


            for (int j = 0; j < projectilePerBurst; j++) {

                Vector2 pos = FindBulletSpawnPos(currentAngle);

                GameObject bullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                bullet.transform.right = bullet.transform.position - transform.position;

                if (bullet.TryGetComponent(out EnemyProjectile projectile)) {
                    projectile.SetProjectileMovingSpeed(bulletMovingSpeed);
                }

                currentAngle += angleStep;
                if (stagger) {
                    yield return new WaitForSeconds(timeBetweenProjectiles);
                }
            }

            if (!stagger) {
                yield return new WaitForSeconds(fireRate);
            }
        }
    }

    private void CalculateConeOfBurst(out float startAngle, out float currentAngle, out float angleStep, out float endAngle) {
        Vector2 targetDirection = Player.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0f;
        if (angleSpread != 0f) {
            angleStep = angleSpread / (projectilePerBurst - 1);
            halfAngleSpread = angleSpread / 2;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    private Vector2 FindBulletSpawnPos(float currentAngle) {
        float x = transform.position.x + startingBulletSpawnDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingBulletSpawnDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }


}
