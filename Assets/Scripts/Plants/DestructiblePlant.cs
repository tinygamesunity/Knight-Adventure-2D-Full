using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructiblePlant : MonoBehaviour {

    [SerializeField] private bool isDropItems = false;

    private PickupSpawner pickupSpawner;

    private void Awake() {
        pickupSpawner = GetComponent<PickupSpawner>();
    }

    public event EventHandler OnDestructibleTakeDamage;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.GetComponent<BaseWeapon>() || collision.gameObject.GetComponent<BaseProjectile>()) {
            if (isDropItems) {
                pickupSpawner.DropItems();
            }
            OnDestructibleTakeDamage?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);

            NavMeshSurfaceManagement.Instance.RebakeNavmeshSurface();
        }
    }

}
