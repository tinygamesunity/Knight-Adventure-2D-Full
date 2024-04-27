using UnityEngine;
using DP.Utils;

[CreateAssetMenu()]
public class PickupSO : ScriptableObject {
    public string pickUpName;
    public Utils.PickUpType pickUpType;
    public GameObject pickupPrefab;
    public float pickupDropProbability;
    public int pickupAmount;
    public int pickupMinDropAmount;
    public int pickupMaxDropAmount;
}
