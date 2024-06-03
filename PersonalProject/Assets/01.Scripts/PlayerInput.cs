using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public event Action<Vector3> OnMove;
    public event Action<Vector3> OnObstacle;

    private Stack<InputStateRecord> inputStateRecords = new Stack<InputStateRecord>();

    void Update()
    {
        RecordInputState();
        Movement();
    }

    private void RecordInputState()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) ||
            Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            KeyCode keyPressed = KeyCode.None;
            if (Input.GetKeyDown(KeyCode.W)) keyPressed = KeyCode.W;
            else if (Input.GetKeyDown(KeyCode.S)) keyPressed = KeyCode.S;
            else if (Input.GetKeyDown(KeyCode.A)) keyPressed = KeyCode.A;
            else if (Input.GetKeyDown(KeyCode.D)) keyPressed = KeyCode.D;

            InputStateRecord record = new InputStateRecord
            {
                timestamp = Time.time,
                keyPressed = keyPressed
            };

            inputStateRecords.Push(record);
        }
    }

    private void Movement()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Undo();
        }
        if (!RuleManager.instance.BaboIsYou()) return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            OnMove?.Invoke(Vector3.forward);
            OnObstacle?.Invoke(Vector3.forward);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            OnMove?.Invoke(Vector3.back);
            OnObstacle?.Invoke(Vector3.back);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            OnMove?.Invoke(Vector3.left);
            OnObstacle?.Invoke(Vector3.left);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            OnMove?.Invoke(Vector3.right);
            OnObstacle?.Invoke(Vector3.right);
        }

    }

    public void Undo()
    {
        if (inputStateRecords.Count > 0)
        {
            InputStateRecord lastRecord = inputStateRecords.Pop();
            ApplyInputState(lastRecord);

            Vector3 originalDirection = GetOriginalDirection(lastRecord.keyPressed);
            OnMove?.Invoke(originalDirection);
            OnObstacle?.Invoke(originalDirection);
        }
    }

    private Vector3 GetOriginalDirection(KeyCode keyCode)
    {
        // WASD 키에 대한 원래 방향을 반환
        switch (keyCode)
        {
            case KeyCode.W:
                return Vector3.back;
            case KeyCode.S:
                return Vector3.forward;
            case KeyCode.A:
                return Vector3.right;
            case KeyCode.D:
                return Vector3.left;
            default:
                return Vector3.zero;
        }
    }

    private void ApplyInputState(InputStateRecord record)
    {
        KeyCode keyPressed = record.keyPressed;

        if (keyPressed == KeyCode.W)
        {
            OnMove?.Invoke(Vector3.back);
            OnObstacle?.Invoke(Vector3.back);
        }
        else if (keyPressed == KeyCode.S)
        {
            OnMove?.Invoke(Vector3.forward);
            OnObstacle?.Invoke(Vector3.forward);
        }
        else if (keyPressed == KeyCode.A)
        {
            OnMove?.Invoke(Vector3.right);
            OnObstacle?.Invoke(Vector3.right);
        }
        else if (keyPressed == KeyCode.D)
        {
            OnMove?.Invoke(Vector3.left);
            OnObstacle?.Invoke(Vector3.left);
        }
    }
}
