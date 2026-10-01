using UnityEngine;

interface IDamagable {
    void damage(int value, DamageType type);
    void heal(int value, DamageType type);
    void kill();
}