using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyInfo))]
public class EnemyCombat : Attackable
{
    [SerializeField] private AudioClip[] _swingSounds;
    [SerializeField] private CircleCollider2D _attackField;

    private EnemyInfo _enemy;
    private bool _isAttacking = false;
    private List<Collider2D> _attackedPlayers = new List<Collider2D>();
    private ContactFilter2D _playerFilter = new ContactFilter2D();
    private Coroutine _attacking;

    public bool IsSeeingPlayer { get; private set; } = false;
    public CircleCollider2D AttackField => _attackField;

    private void Start()
    {
        _enemy = Character as EnemyInfo;
        _playerFilter.SetLayerMask(LayerMask.GetMask("Player"));
    }

    private void FixedUpdate()
    {
        if (IsSeeingPlayer && _isAttacking == false && IsDead == false && IsStaned == false)
        {
            _attackField.OverlapCollider(_playerFilter, _attackedPlayers);

            if (_attackedPlayers.Count > 0 && _attackedPlayers[0].TryGetComponent(out Attackable player) && player.IsDead == false)
                _attacking = StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        _isAttacking = true;

        _enemy.Anim.SetTrigger(EnemyAnimatorData.Params.Attack);
        _enemy.Audio.PlayOneShot(_swingSounds[Random.Range(0, _swingSounds.Length)]);

        yield return new WaitForSeconds(_enemy.Stats.AttackDuration / 2f);

        _attackField.OverlapCollider(_playerFilter, _attackedPlayers);

        if (_attackedPlayers.Count > 0 && _attackedPlayers[0].TryGetComponent(out Attackable player))
        {
            player.TakeDamage(_enemy.Stats.AttackDamage);
            player.TakeDamageForce(100f * (Vector2)(_attackedPlayers[0].bounds.center - transform.position));
        }

        yield return new WaitForSeconds(_enemy.Stats.AttackDuration / 2f + _enemy.Stats.AttackCooldown);

        _isAttacking = false;
    }

    public override void TakeDamage(float damage)
    {
        _enemy.Anim.SetTrigger(EnemyAnimatorData.Params.Hurt);
        base.TakeDamage(damage);
    }

    public override IEnumerator Die()
    {
        _enemy.Anim.SetTrigger(EnemyAnimatorData.Params.Die);

        if (_attacking != null)
        {
            StopCoroutine(_attacking);
            _isAttacking = false;
        }

        yield return base.Die();
    }

    public override IEnumerator Stan()
    {
        if (_attacking != null)
        {
            StopCoroutine(_attacking);
            _isAttacking = false;
        }

        return base.Stan();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerInfo _))
            IsSeeingPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent(out PlayerInfo _))
            IsSeeingPlayer = false;
    }
}
