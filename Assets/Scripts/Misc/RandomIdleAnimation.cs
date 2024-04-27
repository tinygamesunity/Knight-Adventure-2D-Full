using UnityEngine;

public class RandomIdleAnimation : MonoBehaviour {

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animator.Play(animatorStateInfo.fullPathHash, -1, Random.Range(0f, 1f));
    }

}
