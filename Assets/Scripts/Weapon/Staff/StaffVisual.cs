using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaffVisual : MonoBehaviour {

    [SerializeField] private Staff staff;

    private const string ATTACK = "Attack";

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
        staff.OnStaffAttack += Staff_OnStaffAttack;
    }

    private void Staff_OnStaffAttack(object sender, System.EventArgs e) {
        animator.SetTrigger(ATTACK);
    }

}
