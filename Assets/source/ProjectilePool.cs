using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectilePool : MonoBehaviour {
    [SerializeField] Sprite _bulletSprite;

    List<GameObject> _container = new List<GameObject>();

    public enum Spritey {
        Bullet
    }

    Dictionary<Spritey, Sprite> _sprites = new Dictionary<Spritey, Sprite>();

    void Awake() {
        _sprites.Add(Spritey.Bullet, _bulletSprite);
    }

    public void spawnBullet(Vector2 pos, Vector2 dir) => _spawn (
        pos,
        dir,
        1,
        8,
        Spritey.Bullet
    );

    void _spawn (
        Vector2 pos,
        Vector2 dir,
        int dmg,
        float speed,
        Spritey spritey
    ) {
        Projectile.Factory factory = new Projectile.Factory (
            _sprites[spritey],
            pos,
            dir,
            dmg,
            speed
        );

        foreach (GameObject proj in _container) {
            if (proj.activeInHierarchy) continue;

            proj.GetComponent<Projectile>().enable(factory);
            return;
        }

        _container.Add(new GameObject());
        _container.Last().AddComponent<Projectile>().enable(factory);
    }
}