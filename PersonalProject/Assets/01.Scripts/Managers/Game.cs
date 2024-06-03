using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputStateRecord
{
    public float timestamp;
    public KeyCode keyPressed;
    public GameState gameState;
}

[System.Serializable]
public class GameState
{
    public Vector3 playerPosition;
    public Quaternion playerRotation;
    public List<ObjectState> objectStates;
}

[System.Serializable]
public class ObjectState
{
    public Vector3 position;
    public Quaternion rotation;
    public bool isActive;
}
