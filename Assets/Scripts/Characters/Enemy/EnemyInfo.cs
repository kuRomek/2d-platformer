using UnityEngine;

public class EnemyInfo : CharacterInfo
{
    [SerializeField] private EnemyMovement _movement;
    [SerializeField] private EnemyCombat _combat;
    [SerializeField] private Animator _animator;

    public EnemyStats Stats { get; private set; }
    public EnemyMovement Movement => _movement;
    public EnemyCombat Combat => _combat;
    public Animator Anim => _animator;

    private void Start()
    {
        Stats = BaseStats as EnemyStats;
    }
}
