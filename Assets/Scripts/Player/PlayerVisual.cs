using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DP.Utils;

public class PlayerVisual : MonoBehaviour {
    private const string IS_RUNNING = "IsRunning";
    private const string IS_DIE = "IsDie";

    private Animator animator;
    private FlashBlink flashBlink;
    private SpriteRenderer spriteRenderer;

    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashBlink = GetComponent<FlashBlink>();
    }

    private void Start() {
        Player.Instance.OnPlayerDeath += Player_OnPlayerDeath;
    }

    private void Player_OnPlayerDeath(object sender, System.EventArgs e) {
        animator.SetBool(IS_DIE, true);
        flashBlink.StopBlinking();
    }

    private void FixedUpdate() {
        animator.SetBool(IS_RUNNING, Player.Instance.IsRunning());

        if (Player.Instance.IsAlive()) {
            AdjustPlayerFacingDirection();
        }
    }

    private void AdjustPlayerFacingDirection() {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerScreenPoint = Utils.GetGameObjectScreenPoint(Player.Instance.transform);

        if (mousePos.x < playerScreenPoint.x) {
            spriteRenderer.flipX = true;
        } else {
            spriteRenderer.flipX = false;
        }
    }
}
