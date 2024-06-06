using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterInfo))]
public class Attackable<StatsType> : MonoBehaviour where StatsType : CharacterStats
{
    [SerializeField] private AudioClip[] _hitSounds;

    private bool _isDead = false;
    private float _dyingSeconds = 1f;
    private bool _isStaned = false;
    private float _stanedSeconds = 0.4f;

    public bool IsStaned => _isStaned;
    public bool IsDead => _isDead;
    protected CharacterInfo<StatsType> Character { get; private set; }

    private void Awake()
    {
        Character = GetComponent<CharacterInfo<StatsType>>();
    }

    public virtual void TakeDamage(float damage)
    {
        Character.Stats.HealthPoints.TakeDamage(damage);
        Character.Audio.PlayOneShot(_hitSounds[Random.Range(0, _hitSounds.Length)]);

        if (Character.Stats.HealthPoints.Amount <= 0)
            StartCoroutine(Die());
        else
            StartCoroutine(Stan());

    }

    public virtual void TakeDamageForce(Vector2 force)
    {
        Character.Rigidbody.velocity = Vector2.zero;
        Character.Rigidbody.AddForce(force);
    }

    protected virtual IEnumerator Die()
    {
        _isDead = true;

        yield return new WaitForSeconds(_dyingSeconds);

        Character.Collider.enabled = false;
        Character.Rigidbody.velocity = Vector2.zero;
        Character.Rigidbody.isKinematic = true;
    }

    protected virtual IEnumerator Stan()
    {
        _isStaned = true;
        yield return new WaitForSeconds(_stanedSeconds);
        _isStaned = false;
    }
}
