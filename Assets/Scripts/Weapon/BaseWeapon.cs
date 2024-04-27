using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour {
    public virtual void Attack() {
        Debug.LogError("BaseWeapon.Attack();");
    }
}
