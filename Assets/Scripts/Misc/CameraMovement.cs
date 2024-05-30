using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraMovement : MonoBehaviour
{
    [SerializeField] private PlayerInfo _player;
    [SerializeField] private Collider2D _levelCollider;

    private Camera _camera;
    private float _colliderOffset = 0.2f;
    private float _speed = 0.05f;
    private float _startXValue;
    private float _finalXValue;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _startXValue = _levelCollider.bounds.min.x + _colliderOffset + _camera.aspect * _camera.orthographicSize;
        _finalXValue = _levelCollider.bounds.max.x - _colliderOffset - _camera.aspect * _camera.orthographicSize;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(Mathf.Clamp(_player.transform.position.x, _startXValue, _finalXValue),
                                                                          transform.position.y,
                                                                          transform.position.z), _speed);
    }
}
