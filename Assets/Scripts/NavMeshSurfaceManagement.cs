using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshSurfaceManagement : MonoBehaviour {

    public static NavMeshSurfaceManagement Instance { get; private set; }

    private NavMeshSurface navmeshSurface;

    private void Awake() {
        Instance = this;
        navmeshSurface = GetComponent<NavMeshSurface>();
        navmeshSurface.hideEditorLogs = true;
    }

    public void RebakeNavmeshSurface() {
        navmeshSurface.BuildNavMesh();
    }


}
