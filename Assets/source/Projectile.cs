using UnityEngine;

public class Projectile : MonoBehaviour {
    SpriteRenderer _rend;
    BoxCollider2D _coll;

    Vector2 _dir;
    int _dmg;
    float _speed;

    public class Factory {
        public Sprite sprite;
        public Material mat;
        public Vector2 pos;
        public Vector2 dir;
        public int dmg;
        public float speed;
        public Vector2 collSize;
        public Vector2 collOffset;

        public Factory (
            Sprite sprite,
            Material mat,
            Vector2 pos,
            Vector2 dir,
            int dmg,
            float speed,
            Vector2 collSize,
            Vector2 collOffset
        ) {
            this.sprite = sprite;
            this.mat = mat;
            this.pos = pos;
            this.dir = dir;
            this.dmg = dmg;
            this.speed = speed;
            this.collSize = collSize;
            this.collOffset = collOffset;
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
        _rend.material = factory.mat;
        transform.position = factory.pos;
        _dir = factory.dir;
        _dmg = factory.dmg;
        _speed = factory.speed;
        _coll.size = factory.collSize;
        _coll.offset = factory.collOffset;

        float rot = 0.0f;

        switch (_dir.x, _dir.y) {
            case (0.0f, 1.0f): {
                rot = 90.0f;
                break;
            }

            case (0.0f, -1.0f): {
                rot = -90.0f;
                break;
            }

            case (-1.0f, 0.0f): {
                rot = 180.0f;
                break;
            }

            case (1.0f, 0.0f): {
                rot = 0.0f;
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

        float x = transform.position.x;
        float y = transform.position.y;

        if (x > 10
        ||  x < -10
        ||  y > 6
        ||  y < -6
        ) disable();
    }

    public void OnTriggerEnter2D(Collider2D coll) {
        IDamagable iDmg = coll.GetComponent<IDamagable>();
        if (iDmg == null) return;
        iDmg.damage(_dmg);
    }
}