using System;
using UnityEngine;

public class BaseProjectile : MonoBehaviour {

    public virtual event EventHandler OnMeetObstacle;

    protected void RaiseOnMeetObstacleEvent() {
        OnMeetObstacle?.Invoke(this, EventArgs.Empty);
    }

    protected void DetectFireDistance(ProjectileSO projectileSO, Vector3 startPoint) {
        float projectileRange = projectileSO.projectileRange;
        if (projectileSO.weaponSO != null) {
            projectileRange = projectileSO.weaponSO.weaponProjectileRange;
        }

        if (Vector3.Distance(transform.position, startPoint) > projectileRange) {
            Destroy(gameObject);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        Indestructable indestructable = collision.gameObject.GetComponent<Indestructable>();
        if (indestructable != null && !collision.isTrigger) {
            Destroy(gameObject);
            RaiseOnMeetObstacleEvent();
        }
    }

    protected void MoveProjectile(float projectileMovingSpeed) {
        transform.Translate(Vector3.right * projectileMovingSpeed * Time.fixedDeltaTime);
    }
}
