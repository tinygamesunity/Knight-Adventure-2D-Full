using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staff : BaseWeapon {

    [SerializeField] private WeaponSO weaponSO;
    [SerializeField] private ProjectileSO projectileSO;
    [SerializeField] private Transform projectileSpawnPoint;

    public event EventHandler OnStaffAttack;
   
    public override void Attack() {
        GameObject magicLaserGameObject = Instantiate(projectileSO.projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        OnStaffAttack?.Invoke(this, EventArgs.Empty);
    }

}
