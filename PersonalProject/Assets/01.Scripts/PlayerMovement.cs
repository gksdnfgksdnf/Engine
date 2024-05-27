using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput _playerInput;
    [SerializeField] float _speed = 5f;

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
        transform.Translate(movement * Time.deltaTime);
    }
}
