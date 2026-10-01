using UnityEngine;

public class TargetDummy : MonoBehaviour, IDamagable {
    [SerializeField] int health;
    [SerializeField] int shield;

    public void damage (
        int value,
        DamageType type
    ) {
        switch (type) {
            case (DamageType.Physical): {
                if (shield <= 0)
                    health -= value;
                break;
            }

            case (DamageType.Energy): {
                shield -= value;
                break;
            }
        }

        if (health > 0) return;
        kill();
    }

    public void heal(int value, DamageType type) {
        switch (type) {
            case (DamageType.Physical): {
                health += value;
                break;
            }

            case (DamageType.Energy): {
                shield += value;
                break;
            }
        }
    }

    public void kill() {
        Destroy(gameObject);
    }

    public void Update() {
        Debug.Log($"Target Dummy: (shield={shield}) (health={health})");
    }
}