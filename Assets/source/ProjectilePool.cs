using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectilePool : MonoBehaviour {
    [SerializeField] GameObject bullet;
    List<GameObject> _bullets = new List<GameObject>();

    [SerializeField] GameObject energyBolt;
    List<GameObject> _energyBolts = new List<GameObject>();

    public void spawnBullet(Vector2 pos, Vector2 dir)
        => _spawn(pos, dir, bullet, ref _bullets);

    public void spawnEnergyBolt(Vector2 pos, Vector2 dir)
        => _spawn(pos, dir, energyBolt, ref _energyBolts);

    void _spawn(Vector2 pos, Vector2 dir, GameObject gameObject, ref List<GameObject> container) {
        container.Add(Instantiate(gameObject));
        container.Last().GetComponent<Projectile>().enable(pos, dir);
    }
}