using UnityEngine;

public class SelfDestroyVFX : MonoBehaviour {

    private ParticleSystem ps;

    private void Awake() {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update() {
        if (ps && !ps.IsAlive()) {
            DestroySelf();
        }
    }

    private void DestroySelf() {
        Destroy(gameObject);
    }
}
