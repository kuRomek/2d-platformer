using System;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    [SerializeField] private Attack _attack1;
    [SerializeField] private Attack _attack2;
    [SerializeField] private int _coinCount;
    [SerializeField] private int _gemCount;

    public int CoinCount => _coinCount;
    public int GemCount => _gemCount;
    public Attack Attack1 => _attack1;
    public Attack Attack2 => _attack2;

    public event Action OnItemsCountUpdate;

    [Serializable]
    public struct Attack
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

    public void GetItem(Collectable item, int amount)
    {
        switch (item)
        {
            case Gem:
                _gemCount += amount;
                break;

            case Coin:
                _coinCount += amount;
                break;
        }

        OnItemsCountUpdate?.Invoke();
    }
}