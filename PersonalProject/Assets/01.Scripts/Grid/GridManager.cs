using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoSingleton<GridManager>
{
    public int x, y, z;
    private Dictionary<Vector3Int, Object> objectPositions = new Dictionary<Vector3Int, Object>();
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

    public void SetObject(Vector3Int pos, Object obj)
    {
        if (IsValidPosition(pos))
        {
            objectPositions[pos] = obj;
        }
    }

    public bool IsObjectAtPosition(Vector3Int pos)
    {
        return objectPositions.ContainsKey(pos);
    }

    public Object GetObjectAtPosition(Vector3Int position)
    {
        objectPositions.TryGetValue(position, out Object obj);
        return obj;
    }

    public Vector3Int GetPlayerPosition()
    {
        return playerPosition;
    }

    private bool IsValidPosition(Vector3Int pos)
    {
        return pos.x >= 0 && pos.x < x &&
               pos.y >= 0 && pos.y < y &&
               pos.z >= 0 && pos.z < z;
    }
}
