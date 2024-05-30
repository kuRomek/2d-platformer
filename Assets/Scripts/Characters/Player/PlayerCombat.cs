using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : Attackable
{
    [SerializeField] private AudioClip[] _swingSounds;
    [SerializeField] private CircleCollider2D _attackField;

    private PlayerInfo _player;
    private List<Collider2D> _attackedEnemies = new List<Collider2D>();
    private ContactFilter2D _enemyFilter = new ContactFilter2D();
    private bool _isInvincible = false;
    private float _invincibleSeconds = 0.75f;
    private Coroutine _attacking;

    public bool IsAttacking { get; private set; } = false;

    private void Start()
    {
        _player = Character as PlayerInfo;
        _enemyFilter.SetLayerMask(LayerMask.GetMask("Enemy"));
    }

    private void FixedUpdate()
    {
        if (_player.Input.ReadyForLightAttack)
        { 
            _attacking = StartCoroutine(Attack(PlayerStats.AttackType.Light));
            _player.Input.ResetLightAttack();
        }

        if (_player.Input.ReadyForHeavyAttack)
        {
            _attacking = StartCoroutine(Attack(PlayerStats.AttackType.Heavy));
            _player.Input.ResetHeavyAttack();
        }
    }

    private IEnumerator Attack(PlayerStats.AttackType type)
    {
        IsAttacking = true;

        _player.Anim.SetTrigger(type == PlayerStats.AttackType.Light ? PlayerAnimatorData.Params.LightAttack : PlayerAnimatorData.Params.HeavyAttack);
        _player.Audio.PlayOneShot(_swingSounds[Random.Range(0, _swingSounds.Length)]);

        yield return new WaitForSeconds(_player.Stats.GetAttackDuration(type) * 0.33f);

        _attackField.OverlapCollider(_enemyFilter, _attackedEnemies);

        foreach (Collider2D enemyCollider in _attackedEnemies)
        {
            if (enemyCollider.TryGetComponent(out Attackable enemy))
            {
                enemy.TakeDamage(_player.Stats.GetAttackDamage(type));
                enemy.TakeDamageForce(100f * (Vector2)(enemyCollider.bounds.center - transform.position));
            }
        }

        yield return new WaitForSeconds(_player.Stats.GetAttackDuration(type) * 0.67f);

        IsAttacking = false;
    }

    public override void TakeDamage(float damage)
    {
        if (_isInvincible == false)
        {
            _player.Anim.SetTrigger(PlayerAnimatorData.Params.Hurt);

            base.TakeDamage(damage);

            StartCoroutine(StayInvincible());
        }
    }

    public override void TakeDamageForce(Vector2 force)
    {
        if (_isInvincible == false)
            base.TakeDamageForce(force);
    }

    public override IEnumerator Stan()
    {
        if (_attacking != null)
        {
            StopCoroutine(_attacking);
            IsAttacking = false;
        }

        yield return base.Stan();
    }

    public override IEnumerator Die()
    {
        _player.Anim.SetTrigger(PlayerAnimatorData.Params.Die);

        if (_attacking != null)
        {
            StopCoroutine(_attacking);
            IsAttacking = false;
        }

        yield return base.Die();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out EnemyInfo enemy) && IsDead == false && enemy.Combat.IsDead == false)
        {
            float force = 350f;

            TakeDamageForce(force * (collision.GetContact(0).point - (Vector2)collision.transform.position));
            TakeDamage(enemy.Stats.TickDamage);
        }
    }

    private IEnumerator StayInvincible()
    {
        _isInvincible = true;
        yield return new WaitForSeconds(_invincibleSeconds);
        _isInvincible = false;
    }
}
