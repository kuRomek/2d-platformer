using System;
using UnityEngine;

public class HealthPoints : MonoBehaviour
{
    [SerializeField] private float _max;

    private float _amount;

    public event Action OnValueChange;

    public float Max => _max;
    public float Amount => _amount;

    private void Awake()
    {
        _amount = _max;
    }

    public void TakeDamage(float value)
    {
        _amount = Mathf.Clamp(_amount - value, 0f, _max);
        OnValueChange?.Invoke();
    }

    public void Heal(float value)
    {
        _amount = Mathf.Clamp(_amount + value, 0f, _max);
        OnValueChange?.Invoke();
    }
}
