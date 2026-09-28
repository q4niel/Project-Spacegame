using UnityEngine;

public class PlayerInput : MonoBehaviour {
    PlayerControls controls;
    PlayerMovement playerMovement;

    public void Awake() {
        controls = new PlayerControls();

        if (!TryGetComponent<PlayerMovement>(out playerMovement))
            Debug.LogError("Player has no component of type 'PlayerMovement'");
    }

    public void OnEnable() => controls.Enable();
    public void OnDisable() => controls.Disable();

    public void Update() {
        playerMovement.setDirection(controls.game.move.ReadValue<Vector2>());
    }
}