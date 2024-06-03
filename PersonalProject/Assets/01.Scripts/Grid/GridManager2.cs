using UnityEngine;

public class GridManager2 : MonoBehaviour
{
    public int gridSizeX = 10;
    public int gridSizeY = 10;
    public int gridSizeZ = 10;
    public float cellSize = 1.0f;


    private GameObject[,,] gridArray;

    void Start()
    {
        InitializeGrid();
    }

    void InitializeGrid()
    {
        gridArray = new GameObject[gridSizeX , gridSizeY, gridSizeZ];
    }

    public Vector3 GetWorldPosition(int x, int y, int z)
    {
        return new Vector3(x, y, z) * cellSize;
    }

    public void PlaceObjectInGrid(GameObject obj, int x, int y, int z)
    {
        if (x >= 0 && y >= 0 && z >= 0 && x < gridSizeX && y < gridSizeY && z < gridSizeZ)
        {
            gridArray[x , y, z] = obj;
            obj.transform.position = GetWorldPosition(x, y, z);
        }
    }

    public bool MoveObjectInGrid(GameObject obj, int newX, int newY, int newZ)
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                for (int z = 0; z < gridSizeZ; z++)
                {
                    if (gridArray[x, y, z] == obj)
                    {
                        gridArray[x, y, z] = null;
                        gridArray[newX, newY, newZ] = obj;
                        obj.transform.position = GetWorldPosition(newX, newY, newZ);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public GameObject GetObjectAtGridPosition(int x, int y, int z)
    {
        if (x >= 0 && y >= 0 && z >= 0 && x < gridSizeX && y < gridSizeY && z < gridSizeZ)
        {
            return gridArray[x, y, z];
        }
        return null;
    }
}
