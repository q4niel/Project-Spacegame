using UnityEngine;

public class Projectile : MonoBehaviour {
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] DamageType damageType;
    [SerializeField] Sprite sprite;

    Vector2 _dir;

    public void Awake() {
        disable();

        transform.gameObject.AddComponent<SpriteRenderer>().sprite = sprite;

        Rigidbody2D rb = transform.gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0.0f;
    }

    public void enable(Vector2 pos, Vector2 dir) {
        transform.position = pos;
        _dir = dir;
        float rot = 0.0f;

        switch (dir.x, dir.y) {
            case (0.0f, 1.0f): {
                rot = 0.0f;
                break;
            }

            case (0.0f, -1.0f): {
                rot = 180.0f;
                break;
            }

            case (-1.0f, 0.0f): {
                rot = 90.0f;
                break;
            }

            case (1.0f, 0.0f): {
                rot = -90.0f;
                break;
            }

            default: {
                Debug.LogError($"Invalid direction ({dir}) passed, valid directions are: ({Vector2.up}), ({Vector2.down}), ({Vector2.left}), ({Vector2.right})");
                break;
            }
        }

        transform.Rotate(new Vector3(0.0f, 0.0f, rot));
        transform.gameObject.SetActive(true);
    }

    public void disable() => transform.gameObject.SetActive(false);

    public void Update() {
        Vector2 newPos = transform.position;
        newPos += _dir * speed * Time.deltaTime;
        transform.position = newPos;
    }

    public void OnTriggerEnter2D(Collider2D coll) {
        IDamagable iDmg = coll.GetComponent<IDamagable>();
        if (iDmg == null) return;
        iDmg.damage(damage, damageType);
    }
}