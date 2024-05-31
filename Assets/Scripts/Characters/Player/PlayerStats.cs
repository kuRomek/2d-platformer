using UnityEngine;

public class PlayerStats : CharacterStats
{
    [SerializeField] private float _lightAttackDamage;
    [SerializeField] private float _heavyAttackDamage;
    private float _lightAttackDuration = 0.429f;
    private float _heavyAttackDuration = 0.571f;

    public enum AttackType
    {
        Light,
        Heavy
    }

    public float GetAttackDamage(AttackType type)
    {
        if (type == AttackType.Light)
            return _lightAttackDamage;
        else
            return _heavyAttackDamage;
    }

    public float GetAttackDuration(AttackType type)
    {
        if (type == AttackType.Light)
            return _lightAttackDuration;
        else
            return _heavyAttackDuration;
    }
}
