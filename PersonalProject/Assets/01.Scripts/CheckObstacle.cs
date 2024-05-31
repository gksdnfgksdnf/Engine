using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckObstacle : MonoBehaviour
{
    PlayerMovement _player;
    PlayerInput _playerInput;
    [SerializeField] float _range = 2f;
    [SerializeField] private LayerMask _whatisObstacle;
    [SerializeField] private bool _isMove;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerMovement>();
        _playerInput = GetComponentInParent<PlayerInput>();
        _playerInput.OnObstacle += OnMove;
    }

    private void Update()
    {
        DrawRay();
    }

    public void DrawRay()
    {
        bool isHit = Physics.Raycast(transform.position, _player._movementDirection, out RaycastHit hit, _range, _whatisObstacle);

        if (isHit)
        {
            if (_isMove)
                if (hit.collider.TryGetComponent<Obstacle>(out Obstacle obstacle))
                {
                    obstacle.SetDirection(_player._movementDirection);
                    _isMove = false; // 키 입력 처리 후 _isMove를 false로 설정
                }
        }
        else
            _isMove = false;
    }

    private void OnMove()
    {
        _isMove = true;
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
