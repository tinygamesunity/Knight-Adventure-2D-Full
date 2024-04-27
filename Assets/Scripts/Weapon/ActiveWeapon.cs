using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DP.Utils;

public class ActiveWeapon : Singleton<ActiveWeapon> {

    [SerializeField] private WeaponSO[] weaponSOList;

    private WeaponSO currentWeaponSO;
    private bool isFlipped;

    private void Start() {
        isFlipped = false;
        currentWeaponSO = weaponSOList[0];
        SetCurrentWeaponSO(currentWeaponSO.weaponKeyboardKey);
    }
    protected override void Update() {
        base.Update();
        SetActiveWeaponRotation();
    }

    public WeaponSO GetCurrentWeaponSO() {
        return currentWeaponSO;
    }

    private void MouseFollowRotation() {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector2 direction = Camera.main.WorldToScreenPoint(transform.position) - mousePos;
        transform.right = -direction;
    }

    private void MouseFolowingDirection() {
        Vector3 mousePos = GameInput.Instance.GetMousePosition();
        Vector3 playerScreenPoint = Utils.GetGameObjectScreenPoint(Player.Instance.transform);
        //playerScreenPoint = Camera.main.ScreenToWorldPoint(playerScreenPoint);

        float angleRotation = Mathf.Atan2(Mathf.Abs(playerScreenPoint.y - mousePos.y), Mathf.Abs(playerScreenPoint.x - mousePos.x)) * Mathf.Rad2Deg;
        // float angleRotation = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

        if (mousePos.x < playerScreenPoint.x) {
            isFlipped = true;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        } else {
            isFlipped = false;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    private void SetActiveWeaponRotation() {
        if (Player.Instance.IsAlive()) {
            if (currentWeaponSO.weaponSpinsAround) {
                MouseFollowRotation();
            } else {
                MouseFolowingDirection();
            }
        }
    }

    public void SetCurrentWeaponSO(int keyboardKey) {
        // delete a previous weapon
        if (currentWeaponSO != null && currentWeaponSO.weaponGameObject != null) {
            Destroy(currentWeaponSO.weaponGameObject.gameObject);
        }


        for (int i = 0; i < weaponSOList.Length; i++) {
            if (weaponSOList[i].weaponKeyboardKey == keyboardKey) {
                currentWeaponSO = weaponSOList[i];
                
                SetActiveWeaponRotation();

                BaseWeapon currentWeaponPrefab = currentWeaponSO.weaponPrefab;

                // calculate position of the weapon based on ActiveWeapon rotation
                // https://forum.unity.com/threads/calculating-global-position-from-local-rotations.654034/
                var m = Matrix4x4.TRS(Vector3.zero, this.transform.rotation, Vector3.one);
                Vector3 currentWeaponGameObjectPosition = m.MultiplyPoint(currentWeaponPrefab.transform.position);
 
                Vector3 prefabPosition;
                if (isFlipped) {
                    prefabPosition = new Vector3(-currentWeaponPrefab.transform.position.x, currentWeaponPrefab.transform.position.y, currentWeaponPrefab.transform.position.z);
                } else {
                    prefabPosition = currentWeaponPrefab.transform.position;
                }

                BaseWeapon currentWeaponGameObject = Instantiate(currentWeaponPrefab, transform.position + prefabPosition, transform.rotation * currentWeaponPrefab.transform.rotation);

                currentWeaponGameObject.transform.parent = this.transform;
                currentWeaponSO.weaponGameObject = currentWeaponGameObject;
                break;
            }
        }
    }
}
