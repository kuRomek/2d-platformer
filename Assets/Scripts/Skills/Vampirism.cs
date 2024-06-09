using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampirism : Skill
{
    [SerializeField] private float _healthByOneSecond;
    [SerializeField] private float _durationSeconds;
    [SerializeField] private Collider2D _effect;
    [SerializeField] private AudioClip _sound;
    [SerializeField] private CircleCollider2D _useField;

    private PlayerInfo _player;
    private EnemyInfo _enemy;
    private List<Collider2D> _hitEnemies = new List<Collider2D>();
    private ContactFilter2D _enemyFilter = new ContactFilter2D();

    public event Action OnSkillUsed;
    public event Action OnSkillEnd;

    public float DurationSeconds => _durationSeconds;

    private void Start()
    {
        _player = GetComponent<PlayerInfo>();
        _enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
    }

    protected override IEnumerator Execute()
    {
        yield return base.Execute();

        _useField.OverlapCollider(_enemyFilter, _hitEnemies);

        if (_hitEnemies.Count != 0 && _hitEnemies[0].TryGetComponent(out _enemy))
        {
            OnSkillUsed?.Invoke();

            float endTime = Time.time + _durationSeconds;

            _enemy.Combat.Stan();
            _player.Combat.Stan();
            _player.Audio.PlayOneShot(_sound);

            Collider2D effect = Instantiate(_effect);

            while (Time.time < endTime)
            {
                SetTransformOfEffect(effect, _enemy);
                _enemy.Stats.HealthPoints.TakeDamage(_healthByOneSecond * Time.deltaTime);
                _player.Stats.HealthPoints.Heal(_healthByOneSecond * Time.deltaTime);
                
                if (_enemy.Combat.IsDead || Input.anyKeyDown)
                {
                    Interrupt();
                    Destroy(effect.gameObject);
                }

                yield return null;
            }

            _enemy.Combat.Unstan();
            _player.Combat.Unstan();
            _player.Audio.Stop();

            OnSkillEnd?.Invoke();
            Destroy(effect.gameObject);
        }
        else
        {
            ResetCooldown();
        }
    }

    private void SetTransformOfEffect(Collider2D effect, EnemyInfo enemy)
    {
        float angle = -90f;

        if (transform.position.y > enemy.transform.position.y)
            angle -= Vector3.Angle(Vector3.right, enemy.Collider.bounds.center - _player.Collider.bounds.center);
        else
            angle += Vector3.Angle(Vector3.right, enemy.Collider.bounds.center - _player.Collider.bounds.center);

        effect.transform.eulerAngles = new Vector3(0f, 0f, angle);
        effect.transform.position = Vector3.Lerp(enemy.Collider.bounds.center, _player.Collider.bounds.center, 0.5f);
        effect.transform.localScale = new Vector2(_player.Sprite.transform.localScale.x, effect.transform.localScale.y / effect.bounds.size.x *
                                                        Vector3.Distance(enemy.Collider.bounds.center, _player.Collider.bounds.center));
    }

    public override void Interrupt()
    {
        base.Interrupt();

        _enemy.Combat.Unstan();
        _player.Combat.Unstan();
        _player.Audio.Stop();
        OnSkillEnd?.Invoke();
    }
}
