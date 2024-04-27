using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Singleton<CameraController> {

    private CinemachineVirtualCamera cinemachineVirtualCamera;

    public void SetPlayerCameraFollow() {
        cinemachineVirtualCamera = FindAnyObjectByType<CinemachineVirtualCamera>();
        if (cinemachineVirtualCamera != null) {
            cinemachineVirtualCamera.Follow = Player.Instance.transform;
        }
    }
}
