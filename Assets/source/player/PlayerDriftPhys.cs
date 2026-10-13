using System;
using UnityEngine;

public class PlayerDriftPhys : MonoBehaviour {
    [SerializeField] float driftTime;

    PlayerMovement _move = null;
    Vector2 _prevDir = Vector2.zero;
    float _currentHDriftSpeed;
    float _currentVDriftSpeed;
    float _hDriftPos = .0f;
    float _vDriftPos = .0f;

    void Start() {
        _move = gameObject.GetComponent<PlayerMovement>();

        if (_move == null)
            Debug.LogWarning("PlayerDriftPhys could not find component PlayerMovement");

        _currentHDriftSpeed = _move.getSpeed();
        _currentVDriftSpeed = _move.getSpeed();
    }

    void Update() {
        Vector2 moveDir = _move.getDirection();

        if (moveDir.x != 0) {
            _prevDir.x = moveDir.x;
        }
        else _drift (
            ref _hDriftPos,
            transform.position.x,
            ref _prevDir.x,
            ref _currentHDriftSpeed

        );

        if (moveDir.y != 0) {
            _prevDir.y = moveDir.y;
        }
        // else _drift();
    }

    void _drift (
        ref float driftPos,
        float transPos,
        ref float driftDir,
        ref float currDriftSpeed
    ) {
        driftPos = transPos;
        driftPos += driftDir * currDriftSpeed * Time.deltaTime;

        currDriftSpeed = Math.Clamp (
            currDriftSpeed - Time.deltaTime * driftTime,
            0,
            _move.getSpeed()
        );
    }
}