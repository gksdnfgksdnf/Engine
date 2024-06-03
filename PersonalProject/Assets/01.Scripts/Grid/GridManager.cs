using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int x, y, z;

    public Vector3Int[,,] grid = new Vector3Int[10, 10, 10];

    public int[] GetGrid(Vector3Int pos)
    {
        for (int x = 0; x < this.x; x++)
        {
            for (int y = 0; y < this.y; y++)
            {
                for (int z = 0; z < this.z; z++)
                {
                    if (grid[x, y, z] == pos)
                    {
                        return new int[3] { x, y, z };
                    }
                }
            }
        }
        return new int[0];
    }


}
