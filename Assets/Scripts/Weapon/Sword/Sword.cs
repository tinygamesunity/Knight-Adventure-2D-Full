using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : BaseWeapon {

    [SerializeField] private WeaponSO weaponSO;

    public event EventHandler OnSwingDown;
    private PolygonCollider2D polygonCollider2D;

    private void Awake() {
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private void Start() {
        SwordColliderTurnOff();
    }

    public override void Attack() {
        // turn off and turn on collider if it was turned on to fire trigger
        SwordColliderTurnOffOn();

        OnSwingDown?.Invoke(this, EventArgs.Empty);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.TryGetComponent(out EnemyEntity enemyEntity)) {
            enemyEntity.TakeDamage(weaponSO.weaponDamageAmout);
        }
    }

    public void SwordColliderTurnOff() {
        polygonCollider2D.enabled = false;
    }

    private void SwordColliderTurnOn() {
        polygonCollider2D.enabled = true;
    }

    private void SwordColliderTurnOffOn() {
        SwordColliderTurnOff();
        SwordColliderTurnOn();
    }
}
