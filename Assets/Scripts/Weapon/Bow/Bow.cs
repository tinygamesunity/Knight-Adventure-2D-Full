using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : BaseWeapon {

    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private ProjectileSO projectileSO;
    [SerializeField] private Transform projectileSpawnPoint;

    public event EventHandler OnBowAttack;
   
    public override void Attack() {
        GameObject arrowGameObject = Instantiate(projectileSO.projectilePrefab, projectileSpawnPoint.position, this.transform.rotation);
        OnBowAttack?.Invoke(this, EventArgs.Empty);
    }

}
