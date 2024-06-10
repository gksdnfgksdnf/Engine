using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] private bool _static = false;

    private Rigidbody _rb;
    private Vector3 _targetPosition;
    private float _obstacleSpeed = 20f;
    private bool _isMoving = false;
    private Stack<Vector3> _positionHistory = new Stack<Vector3>();

    [SerializeField] private float _range = 1f;
    [SerializeField] private LayerMask _whatisObstacle;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Obstacle");
        _rb = GetComponent<Rigidbody>();
        _whatisObstacle = 1 << LayerMask.NameToLayer("Obstacle");
        _rb.useGravity = false;
    }

    private void FixedUpdate()
    {
        if (_static) return;

        if (_isMoving)
            Move();
    }

    private void Move()
    {
        Vector3 newPosition = Vector3.MoveTowards(_rb.position, _targetPosition, _obstacleSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(newPosition);

        if (Vector3.Distance(_rb.position, _targetPosition) <= 0.001f)
        {
            _rb.position = _targetPosition;
            _isMoving = false;
        }
    }

    public void StartMovement(Vector3 direction)
    {
        _positionHistory.Push(_rb.position);
        _targetPosition = _rb.position + direction;
        _isMoving = true;
        CheckForObstaclesInDirection(direction);
    }

    private void CheckForObstaclesInDirection(Vector3 direction)
    {
        if (Physics.Raycast(transform.position, direction, out RaycastHit hit, _range, _whatisObstacle))
            if (hit.collider.TryGetComponent<Obstacle>(out Obstacle obstacle))
                HandleObstacleHit(obstacle, direction);
    }

    private void HandleObstacleHit(Obstacle obstacle, Vector3 direction)
    {
        if (_isMoving)
            obstacle.StartMovement(direction);
    }

    public void UndoMove()
    {
        if (_positionHistory.Count > 0)
        {
            Vector3 lastPosition = _positionHistory.Pop();
            _targetPosition = lastPosition;
            _isMoving = true;
        }
    }

}
