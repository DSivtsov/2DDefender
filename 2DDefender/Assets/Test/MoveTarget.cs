using UnityEngine;

public sealed class MoveTarget : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 0.1f;
    [SerializeField] private Vector3 _moveDirection = Vector3.down;

    public Vector3 MoveDirection => _moveDirection;
    public float SpeedTarget => _moveSpeed;
    
    public Vector3 Position => _transform.position;
    public Vector3 Velocity => _velocity;

    private Transform _transform;
    private Vector3 _velocity;

    private void Awake()
    {
        _transform = transform;
        _velocity = _moveDirection * _moveSpeed;
    }

    void Update()
    {
        _transform.Translate(_velocity * Time.deltaTime, Space.World);
    }
}
