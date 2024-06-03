using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    public PlayerInput _playerInput;

    [SerializeField] private float _speed = 5f;
    public Vector3 _movementDirection = Vector3.zero;
    private Vector3 _targetPosition;

    private bool _isMoving = false;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.OnMove += StartMovement;
    }

    private void StartMovement(Vector3 direction)
    {
        if (_isMoving) return;

        _movementDirection = direction;
        _targetPosition = _rb.position + _movementDirection;
        _isMoving = true;
    }

    private void FixedUpdate()
    {
        if (_isMoving)
            Move();

        ApplyRotation();
    }

    private void Move()
    {

        Vector3 newPosition = Vector3.MoveTowards(_rb.position, _targetPosition, _speed * Time.fixedDeltaTime);
        _rb.MovePosition(newPosition);

        if (Vector3.Distance(_rb.position, _targetPosition) <= 0.01f)
        {
            _rb.position = _targetPosition; // 정확한 위치
            _isMoving = false;
        }
    }

    private void ApplyRotation()
    {
        if (_movementDirection.normalized != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(_movementDirection.normalized);
    }
}
