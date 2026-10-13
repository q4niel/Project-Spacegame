using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float _maxVelocity;
    [SerializeField] float _velocityDecreaseDuration;
    [SerializeField] float _boostVelocityMultiplier;
    [SerializeField] Animator _thrusterAnimator;

    Vector2 _prevDir = Vector2.zero;

    float _xVel = .0f;
    float _xVelElapsed = .0f;

    float _yVel = .0f;
    float _yVelElapsed = .0f;

    bool _isBoosting = false;

    public void boost(bool value) => _isBoosting = value;

    public void move(Vector2 dir) {
        Vector2 newPos = transform.position;

        if (dir.x != 0) {
            _prevDir.x = dir.x;
            _xVel = _maxVelocity;
            _xVelElapsed = .0f;

            if (_isBoosting) _thrusterAnimator.SetInteger (
                "boost_state",
                dir.x > 0 ? 1 : 2
            );
            else _thrusterAnimator.SetInteger("boost_state", 0);
        }
        else {
            _decreaseVel(ref _xVel, ref _xVelElapsed);
            _thrusterAnimator.SetInteger("boost_state", 0);
        }

        if (dir.y != 0) {
            _prevDir.y = dir.y;
            _yVel = _maxVelocity;
            _yVelElapsed = .0f;
        }
        else _decreaseVel(ref _yVel, ref _yVelElapsed);

        newPos.x += _prevDir.x * _xVel * (_isBoosting ? _boostVelocityMultiplier : 1) * Time.deltaTime;
        newPos.y += _prevDir.y * _yVel * Time.deltaTime;

        transform.position = newPos;
    }

    void _decreaseVel(ref float vel, ref float elapsed) {
        elapsed += Time.deltaTime;
        vel = Mathf.Lerp (
            _maxVelocity,
            .0f,
            _easeOutQuart(elapsed / _velocityDecreaseDuration)
        );
    }

    float _easeOutQuart(float x)
        => 1.0f - Mathf.Pow(1.0f - Math.Clamp(x, .0f, 1.0f), 4.0f);
}