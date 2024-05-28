using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{

    public event Action<Vector3> OnMove;

    private Vector3 moveDirection = Vector3.zero; // �̵� ������ �����ϴ� ����

    private bool _isMove = false;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {

        int h = Input.GetKeyDown(KeyCode.D) ? 1 : Input.GetKeyDown(KeyCode.A) ? -1 : 0;
        int v = Input.GetKeyDown(KeyCode.W) ? 1 : Input.GetKeyDown(KeyCode.S) ? -1 : 0;


        // �Է��� ������ ��� �̵� ���� ����
        if ((h != 0 || v != 0) && _isMove)
        {
            moveDirection = new Vector3(h, 0, v);
            OnMove?.Invoke(moveDirection);
            _isMove = false;
        }

        if (h == 0 && v == 0)
        {
            _isMove = true;
        }
    }
}
