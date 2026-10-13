using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [Header("General")]
    [SerializeField] float _maxVelocity;
    [SerializeField] float _velocityDecreaseDuration;

    [Header("Boost")]
    [SerializeField] float _boostBuildupTime;
    [SerializeField] float _boostVelocityMultiplier;

    [Header("Fall")]
    [SerializeField] float _fallBuildupTime;
    [SerializeField] float _fallVelocityMultiplier;

    [Header("References")]
    [SerializeField] Animator _thrusterAnimator;

    Vector2 _prevDir = Vector2.zero;

    float _xVel = .0f;
    float _xVelElapsed = .0f;

    float _yVel = .0f;
    float _yVelElapsed = .0f;

    enum XVelocityState {
        Fall,
        Default,
        Boost
    }
    float _xVelocityStateBuildup = .0f;
    float _xVelocityStateMultiplier;

    public void move(Vector2 dir) {
        Vector2 newPos = transform.position;

        if (dir.x != 0) {
            _prevDir.x = dir.x;
            _xVel = _maxVelocity;
            _xVelElapsed = .0f;

            if (dir.x > 0) _setXVelocityState(XVelocityState.Boost);
            else _setXVelocityState(XVelocityState.Fall);
        }
        else {
            _decreaseVel(ref _xVel, ref _xVelElapsed);
            _setXVelocityState(XVelocityState.Default);
        }

        if (dir.y != 0) {
            _prevDir.y = dir.y;
            _yVel = _maxVelocity;
            _yVelElapsed = .0f;
        }
        else _decreaseVel(ref _yVel, ref _yVelElapsed);

        newPos.x += _prevDir.x * _xVel * _xVelocityStateMultiplier * Time.deltaTime;
        newPos.y += _prevDir.y * _yVel * Time.deltaTime;

        transform.position = newPos;
    }

    void _setXVelocityState(XVelocityState state) {
        switch (state) {
            case (XVelocityState.Fall): {
                _xVelocityStateBuildup += Time.deltaTime;

                if (_xVelocityStateBuildup >= _fallBuildupTime) {
                    _thrusterAnimator.SetBool("fall", true);
                    _xVelocityStateMultiplier = _fallVelocityMultiplier;
                }
                break;
            }

            case (XVelocityState.Default): {
                _xVelocityStateBuildup = .0f;
                _xVelocityStateMultiplier = 1.0f;
                _thrusterAnimator.SetBool("fall", false);
                _thrusterAnimator.SetBool("boost", false);
                break;
            }

            case (XVelocityState.Boost): {
                _xVelocityStateBuildup += Time.deltaTime;

                if (_xVelocityStateBuildup >= _boostBuildupTime) {
                    _thrusterAnimator.SetBool("boost", true);
                    _xVelocityStateMultiplier = _boostVelocityMultiplier;
                }
                break;
            }
        }
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