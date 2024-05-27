using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController _player;
    [SerializeField] private Collider2D _levelCollider;

    private Camera _camera;
    private float _cameraHorizontalExtent;
    private float _colliderOffset = 0.2f;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _cameraHorizontalExtent = _camera.aspect * _camera.orthographicSize;
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, 
                                          new Vector3(Mathf.Clamp(_player.transform.position.x, 0f, _levelCollider.bounds.max.x - _colliderOffset - _cameraHorizontalExtent),
                                          transform.position.y,
                                          transform.position.z), 0.05f);
    }
}
