using UnityEngine;

public class TargetDummy : MonoBehaviour, IDamagable {
    [SerializeField] int health;

    public void damage(int value) {
        health -= value;
        if (health > 0) return;
        kill();
    }

    public void heal(int value) {
        health += value;
    }

    public void kill() {
        Destroy(gameObject);
    }
}