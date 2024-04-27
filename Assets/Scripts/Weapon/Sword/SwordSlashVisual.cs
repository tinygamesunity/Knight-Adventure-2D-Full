using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSlashVisual : MonoBehaviour {

    [SerializeField] private Sword sword;

    private string ATTACK = "Attack";
    private Animator animator;


    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        sword.OnSwingDown += Sword_OnSwingDown;
    }

    private void Sword_OnSwingDown(object sender, System.EventArgs e) {
        animator.SetTrigger(ATTACK);
    }

    public void TriggerEndSwingAnimation() {
        sword.SwordColliderTurnOff();
    }
}
