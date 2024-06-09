using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterInfo))]
public class Attackable<StatsType> : MonoBehaviour where StatsType : CharacterStats
{

    [SerializeField] private AudioClip[] _hitSounds;

    private bool _isDead = false;
    private float _defaultStanedSeconds = 0.4f;
    private float _dyingSeconds = 1f;

    public bool IsStaned { get; set; }
    public bool IsDead => _isDead;
    protected CharacterInfo<StatsType> Character { get; private set; }

    private void Awake()
    {
        Character = GetComponent<CharacterInfo<StatsType>>();
    }

    private void OnEnable()
    {
        Character.Stats.HealthPoints.OnValueChange += BeginDying;
    }

    private void OnDisable()
    {
        Character.Stats.HealthPoints.OnValueChange -= BeginDying;
    }

    public virtual void TakeDamage(float damage)
    {
        Character.Stats.HealthPoints.TakeDamage(damage);
        Character.Audio.PlayOneShot(_hitSounds[Random.Range(0, _hitSounds.Length)]);
    }

    public virtual void TakeDamageForce(Vector2 force)
    {
        Character.Rigidbody.velocity = Vector2.zero;
        Character.Rigidbody.AddForce(force);
    }

    public void BeginDying()
    {
        if (Character.Stats.HealthPoints.Amount == 0f)
            StartCoroutine(Die());
    }

    protected virtual IEnumerator Die()
    {
        _isDead = true;

        yield return new WaitForSeconds(_dyingSeconds);

        Character.Collider.enabled = false;
        Character.Rigidbody.velocity = Vector2.zero;
        Character.Rigidbody.isKinematic = true;
    }

    public IEnumerator StanForFixedTime()
    {
        Stan();

        yield return new WaitForSeconds(_defaultStanedSeconds);
        
        Unstan();
    }

    public virtual void Stan()
    {
        IsStaned = true;
    }

    public void Unstan()
    {
        IsStaned = false;
    }
}
