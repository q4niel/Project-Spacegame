using UnityEngine;

public class PlayerInput : MonoBehaviour {
    PlayerControls controls;
    PlayerMovement movement;
    ProjectilePool projPool;

    public void Awake() {
        controls = new PlayerControls();
    }

    public void Start() {
        if (!TryGetComponent<PlayerMovement>(out movement))
            Debug.LogError("Player has no component of type 'PlayerMovement'");
        else {
            controls.game.boost.performed += ctx => movement.boost(true);
            controls.game.boost.canceled += ctx => movement.boost(false);
        }

        projPool = FindAnyObjectByType<ProjectilePool>();
        if (projPool == null)
            Debug.LogError("Player could not find global object of type 'ProjectilePool'");
    }

    public void OnEnable() => controls.Enable();
    public void OnDisable() => controls.Disable();

    public void Update() {
        movement.move(controls.game.move.ReadValue<Vector2>());

        if (controls.game.bullet.triggered)
            projPool.spawnLaserDarkViolet(transform.position, Vector2.right);
    }
}