using UnityEngine;

public class ActiveInventoryUI : MonoBehaviour {

    [SerializeField] private Transform[] inventorySlots;

    private void Start() {
        GameInput.Instance.OnInventoryKeyboard += GameInput_OnInventoryKeyboard;
    }

    private void GameInput_OnInventoryKeyboard(object sender, GameInput.OnInventoryKeyboardEventArgs e) {
        ToggleActiveHighlight(e.pressedKeyboardKey);
    }

    private void ToggleActiveHighlight(int pressedKey) {
        foreach (Transform inventorySlot in this.transform) {
            inventorySlot.GetChild(0).gameObject.SetActive(false);
        }

        this.transform.GetChild(pressedKey - 1).GetChild(0).gameObject.SetActive(true);
    }
}
