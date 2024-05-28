using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private PlayerInput _playerInput;
    private Vector3 targetPosition;
    [SerializeField] float _speed = 5f;

    private bool _isMove = false;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        _playerInput.OnMove += Movement;
    }

    private void Movement(Vector3 movement)
    {
        if (!_isMove)
        {
            targetPosition = transform.position + movement;
            _isMove = true;
        }
    }

    private void FixedUpdate()
    {
        if (_isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.fixedDeltaTime);

            if (transform.position == targetPosition)
            {
                _isMove = false;
            }
        }
    }
}
