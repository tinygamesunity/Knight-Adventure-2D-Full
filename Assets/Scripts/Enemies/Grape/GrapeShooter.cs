using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapeShooter : MonoBehaviour, IEnemyShooter {

    [SerializeField] private GameObject grapeProjectilePrefab;
    [SerializeField] private GrapeVisual grapeVisual;
    [SerializeField] private Transform projectileSpawnPosition;

    private void Start() {
        grapeVisual.OnAttackAnimationFinished += GrapeVisual_OnAttackAnimationFinished;
    }

    private void GrapeVisual_OnAttackAnimationFinished(object sender, EventArgs e) {
        Instantiate(grapeProjectilePrefab, projectileSpawnPosition.position, Quaternion.identity);
    }

    public void Attack() {

    }

    

}
