using System.Collections.Generic;
using UnityEngine;

public class InputStateRecorder : MonoBehaviour
{
    public GameObject player;
    public List<GameObject> gameObjects;
    private Stack<InputStateRecord> inputStateRecords = new Stack<InputStateRecord>();

    void Update()
    {
        RecordInputState();
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
                keyPressed = keyPressed,
                gameState = CaptureGameState()
            };

            inputStateRecords.Push(record);
        }
    }

    private GameState CaptureGameState()
    {
        GameState gameState = new GameState
        {
            playerPosition = player.transform.position,
            playerRotation = player.transform.rotation,
            objectStates = new List<ObjectState>()
        };

        foreach (GameObject obj in gameObjects)
        {
            ObjectState objState = new ObjectState
            {
                position = obj.transform.position,
                rotation = obj.transform.rotation,
                isActive = obj.activeSelf
            };
            gameState.objectStates.Add(objState);
        }

        return gameState;
    }

    public void Undo()
    {
        if (inputStateRecords.Count > 0)
        {
            InputStateRecord lastRecord = inputStateRecords.Pop();
            ApplyGameState(lastRecord.gameState);
        }
    }

    private void ApplyGameState(GameState gameState)
    {
        player.transform.position = gameState.playerPosition;
        player.transform.rotation = gameState.playerRotation;

        for (int i = 0; i < gameObjects.Count; i++)
        {
            gameObjects[i].transform.position = gameState.objectStates[i].position;
            gameObjects[i].transform.rotation = gameState.objectStates[i].rotation;
            gameObjects[i].SetActive(gameState.objectStates[i].isActive);
        }
    }
}
