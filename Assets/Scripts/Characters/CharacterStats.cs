using UnityEngine;

[RequireComponent(typeof(HealthPoints))]
public class CharacterStats : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;

    public HealthPoints HealthPoints { get; private set; }
    public float Speed => _speed;
    public float JumpForce => _jumpForce;

    private void Awake()
    {
        HealthPoints = GetComponent<HealthPoints>();
    }
}