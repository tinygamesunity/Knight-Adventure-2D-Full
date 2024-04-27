using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileVisual : MonoBehaviour
{
    [SerializeField] private BaseProjectile projectile;
    [SerializeField] private GameObject projectileDestroyVFX;

    private void Start() {
        projectile.OnMeetObstacle += Projectile_OnMeetObstacle;
    }

    private void Projectile_OnMeetObstacle(object sender, System.EventArgs e) {
        Instantiate(projectileDestroyVFX, transform.position, transform.rotation);
    }
}
