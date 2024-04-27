using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManagement : Singleton<MySceneManagement> {

    public string SceneTransitionName { get; private set; }

    public void SetSceneTransitonName(string sceneTransitionName) { 
        this.SceneTransitionName = sceneTransitionName;
    }

}
