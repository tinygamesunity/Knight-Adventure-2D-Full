using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour {

    public static ObjectDestroyer Instance { get; private set; }

    public event EventHandler OnDestroyObjects;
    private bool isFirstUpdate = true;

    private void Awake() {
        Instance = this;
    }

    private void Update() {
        if (isFirstUpdate) {
            isFirstUpdate = false;
            OnDestroyObjects?.Invoke(this, EventArgs.Empty);
        }
    }

}
