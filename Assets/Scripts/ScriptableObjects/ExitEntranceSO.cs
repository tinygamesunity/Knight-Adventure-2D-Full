using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ExitEntranceSO : ScriptableObject {

    public int sceneInd;
    public string sceneName;
    public string sceneExitName;
    public int sceneLeadToInd;
    public string sceneLeadToName;
    public string sceneLeadToExitName;

}
