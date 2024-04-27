using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour {

    [SerializeField] private float knockBackMovingTimerMax = 0.5f;
    [SerializeField] private float knockBackForce = 3f;

    public bool GettingKnockedBack { get; private set; }

    Rigidbody2D rb;

    private float knockBackMovingTimer;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        knockBackMovingTimer -= Time.deltaTime;
        if (knockBackMovingTimer < 0) {
            StopKnockBackMovement();
        }
    }

    public void GetKnockedBack(Transform damageSource) {
        GettingKnockedBack = true;
        knockBackMovingTimer = knockBackMovingTimerMax;
        Vector2 difference = (transform.position - damageSource.position).normalized * knockBackForce * rb.mass;
        rb.AddForce(difference, ForceMode2D.Impulse);
    }

    public void StopKnockBackMovement() {
        rb.velocity = Vector2.zero;
        GettingKnockedBack = false;
    }
}
