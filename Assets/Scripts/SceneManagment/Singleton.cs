using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class Singleton<T> : MonoBehaviour where T : Singleton<T> {

    private static T instance;
    public static T Instance { get { return instance; } }

    private bool isDestroy;

    protected virtual void Awake() {
        isDestroy = false;
        if (this.gameObject != null && instance != null) {
            Destroy(this.gameObject);
        } else {
            instance = (T)this;
        }

        if (!gameObject.transform.parent && !isDestroy) {
            DontDestroyOnLoad(gameObject);
        }
    }

    protected virtual void Update() {
        if (ObjectDestroyer.Instance != null) {
            ObjectDestroyer.Instance.OnDestroyObjects += ObjectDestroyer_OnDestroyObjects;
        } 
    }

    private void ObjectDestroyer_OnDestroyObjects(object sender, System.EventArgs e) {
        Destroy(this.gameObject);
    }
}
