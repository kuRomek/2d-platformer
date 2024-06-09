using UnityEngine;

[RequireComponent(typeof(HealthPoints))]
public class CharacterStats : MonoBehaviour
{
    [SerializeField] private HealthPoints _healthPoints;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    public HealthPoints HealthPoints => _healthPoints;
    public float Speed => _speed;
    public float JumpForce => _jumpForce;

}