using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement _playerMovement;

    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);

    public float Direction { get; private set; }
    public bool ReadyToJump { get; private set; }

    private void Update()
    {
        Direction = Input.GetAxis(Horizontal);

        if (Input.GetButtonDown("Jump") && _playerMovement.IsGrounded())
            ReadyToJump = true;
    }

    public void ResetJump()
    {
        ReadyToJump = false;
    }
}
