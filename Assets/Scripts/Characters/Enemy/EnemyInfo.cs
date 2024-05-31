using UnityEngine;

public class EnemyInfo : CharacterInfo<EnemyStats>
{
    [SerializeField] private EnemyMovement _movement;
    [SerializeField] private EnemyCombat _combat;
    [SerializeField] private Animator _animator;

    public EnemyMovement Movement => _movement;
    public EnemyCombat Combat => _combat;
    public Animator Anim => _animator;
}
