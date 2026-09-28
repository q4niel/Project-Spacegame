using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    [SerializeField] float speed;

    Vector2 _dir = Vector2.zero;
    public Vector2 getDirection() => _dir;
    public void setDirection(Vector2 dir) => _dir = dir;

    public void Update() {
        if (_dir == Vector2.zero) return;

        Vector2 newPos = transform.position;
        newPos += _dir * speed * Time.deltaTime;
        transform.position = newPos;

        _dir = Vector2.zero;
    }
}