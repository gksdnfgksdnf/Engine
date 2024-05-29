using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput _playerInput;

    [SerializeField] float _speed = 5f;

    private bool _isMove = false;

    private Vector3 targetPosition;
    private Vector3 _movement;
    private Quaternion _targetRotation;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    private void Start()
    {
        _playerInput.OnMove += SetDirection;
    }

    private void SetDirection(Vector3 movement)
    {
        if (!_isMove)
        {
            _movement = movement;
            targetPosition = transform.position + movement;
            _isMove = true;
        }
    }

    private void ApplyRotation(Vector3 movement)
    {
        _targetRotation = Quaternion.LookRotation(movement);
        transform.rotation = _targetRotation;
    }

    private void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        if (_isMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * Time.fixedDeltaTime);

            if (transform.position == targetPosition)
                _isMove = false;
        }
        if (_movement != Vector3.zero)
            ApplyRotation(_movement);
    }

}
