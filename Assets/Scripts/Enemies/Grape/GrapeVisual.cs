using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DP.Utils;
using System;

public class GrapeVisual : MonoBehaviour {

    [SerializeField] private EnemyEntity enemyEntity;
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private GameObject deathVFXPrefab;

    public event EventHandler OnAttackAnimationFinished;

  //  private const string ATTACK = "IsAttacking";

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        enemyEntity.OnDeath += EnemyEntity_OnDeath;
    }

    private void Update() {
     //   if (Utils.AnimatorHasParameter(animator, ATTACK)) {
      //      animator.SetBool(ATTACK, enemyAI.IsAttacking());
     //   }
    }

    private void EnemyEntity_OnDeath(object sender, System.EventArgs e) {
        ShowDeathVFX();
    }

    private void ShowDeathVFX() {
        Instantiate(deathVFXPrefab, transform.position, Quaternion.identity);
    }

    public void SpawnProjectileAnimEvent() {
        OnAttackAnimationFinished?.Invoke(this, EventArgs.Empty);
    }
}
