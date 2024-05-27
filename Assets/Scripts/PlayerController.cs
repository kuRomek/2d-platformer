using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Animator _animator;

    private Collider2D _collider;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(new Vector2(_speed * horizontalInput * Time.deltaTime, 0f));

        if (horizontalInput != 0f)
        {
            transform.localScale = new Vector2(Mathf.Sign(horizontalInput), transform.localScale.y);
            _animator.speed = Mathf.Abs(horizontalInput);
        }
        else
        {
            _animator.speed = 1f;
        }

        int idleAnimationState = 0;
        int walkingAnimationState = 1;

        _animator.SetInteger(PlayerAnimatorData.Params.AnimState, horizontalInput == 0 ? idleAnimationState : walkingAnimationState);

        bool isGrounded = IsGrounded();

        _animator.SetBool(PlayerAnimatorData.Params.Grounded, isGrounded);

        if (Input.GetButtonDown("Jump") && isGrounded)
            Jump();

        _animator.SetFloat(PlayerAnimatorData.Params.AirSpeedY, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        _animator.SetTrigger(PlayerAnimatorData.Params.Jump);
    }

    private bool IsGrounded()
    {
        float colliderOffset = 0.1f;

        Vector2 leftRayPoint = new Vector2(_collider.bounds.min.x, _collider.bounds.center.y);
        Vector2 rightRayPoint = new Vector2(_collider.bounds.max.x, _collider.bounds.center.y);

        return Physics2D.Raycast(leftRayPoint, Vector2.down, _collider.bounds.extents.y + colliderOffset, LayerMask.GetMask("Ground")) ||
               Physics2D.Raycast(rightRayPoint, Vector2.down, _collider.bounds.extents.y + colliderOffset, LayerMask.GetMask("Ground"));
    }
}
