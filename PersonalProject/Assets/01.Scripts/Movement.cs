using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private Rigidbody _rb;

    [SerializeField] private float _speed = 20f;
    private Vector3 _moveDir = Vector3.zero;
    private Vector3 _targetPos;
    [SerializeField] private Transform _ladderStartPos;
    [SerializeField] private Transform _ladderEndPos;

    private bool _isMoving = false;
    private bool _isLadderMoving = false;
    //private bool _isLadderConnection = false;
    //private bool _reverseLadderPos = false;

    private Stack<(Vector3, Quaternion)> _moveHisTory = new Stack<(Vector3, Quaternion)>();


    [SerializeField] private float _range = 1f;
    [SerializeField] public LayerMask _whatisObstacle;

    private Vector3 currentInput;
    [SerializeField] private Obstacle[] _contactObjs = new Obstacle[4]; // 4방향의 장애물을 저장하기 위한 배열

    private Vector3[] _directions = { Vector3.forward, Vector3.back, Vector3.right, Vector3.left };

    private Stack<bool> _obstacleMove = new Stack<bool>();


    private bool _isObstacleMove = false;
    private LayerMask _whatisLadder;

    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Player");
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _whatisObstacle = 1 << LayerMask.NameToLayer("Obstacle");
        _whatisLadder = 1 << LayerMask.NameToLayer("Ladder");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            UndoObstacle();
            UndoMove();
        }

        DrawRays();
        Inputs();

        if (_isMoving)
            Move();

        if (_isLadderMoving && !_isMoving)
        {
            //LadderMovement(_ladderStartPos.position);
        }
    }

    private void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.W))
            StartMovement(Vector3.forward);
        else if (Input.GetKeyDown(KeyCode.S))
            StartMovement(Vector3.back);
        else if (Input.GetKeyDown(KeyCode.A))
            StartMovement(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.D))
            StartMovement(Vector3.right);
    }
    public void StartMovement(Vector3 direction)
    {
        currentInput = direction;
        AttemptMove();

        if (_isMoving) return;

        _moveHisTory.Push((_rb.position, transform.rotation));
        _moveDir = direction;
        _targetPos = _rb.position + direction;

        if (direction.normalized != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction.normalized);

        _isMoving = true;

    }

    private void Move()
    {
        Vector3 newPosition = Vector3.MoveTowards(_rb.position, _targetPos, _speed * Time.deltaTime);
        _rb.MovePosition(newPosition);

        if (Vector3.Distance(_rb.position, _targetPos) <= 0.01f)
        {
            _rb.position = _targetPos;
            _isMoving = false;
        }
    }

    private void UndoMove()
    {
        if (_moveHisTory.Count > 0)
        {
            (_targetPos, transform.rotation) = _moveHisTory.Pop();
            _isMoving = true;
        }
    }

    private void DrawRays()
    {
        //bool ishit2 = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit2, _range, _whatisLadder);

        //if (ishit2)
        //    _isLadderConnection = true;
        //else
        //    _isLadderConnection = false;


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
    private void UndoObstacle()
    {
        if (_obstacleMove.Count > 0)
        {
            bool isHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, _range, _whatisObstacle);

            if (isHit && hit.collider.TryGetComponent(out Obstacle obstacle))
                obstacle.UndoMove(transform.forward);

            _obstacleMove.Pop();
        }
    }

    private void LadderMovement(Vector3 targetPos)
    {
        Vector3 newPosition = Vector3.MoveTowards(_rb.position, targetPos, _speed * Time.fixedDeltaTime);
        _rb.MovePosition(newPosition);

        if (Vector3.Distance(_rb.position, _targetPos) <= 0.01f)
        {
            _rb.position = _targetPos;
            _isLadderMoving = false;
        }
    }
}
