using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    [SerializeField, Min(1f)] private float _maxHealthPoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    private float _healthPoints;

    public float MaxHealthPoints => _maxHealthPoints;
    public float HealthPoints => _healthPoints;
    public float Speed => _speed;
    public float JumpForce => _jumpForce;

    private void Awake()
    {
        _healthPoints = _maxHealthPoints;
    }

    public void AddHP(float hpToAdd)
    {
        _healthPoints = Mathf.Clamp(_healthPoints + hpToAdd, 0f, _maxHealthPoints);
        Debug.Log($"{_healthPoints} (+{hpToAdd})");
    }

    public void SubtractHP(float hpToSubtract)
    {
        _healthPoints = Mathf.Clamp(_healthPoints - hpToSubtract, 0f, _maxHealthPoints);
        Debug.Log($"{_healthPoints} (-{hpToSubtract})");
    }
}