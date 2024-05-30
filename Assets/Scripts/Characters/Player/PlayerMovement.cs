using UnityEngine;

[RequireComponent(typeof(PlayerInfo))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private AudioClip[] _landingSounds;

    private PlayerInfo _player;

    private void Start()
    {
        _player = GetComponent<PlayerInfo>();
    }

    private void FixedUpdate()
    {
        Move();

        if (_player.Input.ReadyToJump)
            Jump();
    }

    private void Move()
    {
        transform.Translate(new Vector2(_player.Stats.Speed * _player.Input.Direction * Time.fixedDeltaTime, 0f));

        if (_player.Input.Direction != 0f)
        {
            transform.localScale = new Vector2(Mathf.Sign(_player.Input.Direction), _player.transform.localScale.y);
            _player.Anim.speed = Mathf.Abs(_player.Input.Direction);
        }
        else
        {
            _player.Anim.speed = 1f;
        }

        int idleAnimationState = 0;
        int walkingAnimationState = 1;
        _player.Anim.SetInteger(PlayerAnimatorData.Params.AnimState, _player.Input.Direction == 0 ? idleAnimationState : walkingAnimationState);

        _player.Anim.SetBool(PlayerAnimatorData.Params.Grounded, IsGrounded());
        _player.Anim.SetFloat(PlayerAnimatorData.Params.AirSpeedY, _player.Rigidbody.velocity.y);
    }

    private void Jump()
    {
        _player.Rigidbody.velocity = Vector2.zero;
        _player.Rigidbody.AddForce(Vector2.up * _player.Stats.JumpForce, ForceMode2D.Impulse);
        _player.Anim.SetTrigger(PlayerAnimatorData.Params.Jump);

        _player.Input.ResetJump();
    }

    public bool IsGrounded()
    {
        float colliderOffset = 0.05f;

        BoxCollider2D collider = _player.Collider as BoxCollider2D;
        float rayLength = _player.Collider.bounds.extents.y + collider.edgeRadius + colliderOffset;

        Vector2 leftRayPoint = new Vector2(collider.bounds.min.x, collider.bounds.center.y);
        Vector2 rightRayPoint = new Vector2(collider.bounds.max.x, collider.bounds.center.y);

        return Physics2D.Raycast(leftRayPoint, Vector2.down, rayLength, LayerMask.GetMask("Ground")) ||
               Physics2D.Raycast(rightRayPoint, Vector2.down, rayLength, LayerMask.GetMask("Ground"));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && IsGrounded())
            _player.Audio.PlayOneShot(_landingSounds[Random.Range(0, _landingSounds.Length)]);
    }
}
