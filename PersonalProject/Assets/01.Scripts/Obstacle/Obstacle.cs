using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private PlayerInput _player;

    private bool _isCanMove = false;
    private float _obstacleSpeed = 20f;
    private Vector3 _targetPosition;
    private bool _isMoving = false;

    private void Start()
    {
        _player.OnObstacleMove += ObstacleMove;
    }

    private void ObstacleMove(Vector3 targetPos)
    {
        _targetPosition = transform.position + targetPos;
        _isMoving = true;

    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (_isMoving && _isCanMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _obstacleSpeed * Time.fixedDeltaTime);

            if (transform.position == _targetPosition)
                _isMoving = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            _isCanMove = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
            _isCanMove = false;
    }
}
