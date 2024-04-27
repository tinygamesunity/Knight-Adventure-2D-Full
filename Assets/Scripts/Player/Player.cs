using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DP.Utils;

public class Player : Singleton<Player> {

    [SerializeField] private float movingSpeed = 15f;
    [SerializeField] private float dashSpeed = 4f;
    [SerializeField] private float dashTime = 0.2f;
    [SerializeField] private float dashCoolDownTime = 0.25f;
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private float damageRecoveryTime;
    [SerializeField] private ActiveWeapon activeWeapon;
    [SerializeField] private float delayAfterDeath = 2f;

    private Rigidbody2D rb;
    private KnockBack knockBack;

    public event EventHandler OnFlashBlink;
    public event EventHandler OnPlayerDeath;

    private Vector2 inputVector;
    private bool isRunning;
    private bool isAlive;
    private float minMovingSpeed = .1f;
    private float initialMovingSpeed;
    private bool isDashing = false;
    

    private int currentHealth;
    private bool canTakeDamage = true;

    private int currentGoldCoin = 0;

    protected override void Awake() {
        base.Awake();
        rb = GetComponent<Rigidbody2D>();
        knockBack = GetComponent<KnockBack>();
    }

    private void Start() {
        initialMovingSpeed = movingSpeed;
        currentHealth = maxHealth;
        isAlive = true;

        HealthBar.Instance.SetMaxHealth(maxHealth);
        GoldColnCounter.Instance.SetGoldCoinAmount(currentGoldCoin);

        GameInput.Instance.OnPlayerAttack += GameInput_OnPlayerAttack;
        GameInput.Instance.OnPlayerDash += GameInput_OnPlayerDash;
        GameInput.Instance.OnInventoryKeyboard += GameInput_OnInventoryKeyboard;
    }

    protected override void Update() {
        base.Update();
        inputVector = GameInput.Instance.GetMovementVector();
    }

    private void FixedUpdate() {
        if (knockBack.GettingKnockedBack) {
            return;
        }

        HandleMovement();
    }

    private void GameInput_OnInventoryKeyboard(object sender, GameInput.OnInventoryKeyboardEventArgs e) {
        ActiveWeapon.Instance.SetCurrentWeaponSO(e.pressedKeyboardKey);
    }

    private void GameInput_OnPlayerDash(object sender, System.EventArgs e) {
        Dash();
    }

    private void GameInput_OnPlayerAttack(object sender, System.EventArgs e) {
        ActiveWeapon.Instance.GetCurrentWeaponSO().weaponGameObject.Attack();
    }


    private void HandleMovement() {
        rb.MovePosition(rb.position + inputVector * movingSpeed * Time.fixedDeltaTime);

        if (Mathf.Abs(inputVector.x) > minMovingSpeed || Mathf.Abs(inputVector.y) > minMovingSpeed) {
            isRunning = true;
        } else {
            isRunning = false;
        }
    }

    public void HealPlayer(int healthAmount) {
        currentHealth = Math.Min(maxHealth, currentHealth += healthAmount);
        HealthBar.Instance.SetHealth(currentHealth);
    }

    public void AddGoldCoin(int goldCoinAmount) {
        currentGoldCoin += goldCoinAmount;
        GoldColnCounter.Instance.SetGoldCoinAmount(currentGoldCoin);
    }

    public bool IsRunning() {
        return isRunning;
    }

    public bool IsAlive() {
        return isAlive;
    }

    private void Dash() {
        if (!isDashing) {
            StartCoroutine(EndDashRoutine());
        }
    }

    private IEnumerator EndDashRoutine() {
        isDashing = true;
        movingSpeed *= dashSpeed;
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashTime);
        movingSpeed = initialMovingSpeed;
        trailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCoolDownTime);
        isDashing = false;
    }

    public void TakeDamage(Transform damageSourceTransform, int damage) {
        if (canTakeDamage && isAlive) {
            currentHealth = Math.Max(0, currentHealth -= damage);
            HealthBar.Instance.SetHealth(currentHealth);

            ScreenShakeManager.Instance.ShakeScreen();

            if (damageSourceTransform) {
                knockBack.GetKnockedBack(damageSourceTransform);
            }

            OnFlashBlink?.Invoke(this, EventArgs.Empty);
            canTakeDamage = false;
            StartCoroutine(DamageRecoveryRoutine());
        }

        DetectDeath();
    }

    private void DetectDeath() {
        if (currentHealth == 0 && isAlive) {
            isAlive = false;
            GameInput.Instance.DisableMovement();
            knockBack.StopKnockBackMovement();
            OnPlayerDeath?.Invoke(this, EventArgs.Empty);
      
            StartCoroutine(DelayAfterDeathRoutine());
        }
    }


    private IEnumerator DamageRecoveryRoutine() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private IEnumerator DelayAfterDeathRoutine() {
        yield return new WaitForSeconds(delayAfterDeath);
        Loader.InstantLoad(Loader.Scene.GameOverScene);
    }



}
