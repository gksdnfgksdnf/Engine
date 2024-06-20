using System;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector3> OnMove;

    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("W key pressed");
            OnMove?.Invoke(Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S key pressed");
            OnMove?.Invoke(Vector3.back);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A key pressed");
            OnMove?.Invoke(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log("D key pressed");
            OnMove?.Invoke(Vector3.right);
        }
    }
}


