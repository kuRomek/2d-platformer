using UnityEngine;

public class EnemyStats : CharacterStats
{
    [SerializeField] private float _attackDamage;
    [SerializeField] private float _tickDamage;

    public float AttackDamage => _attackDamage;
    public float TickDamage => _tickDamage;
    public float AttackDuration { get; private set; } = 0.8f;
    public float AttackCooldown { get; private set; } = 2f;
}
