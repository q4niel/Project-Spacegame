using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectilePool : MonoBehaviour {
    [Header("Laser")]
    [SerializeField] int _laserDamage;
    [SerializeField] float _laserSpeed;
    [SerializeField] Vector2 _laserColliderSize;
    [SerializeField] Vector2 _laserColliderOffset;
    [SerializeField] Sprite _laserSprite;
    [SerializeField] Material _laserDarkVioletMaterial;
    [SerializeField] Material _laserLimeMaterial;

    List<GameObject> _container = new List<GameObject>();

    public void spawnLaserDarkViolet(Vector2 pos, Vector2 dir)
        => spawnLaser(pos, dir, _laserDarkVioletMaterial);

    public void spawnLaserLime(Vector2 pos, Vector2 dir)
        => spawnLaser(pos, dir, _laserLimeMaterial);

    public void spawnLaser (
        Vector2 pos,
        Vector2 dir,
        Material mat
    ) => _spawn(new Projectile.Factory (
        _laserSprite,
        mat,
        pos,
        dir,
        _laserDamage,
        _laserSpeed,
        _laserColliderSize,
        _laserColliderOffset
    ));

    void _spawn(Projectile.Factory factory) {
        foreach (GameObject proj in _container) {
            if (proj.activeInHierarchy) continue;

            proj.GetComponent<Projectile>().enable(factory);
            return;
        }

        _container.Add(new GameObject());
        _container.Last().AddComponent<Projectile>().enable(factory);
    }
}