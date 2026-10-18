using UnityEngine;

[CreateAssetMenu (
    fileName = "VeylithFighterData",
    menuName = "Scriptable Objects/Veylith Fighter Data"
)]
public class VeylithFighterData : ScriptableObject {
    [Header("General")]
    [SerializeField] public int health;
    [SerializeField] public float attackTime;

    [Header("Movement")]
    [SerializeField] public Vector2 velocity;
    [SerializeField] public float horizontalVelocityRepositionMultiplier;
    [SerializeField] public float targetHorizontalDistance;
    [SerializeField] public float verticalBounceLimit;
}