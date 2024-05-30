using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(AudioSource))]
public class CharacterInfo : MonoBehaviour
{
    [SerializeField] private CharacterStats _stats;

    public CharacterStats BaseStats => _stats;
    public Collider2D Collider { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public AudioSource Audio { get; private set; }

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Audio = GetComponent<AudioSource>();
    }
}
