using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(Rigidbody))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] Player _player;
    private PlayerMovement _playerMove;
    private PlayerInput _playerInput;

    [SerializeField] private bool _rock = false;

    [SerializeField] Obstacle _currentContactObj;

    private Rigidbody _rb;

    private Vector3 _targetPosition;
    private float _obstacleSpeed = 20f;
    private bool _isMoving = false;
    public bool _isContactPlayer = false;
    public bool _isContactObj = false;

    [SerializeField] private float _range = 1f;
    [SerializeField] private LayerMask _whatisObstacle;
    [SerializeField] private bool _isMove;

    private Vector3 lastInput;
    private Vector3 moveInput;


    private void Awake()
    {
        //_checkObstacle = _player.GetComponentInChildren<CheckObstacle>();
        _playerMove = _player.GetComponent<PlayerMovement>();
        _rb = GetComponent<Rigidbody>();
        _playerInput = _player.GetComponent<PlayerInput>();
        _playerInput.OnObstacle += OnMove;
    }
    private void FixedUpdate()
    {
        if (_rock) if (RuleManager.instance.RockIsPush()) return;

        if (_isMoving)
            Movement();
    }

    private void Movement()
    {
        Vector3 newPosition = Vector3.MoveTowards(_rb.position, _targetPosition, _obstacleSpeed * Time.fixedDeltaTime);
        _rb.MovePosition(newPosition);

        if (Vector3.Distance(_rb.position, _targetPosition) <= 0.001f)
        {
            _rb.position = _targetPosition; // 정확한 위치
            _isMoving = false;
        }
    }

    private void Update()
    {
        DrawRay();
        //if (_playerMove._movementDirection.normalized != Vector3.zero)
        //    transform.rotation = Quaternion.LookRotation(_playerMove._movementDirection.normalized);
    }

    private void OnMove(Vector3 input)
    {
        moveInput = input;
        _isMove = true;
    }

    public void DrawRay()
    {
        bool isHit = Physics.Raycast(transform.position, _playerMove._movementDirection, out RaycastHit hit, _range, _whatisObstacle);

        if (isHit)
        {
            if (hit.collider.TryGetComponent<Obstacle>(out Obstacle obstacle))
            {
                lastInput = moveInput;
                if (_isContactPlayer)
                {
                    _currentContactObj = obstacle;
                    _currentContactObj._isContactObj = true;
                    _currentContactObj._isContactPlayer = true;
                }
                else
                    _currentContactObj = null;

                if (_isMove)
                    if (lastInput == moveInput)
                        if (_currentContactObj != null)
                            if (_currentContactObj._isContactObj && _currentContactObj._isContactPlayer)
                            {
                                obstacle.SetDirection(_playerMove._movementDirection);
                                _isMove = false; // 키 입력 처리 후 _isMove를 false로 설정
                            }
            }
        }
        else
        {
            _isMove = false;
            if (_currentContactObj != null)
            {
                _currentContactObj._isContactObj = false;
                _currentContactObj._isContactPlayer = false;
            }
        }
    }

    public void SetDirection(Vector3 movementDirection)
    {
        _targetPosition = transform.position + movementDirection;
        _isMoving = true;
    }

    private void OnDrawGizmos()
    {
        RaycastHit hit;
        bool isHit = Physics.Raycast(transform.position, transform.forward, out hit, _range, _whatisObstacle);

        if (isHit)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.forward * hit.distance);
            Gizmos.DrawSphere(hit.point, 0.1f);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, transform.forward * _range);
        }
    }
}
