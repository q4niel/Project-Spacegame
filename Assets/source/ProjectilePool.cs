using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectilePool : MonoBehaviour {
    [SerializeField] Sprite _bulletSprite;
    [SerializeField] Sprite _energyBoltSprite;

    List<GameObject> _container = new List<GameObject>();

    public enum Spritey {
        Bullet,
        EnergyBolt
    }

    Dictionary<Spritey, Sprite> _sprites = new Dictionary<Spritey, Sprite>();

    void Awake() {
        _sprites.Add(Spritey.Bullet, _bulletSprite);
        _sprites.Add(Spritey.EnergyBolt, _energyBoltSprite);
    }

    void Update() {
        Debug.Log($"Num of Projs: {_container.Count}");
    }

    public void spawnBullet(Vector2 pos, Vector2 dir) => _spawn (
        pos,
        dir,
        DamageType.Physical,
        1,
        8,
        Spritey.Bullet
    );

    public void spawnEnergyBolt(Vector2 pos, Vector2 dir) => _spawn (
        pos,
        dir,
        DamageType.Energy,
        1,
        8,
        Spritey.EnergyBolt
    );

    void _spawn (
        Vector2 pos,
        Vector2 dir,
        DamageType dmgType,
        int dmg,
        float speed,
        Spritey spritey
    ) {
        Projectile.Factory factory = new Projectile.Factory (
            _sprites[spritey],
            pos,
            dir,
            dmgType,
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