using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectilePool : MonoBehaviour {
    [Header("Laser")]
    [SerializeField] int _laserDamage;
    [SerializeField] float _laserSpeed;
    [SerializeField] Sprite _laserSprite;
    [SerializeField] Material _laserDarkVioletMaterial;

    List<GameObject> _container = new List<GameObject>();

    public void spawnDarkVioletLaser(Vector2 pos, Vector2 dir) => _spawn (
        pos,
        dir,
        _laserDamage,
        _laserSpeed,
        _laserSprite,
        _laserDarkVioletMaterial
    );

    void _spawn (
        Vector2 pos,
        Vector2 dir,
        int dmg,
        float speed,
        Sprite sprite,
        Material mat
    ) {
        Projectile.Factory factory = new Projectile.Factory (
            sprite,
            mat,
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