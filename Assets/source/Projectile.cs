using UnityEngine;

public class Projectile : MonoBehaviour {
    SpriteRenderer _rend;
    BoxCollider2D _coll;

    Vector2 _dir;
    DamageType _dmgType;
    int _dmg;
    float _speed;

    public class Factory {
        public Sprite sprite;
        public Vector2 pos;
        public Vector2 dir;
        public DamageType dmgType;
        public int dmg;
        public float speed;

        public Factory (
            Sprite sprite,
            Vector2 pos,
            Vector2 dir,
            DamageType dmgType,
            int dmg,
            float speed
        ) {
            this.sprite = sprite;
            this.pos = pos;
            this.dir = dir;
            this.dmgType = dmgType;
            this.dmg = dmg;
            this.speed = speed;
        }
    }

    public void Awake() {
        disable();

        Rigidbody2D rb = transform.gameObject.AddComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0.0f;

        _rend = transform.gameObject.AddComponent<SpriteRenderer>();
        _coll = transform.gameObject.AddComponent<BoxCollider2D>();
    }

    public void enable(Factory factory) {
        _rend.sprite = factory.sprite;
        _coll.size = factory.sprite.bounds.size;
        transform.position = factory.pos;
        _dir = factory.dir;
        _dmgType = factory.dmgType;
        _dmg = factory.dmg;
        _speed = factory.speed;

        float rot = 0.0f;

        switch (_dir.x, _dir.y) {
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
                Debug.LogError($"Invalid direction ({_dir}) passed, valid directions are: ({Vector2.up}), ({Vector2.down}), ({Vector2.left}), ({Vector2.right})");
                break;
            }
        }

        transform.eulerAngles = new Vector3(0.0f, 0.0f, rot);
        transform.gameObject.SetActive(true);
    }

    public void disable() => transform.gameObject.SetActive(false);

    public void Update() {
        Vector2 newPos = transform.position;
        newPos += _dir * _speed * Time.deltaTime;
        transform.position = newPos;

        Vector2 screenPos = Camera.main.WorldToScreenPoint(transform.position);

        if (screenPos.x > Screen.currentResolution.width
        ||  screenPos.x < 0
        ||  screenPos.y > Screen.currentResolution.height
        || screenPos.y < 0
        ) {
            disable();
        }
    }

    public void OnTriggerEnter2D(Collider2D coll) {
        IDamagable iDmg = coll.GetComponent<IDamagable>();
        if (iDmg == null) return;
        iDmg.damage(_dmg, _dmgType);
    }
}