using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{

    public event Action<Vector3> OnMove;
    public event Action<Vector3> OnObstacleMove;

    private Vector3 moveDirection = Vector3.zero; // 이동 방향을 저장하는 변수

    private bool _isCanMove = false;

    private bool _isCanMoveObstacle = false;
    void Update()
    {
        Movement();
    }

    private void Movement()
    {

        int h = Input.GetKeyDown(KeyCode.D) ? 1 : Input.GetKeyDown(KeyCode.A) ? -1 : 0;
        int v = Input.GetKeyDown(KeyCode.W) ? 1 : Input.GetKeyDown(KeyCode.S) ? -1 : 0;


        if ((h != 0 || v != 0) && _isCanMove)
        {
            moveDirection = new Vector3(h, 0, v);
            OnMove?.Invoke(moveDirection);
            OnObstacleMove?.Invoke(moveDirection);

            _isCanMove = false;
        }

        if (h == 0 && v == 0)
        {
            _isCanMove = true;
        }
    }
}
