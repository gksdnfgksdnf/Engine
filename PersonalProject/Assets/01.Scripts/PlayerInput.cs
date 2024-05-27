using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    public event Action<Vector3> OnMove;

    private Vector3 _movement;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        _movement = new Vector3(h, 0, v).normalized;

        OnMove?.Invoke(_movement);
    }
}
