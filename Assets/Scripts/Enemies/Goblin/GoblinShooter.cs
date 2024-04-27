using System;
using UnityEngine;

public class GoblinShooter : MonoBehaviour, IEnemyShooter {

    [SerializeField] private GameObject goblinProjectilePrefab;
    [SerializeField] private GoblinVisual goblinVisual;
    [SerializeField] private Transform projectileSpawnPosition;

    private void Start() {
        goblinVisual.OnAttackAnimationFinished += GoblinVisual_OnAttackAnimationFinished;
    }

    private void GoblinVisual_OnAttackAnimationFinished(object sender, EventArgs e) {
        Instantiate(goblinProjectilePrefab, projectileSpawnPosition.position, Quaternion.identity);
    }

    public void Attack() {

    }

}
