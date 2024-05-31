using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{

    public event Action<Vector3> OnMove;
    public event Action OnObstacle;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            OnMove?.Invoke(Vector3.forward);
            OnObstacle?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            OnMove?.Invoke(Vector3.back);
            OnObstacle?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            OnMove?.Invoke(Vector3.left);
            OnObstacle?.Invoke();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            OnMove?.Invoke(Vector3.right);
            OnObstacle?.Invoke();
        }
    }
}
