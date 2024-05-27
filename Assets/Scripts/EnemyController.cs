using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Checkpoint[] _checkpoints;
    [SerializeField] private Animator _animator;

    private int _currentCheckpointIndex = 0;

    private void Awake()
    {
        int walkingState = 2;

        if (_checkpoints.Length > 0 )
            _animator.SetInteger(EnemyAnimatorData.Params.AnimState, walkingState);
    }

    private void Update()
    {
        if (_checkpoints.Length > 0)
            Move();
    }

    private void Move()
    {
        if (transform.position.x == _checkpoints[_currentCheckpointIndex].transform.position.x)
        {
            _currentCheckpointIndex = ++_currentCheckpointIndex % _checkpoints.Length;
            transform.localScale = new Vector2(-transform.localScale.x, transform.localScale.y);
        }

        transform.position = Vector2.MoveTowards(transform.position, 
                                                 new Vector2(_checkpoints[_currentCheckpointIndex].transform.position.x, transform.position.y), 
                                                 _speed * Time.deltaTime);
    }
}
