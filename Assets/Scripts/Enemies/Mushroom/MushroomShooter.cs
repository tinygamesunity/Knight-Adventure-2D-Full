using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomShooter : MonoBehaviour, IEnemyShooter {

    [SerializeField] private GameObject mushroomProjectilePrefab;
    [SerializeField] private MushroomVisual mushroomVisual;
    [SerializeField] private Transform projectileSpawnPosition;

    private void Start() {
        mushroomVisual.OnAttackAnimationFinished += MushroomVisual_OnAttackAnimationFinished;
    }

    private void MushroomVisual_OnAttackAnimationFinished(object sender, EventArgs e) {
        Instantiate(mushroomProjectilePrefab, projectileSpawnPosition.position, Quaternion.identity);
    }

    public void Attack() {

    }

    

}
