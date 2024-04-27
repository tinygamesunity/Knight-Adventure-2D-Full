using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ProjectileSO : ScriptableObject {

    public WeaponSO weaponSO;
    public GameObject projectilePrefab;
    public GameObject projectileGameObject;
    public float projectileMovingSpeed;
    public float projectileRange;
    public int projectileDamageAmout;

}
