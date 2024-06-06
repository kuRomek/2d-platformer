using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(AudioSource))]
[RequireComponent(typeof(SpriteRenderer))]
public class CharacterInfo<StatsType> : MonoBehaviour where StatsType : CharacterStats
{
    [SerializeField] private StatsType _stats;

    public StatsType Stats => _stats;
    public Collider2D Collider { get; private set; }
    public Rigidbody2D Rigidbody { get; private set; }
    public AudioSource Audio { get; private set; }
    public SpriteRenderer Sprite { get; private set; }

    private void Awake()
    {
        Collider = GetComponent<Collider2D>();
        Rigidbody = GetComponent<Rigidbody2D>();
        Audio = GetComponent<AudioSource>();
        Sprite = GetComponent<SpriteRenderer>();
    }
}
