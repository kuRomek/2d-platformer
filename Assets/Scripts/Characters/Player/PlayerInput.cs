using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInfo))]
public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);
    private const string Jump = nameof(Jump);
    private const string Fire1 = nameof(Fire1);
    private const string Fire2 = nameof(Fire2);

    private PlayerInfo _player;

    public float Direction { get; private set; }
    public bool ReadyToJump { get; private set; }
    public bool ReadyForLightAttack { get; private set; }
    public bool ReadyForHeavyAttack { get; private set; }

    private void Start()
    {
        _player = GetComponent<PlayerInfo>();
    }

    private void Update()
    {
        if ((_player.Combat.IsDead || _player.Combat.IsStaned) == false)
        {
            Direction = Input.GetAxis(Horizontal);

            if (Input.GetButtonDown(Jump) && _player.Movement.IsGrounded())
                ReadyToJump = true;

            if (Input.GetButtonDown(Fire1) && _player.Combat.IsAttacking == false)
                ReadyForLightAttack = true;

            if (Input.GetButtonDown(Fire2) && _player.Combat.IsAttacking == false)
                ReadyForHeavyAttack = true;
        }
        else
        {
            Direction = 0f;
        }

        if (_player.Combat.IsDead)
            if (Input.GetButtonDown(Jump))
                SceneManager.LoadScene(0);

        if (Input.GetKey(KeyCode.Escape))
            Application.Quit();
    }

    public void ResetJump()
    {
        ReadyToJump = false;
    }

    public void ResetLightAttack()
    {
        ReadyForLightAttack = false;
    }

    public void ResetHeavyAttack()
    {
        ReadyForHeavyAttack = false;
    }
}
