using UnityEngine;

public class PickupSpawner : MonoBehaviour {

    [SerializeField] private PickupSO[] pickupSOList;

    public void DropItems() {
        for (int i = 0; i < pickupSOList.Length; i++) {
            PickupSO pickupSO = pickupSOList[i];
            if (isDropItem(pickupSO.pickupDropProbability)) {
                int randomDropAmount = Random.Range(pickupSO.pickupMinDropAmount, pickupSO.pickupMaxDropAmount);
                for (int j = 0; j < randomDropAmount; j++) {
                    Instantiate(pickupSO.pickupPrefab, transform.position, Quaternion.identity);
                }
            }
        }
    }


    private bool isDropItem(float dropProbability) {
        return (Random.value < dropProbability);
    }

}
