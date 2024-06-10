using System;
using System.Collections.Generic;
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
        //if (!RuleManager.instance.BaboIsYou()) return;

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
