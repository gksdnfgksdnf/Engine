using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoSingleton<GridManager>
{
    public int x, y, z;
    [SerializeField] private Dictionary<Vector3Int, Object> objectPositions = new Dictionary<Vector3Int, Object>();
    private Vector3Int playerPosition;

    protected override void Awake()
    {
        base.Awake();
        InitializeGrid();
    }

    private void InitializeGrid()
    {
        objectPositions.Clear();
        playerPosition = Vector3Int.zero;
    }

    public void SetPlayer(Vector3Int pos)
    {
        if (IsValidPosition(pos))
        {
            playerPosition = pos;
        }
    }

    public void SetObject(Vector3Int newPos, Object obj, Vector3Int oldPos = default(Vector3Int))
    {
        if (IsValidPosition(newPos))
        {
            if (oldPos != default(Vector3Int) && objectPositions.ContainsKey(oldPos))
            {
                Debug.Log("뭔가 있으니 ㅣ지운다!!!!!!");
                objectPositions.Remove(oldPos);
            }

            objectPositions[newPos] = obj;
        }
    }

    public bool IsObjectAtPosition(Vector3 pos)
    {
        return objectPositions.ContainsKey(Vector3Int.RoundToInt(pos));
    }

    public Object GetObjectAtPosition(Vector3 position)
    {
        objectPositions.TryGetValue(Vector3Int.RoundToInt(position), out Object obj);
        return obj;
    }

    public Vector3Int GetPlayerPosition()
    {
        return playerPosition;
    }

    private bool IsValidPosition(Vector3Int pos)
    {
        return pos.x >= 0 && pos.x <= x &&
               pos.y >= 0 && pos.y <= y &&
               pos.z >= 0 && pos.z <= z;
    }
}

