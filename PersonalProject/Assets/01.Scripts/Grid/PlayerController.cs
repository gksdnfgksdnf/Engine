using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GridManager2 gridManager;
    private int playerX = 1;
    private int playerY = 1;
    private int playerZ = 1;

    void Start()
    {
        gridManager.PlaceObjectInGrid(this.gameObject, playerX, playerY, playerZ);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W)) MovePlayer(0, 0, 1);
        if (Input.GetKeyDown(KeyCode.S)) MovePlayer(0, 0, -1);
        if (Input.GetKeyDown(KeyCode.A)) MovePlayer(-1, 0, 0);
        if (Input.GetKeyDown(KeyCode.D)) MovePlayer(1, 0, 0);
    }

    void MovePlayer(int deltaX, int deltaY, int deltaZ)
    {
        int newX = playerX + deltaX;
        int newY = playerY + deltaY;
        int newZ = playerZ + deltaZ;

        if (CanMoveTo(newX, newY, newZ))
        {
            GameObject objAtNewPos = gridManager.GetObjectAtGridPosition(newX, newY, newZ);
            if (objAtNewPos != null)
            {
                PushObject(objAtNewPos, deltaX, deltaY, deltaZ);
            }

            gridManager.MoveObjectInGrid(gameObject, newX, newY, newZ);
            playerX = newX;
            playerY = newY;
            playerZ = newZ;
            transform.position = gridManager.GetWorldPosition(playerX, playerY, playerZ);
        }
    }

    void PushObject(GameObject obj, int deltaX, int deltaY, int deltaZ)
    {
        int objX = playerX + deltaX;
        int objY = playerY + deltaY;
        int objZ = playerZ + deltaZ;

        int newObjX = objX + deltaX;
        int newObjY = objY + deltaY;
        int newObjZ = objZ + deltaZ;

        if (CanMoveTo(newObjX, newObjY, newObjZ))
        {
            GameObject nextObj = gridManager.GetObjectAtGridPosition(newObjX, newObjY, newObjZ);
            if (nextObj != null)
            {
                PushObject(nextObj, deltaX, deltaY, deltaZ);
            }
            gridManager.MoveObjectInGrid(obj, newObjX, newObjY, newObjZ);
        }
    }

    bool CanMoveTo(int x, int y, int z)
    {
        return x >= 0 && y >= 0 && z >= 0 && x < gridManager.gridSizeX && y < gridManager.gridSizeY && z < gridManager.gridSizeZ;
    }
}
