using System.Collections.Generic;
using UnityEngine;

public class CheckObstacle : MonoBehaviour
{
    private PlayerInput _playerInput;
    [SerializeField] private float _range = 1f;
    [SerializeField] private LayerMask _whatisObstacle;

    private Vector3 currentInput;
    [SerializeField] private Obstacle[] _contactObjs = new Obstacle[4]; // 4������ ��ֹ��� �����ϱ� ���� �迭

    private Vector3[] _directions = { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };

    private Stack<bool> _obstacleMove = new Stack<bool>();


    private bool _isObstacleMove = false;

    private void Awake()
    {
        _playerInput = GetComponentInParent<PlayerInput>();
        _playerInput.OnMove += OnMove;
    }

    private void Update()
    {
        DrawRays();

        if (Input.GetKeyDown(KeyCode.Z))
            UndoMove();
    }

    private void OnMove(Vector3 input)
    {
        currentInput = input;
        AttemptMove();
    }

    private void DrawRays()
    {
        for (int i = 0; i < _directions.Length; i++)
        {
            Vector3 direction = _directions[i];
            bool isHit = Physics.Raycast(transform.position, direction, out RaycastHit hit, _range, _whatisObstacle);

            if (isHit && hit.collider.TryGetComponent<Obstacle>(out Obstacle obstacle))
                _contactObjs[i] = obstacle; // �� ������ ��ֹ��� ����
            else
                _contactObjs[i] = null; // ��ֹ��� ������ null�� ����
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

        if (_isObstacleMove == false)
            _obstacleMove.Push(false);

        _isObstacleMove = false;

    }

    private void UndoMove()
    {
        if (_obstacleMove.Count > 0 && _obstacleMove.Pop())
        {
            bool isHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _range, _whatisObstacle);

            if (isHit && hit.collider.TryGetComponent<Obstacle>(out Obstacle obstacle))
                obstacle.UndoMove();
        }
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