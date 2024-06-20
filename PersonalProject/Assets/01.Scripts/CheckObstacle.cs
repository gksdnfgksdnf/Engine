using System.Collections.Generic;
using UnityEngine;

public class CheckObstacle : MonoBehaviour
{
    private PlayerInput _playerInput;

    [SerializeField] private float _range = 1f;
    [SerializeField] private LayerMask _whatisObstacle;

    private Vector3 currentInput;
    [SerializeField] private Obstacle[] _contactObjs = new Obstacle[4]; // 4방향의 장애물을 저장하기 위한 배열

    private Vector3[] _directions = { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };

    private Stack<bool> _obstacleMove = new Stack<bool>();

    private bool _isObstacleMove = false;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        if (_playerInput != null)
        {
            Debug.Log("PlayerInput component found.");
            _playerInput.OnMove += OnMove;
        }
        else
        {
            Debug.LogError("PlayerInput component is missing.");
        }

        _whatisObstacle = LayerMask.GetMask("Obstacle");
    }

    private void Update()
    {
        DrawRays();
    }

    private void OnMove(Vector3 input)
    {
        Debug.Log("ming");
        currentInput = input;
        AttemptMove();
    }

    private void DrawRays()
    {
        for (int i = 0; i < _directions.Length; i++)
        {
            Vector3 direction = _directions[i];
            bool isHit = Physics.Raycast(transform.position, direction, out RaycastHit hit, _range, _whatisObstacle);

            if (isHit && hit.collider.TryGetComponent(out Obstacle obstacle))
                _contactObjs[i] = obstacle; // 각 방향의 장애물을 저장
            else
                _contactObjs[i] = null; // 장애물이 없으면 null로 설정
        }
    }

    private void AttemptMove()
    {
        for (int i = 0; i < _directions.Length; i++)
        {
            if (_contactObjs[i] != null && currentInput == _directions[i])
            {
                _contactObjs[i].StartMovement(_directions[i]);
                _obstacleMove.Push(true);
                _isObstacleMove = true;
            }
        }

        if (!_isObstacleMove)
            _obstacleMove.Push(false);

        _isObstacleMove = false;

    }

    private void OnDrawGizmos()
    {
        foreach (Vector3 direction in _directions)
        {
            bool isHit = Physics.Raycast(transform.position, direction, out RaycastHit hit, _range, _whatisObstacle);

            Gizmos.color = isHit ? Color.red : Color.green;
            Gizmos.DrawRay(transform.position, direction * (isHit ? hit.distance : _range));
            if (isHit)
            {
                Gizmos.DrawSphere(hit.point, 0.1f);
            }
        }
    }
}
