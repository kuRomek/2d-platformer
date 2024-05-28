using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Animator _animator;
    [SerializeField] private PlayerInput _input;

    private BoxCollider2D _collider;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();

        if (_input.ReadyToJump)
            Jump();
    }

    private void Move()
    {
        transform.Translate(new Vector2(_speed * _input.Direction * Time.fixedDeltaTime, 0f));

        if (_input.Direction != 0f)
        {
            transform.localScale = new Vector2(Mathf.Sign(_input.Direction), transform.localScale.y);
            _animator.speed = Mathf.Abs(_input.Direction);
        }
        else
        {
            _animator.speed = 1f;
        }

        int idleAnimationState = 0;
        int walkingAnimationState = 1;
        _animator.SetInteger(PlayerAnimatorData.Params.AnimState, _input.Direction == 0 ? idleAnimationState : walkingAnimationState);

        _animator.SetBool(PlayerAnimatorData.Params.Grounded, IsGrounded());
        _animator.SetFloat(PlayerAnimatorData.Params.AirSpeedY, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _animator.SetTrigger(PlayerAnimatorData.Params.Jump);

        _input.ResetJump();
    }

    public bool IsGrounded()
    {
        float colliderOffset = 0.1f;
        float rayLength = _collider.bounds.extents.y + _collider.edgeRadius + colliderOffset;

        Vector2 leftRayPoint = new Vector2(_collider.bounds.min.x, _collider.bounds.center.y);
        Vector2 rightRayPoint = new Vector2(_collider.bounds.max.x, _collider.bounds.center.y);

        return Physics2D.Raycast(leftRayPoint, Vector2.down, rayLength, LayerMask.GetMask("Ground")) ||
               Physics2D.Raycast(rightRayPoint, Vector2.down, rayLength, LayerMask.GetMask("Ground"));
    }
}
