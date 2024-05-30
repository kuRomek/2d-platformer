using UnityEngine;

[RequireComponent(typeof(EnemyInfo))]
public class EnemyMovement : MonoBehaviour
{
    private const int WalkingState = 2;
    private const int IdleState = 1;

    [SerializeField] private PlayerInfo _player;
    [SerializeField] private CircleCollider2D _fieldOfView;
    [SerializeField] private Checkpoint[] _checkpoints;

    private EnemyInfo _enemy;
    private bool _isGrounded;
    private int _currentCheckpointIndex = 0;
    private Vector2 _target;

    private void Start()
    {
        _enemy = GetComponent<EnemyInfo>();
        _isGrounded = true;
    }

    private void FixedUpdate()
    {
        if ((_enemy.Combat.IsDead || _enemy.Combat.IsStaned) == false)
            Move();
    }

    private void Move()
    {
        if (_enemy.Combat.IsSeeingPlayer)
        {
            Vector2 nearbyPlayer = new Vector2(_player.transform.position.x + transform.localScale.x * 1.0f, transform.position.y);

            if (Mathf.Abs(transform.position.x - _player.transform.position.x) < Mathf.Abs(nearbyPlayer.x - _player.transform.position.x))
                _target = transform.position;
            else
                _target = nearbyPlayer;

            transform.localScale = new Vector3(Mathf.Sign(transform.position.x - _player.transform.position.x), 1f, 1f);

            if (_enemy.Combat.AttackField.IsTouchingLayers(LayerMask.GetMask("Ground")) && _isGrounded)
                Jump();
        }
        else if (_checkpoints.Length == 0)
        {
            _target = transform.position;
        }
        else
        {
            if (transform.position.x == _checkpoints[_currentCheckpointIndex].transform.position.x)
                _currentCheckpointIndex = ++_currentCheckpointIndex % _checkpoints.Length;

            _target = new Vector2(_checkpoints[_currentCheckpointIndex].transform.position.x, transform.position.y);

            transform.localScale = new Vector3(Mathf.Sign(transform.position.x - _target.x), 1f, 1f);

            if (_enemy.Combat.AttackField.IsTouchingLayers(LayerMask.GetMask("Ground")) && _isGrounded)
                Jump();
        }

        _enemy.Anim.SetInteger(EnemyAnimatorData.Params.AnimState, _target == (Vector2)transform.position ? IdleState : WalkingState);

        transform.position = Vector2.MoveTowards(transform.position, _target, _enemy.Stats.Speed * Time.fixedDeltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            _isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            _isGrounded = false;
    }

    private void Jump()
    {
        _enemy.Rigidbody.velocity = Vector2.zero;
        _enemy.Rigidbody.AddForce(Vector2.up * _enemy.Stats.JumpForce + Vector2.left * transform.localScale.x, ForceMode2D.Impulse);
        _enemy.Anim.SetTrigger(EnemyAnimatorData.Params.Jump);
    }
}
