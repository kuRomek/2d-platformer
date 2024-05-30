using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterInfo))]
public class Attackable : MonoBehaviour
{
    [SerializeField] private AudioClip[] _hitSounds;

    private CharacterInfo _character;
    private bool _isDead = false;
    private float _dyingSeconds = 1f;
    private bool _isStaned = false;
    private float _stanedSeconds = 0.4f;
    
    protected CharacterInfo Character => _character;
    public bool IsDead => _isDead;
    public bool IsStaned => _isStaned;

    private void Awake()
    {
        _character = GetComponent<CharacterInfo>();
    }

    public virtual void TakeDamage(float damage)
    {
        _character.BaseStats.SubtractHP(damage);
        _character.Audio.PlayOneShot(_hitSounds[Random.Range(0, _hitSounds.Length)]);

        if (_character.BaseStats.HealthPoints <= 0)
            StartCoroutine(Die());
        else
            StartCoroutine(Stan());

    }

    public virtual void TakeDamageForce(Vector2 force)
    {
        _character.Rigidbody.velocity = Vector2.zero;
        _character.Rigidbody.AddForce(force);
    }

    public virtual IEnumerator Die()
    {
        _isDead = true;

        yield return new WaitForSeconds(_dyingSeconds);

        _character.Collider.enabled = false;
        _character.Rigidbody.velocity = Vector2.zero;
        _character.Rigidbody.isKinematic = true;
    }

    public virtual IEnumerator Stan()
    {
        _isStaned = true;
        yield return new WaitForSeconds(_stanedSeconds);
        _isStaned = false;
    }
}
