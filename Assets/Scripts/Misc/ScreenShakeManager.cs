using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ScreenShakeManager : Singleton<ScreenShakeManager> {

    private CinemachineImpulseSource impulseSource;

    protected override void Awake() {
        base.Awake();

        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void ShakeScreen() {
        impulseSource.GenerateImpulse();
    }

}
