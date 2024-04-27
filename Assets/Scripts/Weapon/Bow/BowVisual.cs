using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowVisual : MonoBehaviour {
    
    [SerializeField] private Bow bow;

    private string ATTACK = "Attack";
    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        bow.OnBowAttack += Bow_OnBowAttack;
    }

    private void Bow_OnBowAttack(object sender, System.EventArgs e) {
        animator.SetTrigger(ATTACK);
    }
}
