using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class WeaponSO : ScriptableObject
{
    public BaseWeapon weaponPrefab;
    public BaseWeapon weaponGameObject;
    public int weaponDamageAmout;
    public float weaponProjectileRange;
    public int weaponKeyboardKey;
    public bool weaponSpinsAround;
}
