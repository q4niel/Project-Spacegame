using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float _maxVelocity;
    [SerializeField] float _velocityDecreaseDuration;

    Vector2 _prevDir = Vector2.zero;

    float _xVel = .0f;
    float _xVelElapsed = .0f;

    float _yVel = .0f;
    float _yVelElapsed = .0f;

    public void move(Vector2 dir) {
        Vector2 newPos = transform.position;

        if (dir.x != 0) {
            _prevDir.x = dir.x;
            _xVel = _maxVelocity;
            _xVelElapsed = .0f;
        }
        else decreaseVel(ref _xVel, ref _xVelElapsed);

        if (dir.y != 0) {
            _prevDir.y = dir.y;
            _yVel = _maxVelocity;
            _yVelElapsed = .0f;
        }
        else decreaseVel(ref _yVel, ref _yVelElapsed);

        newPos.x += _prevDir.x * _xVel * Time.deltaTime;
        newPos.y += _prevDir.y * _yVel * Time.deltaTime;

        transform.position = newPos;
    }

    void decreaseVel(ref float vel, ref float elapsed) {
        elapsed += Time.deltaTime;
        vel = Mathf.Lerp (
            _maxVelocity,
            .0f,
            easeOutQuart(elapsed / _velocityDecreaseDuration)
        );
    }

    float easeOutQuart(float x)
        => 1.0f - Mathf.Pow(1.0f - Math.Clamp(x, .0f, 1.0f), 4.0f);
}