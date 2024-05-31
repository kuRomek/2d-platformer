using UnityEngine;

public class PlayerInfo : CharacterInfo<PlayerStats>
{
    [SerializeField] private PlayerInput _input;
    [SerializeField] private PlayerMovement _movement;
    [SerializeField] private PlayerCombat _combat;
    [SerializeField] private Animator _animator;

    public PlayerInput Input => _input;
    public PlayerMovement Movement => _movement;
    public PlayerCombat Combat => _combat;
    public Animator Anim => _animator;
}
