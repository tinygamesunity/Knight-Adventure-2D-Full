using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class GameInput : Singleton<GameInput> {

    PlayerInputActions playerInputActions;

    public event EventHandler OnPlayerAttack;
    public event EventHandler OnPlayerDash;
    public event EventHandler<OnInventoryKeyboardEventArgs> OnInventoryKeyboard;
    public class OnInventoryKeyboardEventArgs : EventArgs {
        public int pressedKeyboardKey;
    }

    protected override void Awake() {
        base.Awake();

        playerInputActions = new PlayerInputActions();
        playerInputActions.Enable();

        playerInputActions.Combat.Attack.started += PlayerAttack_performed;
        playerInputActions.Player.Dash.performed += PlayerDash_performed;

        playerInputActions.Inventory.Keyboard.performed += InventoryKeyboard_performed;
    }

    private void InventoryKeyboard_performed(InputAction.CallbackContext obj) {
        OnInventoryKeyboard?.Invoke(this, new OnInventoryKeyboardEventArgs {
            pressedKeyboardKey = (int)obj.ReadValue<float>()
        });
    }

    private void PlayerDash_performed(InputAction.CallbackContext obj) {
        OnPlayerDash?.Invoke(this, EventArgs.Empty);
    }

    private void PlayerAttack_performed(InputAction.CallbackContext obj) {
        OnPlayerAttack?.Invoke(this, EventArgs.Empty);
    }

    private void OnDestroy() {
        playerInputActions.Dispose();
    }

    public Vector2 GetMovementVector() {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }

    public Vector3 GetMousePosition() {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        return mousePos;
    }

    public void DisableMovement() {
        playerInputActions.Disable(); 
    }

}
