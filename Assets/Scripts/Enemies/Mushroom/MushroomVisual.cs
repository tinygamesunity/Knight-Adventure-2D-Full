using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DP.Utils;
using System;

public class MushroomVisual : MonoBehaviour {

    [SerializeField] private EnemyEntity enemyEntity;
    [SerializeField] private EnemyAI enemyAI;
    [SerializeField] private GameObject enemyShadow;

    public event EventHandler OnAttackAnimationFinished;

    private const string IS_RUNNING = "IsRunning";
    private const string WALKING_SPEED_MULTIPLIER = "WalkingSpeedMultiplier";
    private const string TAKEHIT = "TakeHit";
    private const string IS_DIE = "IsDie";
    private const string ATTACK = "Attack";

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        enemyEntity.OnDeath += EnemyEntity_OnDeath;
        enemyEntity.OnTakeHit += EnemyEntity_OnTakeHit;
        enemyAI.OnAttack += EnemyAI_OnAttack;
    }

    private void EnemyAI_OnAttack(object sender, System.EventArgs e) {
        animator.SetTrigger(ATTACK);
    }

    private void Update() {
        animator.SetBool(IS_RUNNING, enemyAI.IsRunning());
        animator.SetFloat(WALKING_SPEED_MULTIPLIER, enemyAI.GetWalkingAnimationSpeed());
    }

    private void EnemyEntity_OnTakeHit(object sender, System.EventArgs e) {
        animator.SetTrigger(TAKEHIT);
    }

    public void SpawnProjectileAnimEvent() {
        OnAttackAnimationFinished?.Invoke(this, EventArgs.Empty);
    }

    private void EnemyEntity_OnDeath(object sender, System.EventArgs e) {
        animator.SetBool(IS_DIE, true);
        spriteRenderer.sortingOrder = -1;
        enemyShadow.SetActive(false);
    }
}
