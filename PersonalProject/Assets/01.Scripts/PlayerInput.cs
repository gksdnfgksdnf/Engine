using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{

    public event Action<Vector3> OnMove;
    public event Action<Vector3> OnObstacleMove;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {

        if (Input.GetKeyDown(KeyCode.W))
            OnMove?.Invoke(Vector3.forward);
        else if (Input.GetKeyDown(KeyCode.S))
            OnMove?.Invoke(Vector3.back);
        else if (Input.GetKeyDown(KeyCode.A))
            OnMove?.Invoke(Vector3.left);
        else if (Input.GetKeyDown(KeyCode.D))
            OnMove?.Invoke(Vector3.right);
    }
}
