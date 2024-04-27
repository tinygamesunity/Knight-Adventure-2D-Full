using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : BaseProjectile {

    [SerializeField] private ProjectileSO projectileSO;

    public event EventHandler OnSpawnProjectile;

    private Vector3 startPoint;
    private float projectileMovingSpeed;

    private void Awake() {
        projectileMovingSpeed = projectileSO.projectileMovingSpeed;
    }

    private void Start() {
        startPoint = transform.position;
        OnSpawnProjectile?.Invoke(this, EventArgs.Empty);
    }

    private void FixedUpdate() {
        MoveProjectile(projectileMovingSpeed);
        DetectFireDistance(projectileSO, startPoint);
    }

    protected override void OnTriggerEnter2D(Collider2D collision) {
        base.OnTriggerEnter2D(collision);

        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity)) {
            enemyEntity.TakeDamage(projectileSO.projectileDamageAmout);
            Destroy(gameObject);
            base.RaiseOnMeetObstacleEvent();
        }
    }

    public void SetProjectileMovingSpeed(float projectileMovingSpeed) {
        this.projectileMovingSpeed = projectileMovingSpeed;
    }

}
