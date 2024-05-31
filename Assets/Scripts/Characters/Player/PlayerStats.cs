using System;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    [SerializeField] private PlayerAttack _attack1;
    [SerializeField] private PlayerAttack attack2;

    public PlayerAttack Attack1 => _attack1;
    public PlayerAttack Attack2 => attack2;
}

[Serializable]
public struct PlayerAttack
{
    public Tier _type;
    public float _damage;
    public float _duration;

    public enum Tier
    {
        Light,
        Heavy,
    }

    public readonly Tier Type => _type;
    public readonly float Damage => _damage;
    public readonly float Duration => _duration;
}