using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    public PlayerInput _playerInput;

    [SerializeField] private float _speed = 5f;
    public Vector3 _movementDirection = Vector3.zero;
    private Vector3 _targetPosition;
    private Quaternion _initialRotation;

    private bool _isMoving = false;

    private Stack<(Vector3, Quaternion)> _moveHisTory = new Stack<(Vector3, Quaternion)>();

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.OnMove += StartMovement;
        _initialRotation = transform.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            UndoLastMove();
        }
    }

    public void StartMovement(Vector3 direction)
    {
        if (_isMoving) return;

        _moveHisTory.Push((_rb.position, transform.rotation));
        _movementDirection = direction;
        _targetPosition = _rb.position + _movementDirection;

        if (_movementDirection.normalized != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(_movementDirection.normalized);

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
            _rb.position = _targetPosition;
            _isMoving = false;
        }
    }

    private void ApplyRotation()
    {

    }

    private void UndoLastMove()
    {
        if (_moveHisTory.Count > 0)
        {
            (_targetPosition, transform.rotation) = _moveHisTory.Pop();
            _isMoving = true;
        }
    }
}
