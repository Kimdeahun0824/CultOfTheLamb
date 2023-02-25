using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RoomType
{
    NONE, START, NORMAL, BOSS, TAROT, ENDROOM
}
public class RoomGenerator : MonoBehaviour
{
    public RoomNode[,] rooms = default;
    public GameObject mapPrefab;
    public Transform parent;
    int count;
    int tempCount;
    int cost;

    void Start()
    {
        rooms = new RoomNode[5, 5];
        count = 0;
        tempCount = 0;
        cost = 0;
        RoomCreate();

        StartCoroutine(RoomDeployment());
    }

    IEnumerator RoomDeployment()
    {
        for (int i = 0; i < rooms.GetLength(0); i++)
        {
            for (int j = 0; j < rooms.GetLength(1); j++)
            {
                if (rooms[i, j].currentRoomType != RoomType.NONE)
                {
                    rooms[i, j].X = i;
                    rooms[i, j].Y = j;
                    //Instantiate(mapPrefab, new Vector3(AStarManager.Instance.aGrid.gridWorldSize.x * i, 0.1f, AStarManager.Instance.aGrid.gridWorldSize.y * j), Quaternion.identity, parent);
                    //roomsObj[i, j] = Instantiate(mapPrefab, new Vector3(AStarManager.Instance.aGrid.gridWorldSize.x * i, 0.1f, AStarManager.Instance.aGrid.gridWorldSize.y * j), Quaternion.identity, parent);     
                    Instantiate(mapPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity, parent);
                    yield return null;
                }
                Debug.Log($"room Status i : {i} / j : {j} / roomType : {rooms[i, j].currentRoomType} / roomCost : {rooms[i, j].Cost}");
            }
        }

    }

    public void RoomCreate()
    {
        count = 0;
        tempCount = 0;
        cost = 0;
        for (int i = 0; i < rooms.GetLength(0); i++)
        {
            for (int j = 0; j < rooms.GetLength(1); j++)
            {
                rooms[i, j] = new RoomNode();
                rooms[i, j].currentRoomType = RoomType.NONE;
            }
        }

        int x = Random.Range(0, 5);
        int y = Random.Range(0, 5);
        rooms[x, y].currentRoomType = RoomType.START;

        RandomRoomCreate(x, y);
        RoomDFSFind(x, y, x, y);

        for (int i = 0; i < rooms.GetLength(0); i++)
        {
            for (int j = 0; j < rooms.GetLength(1); j++)
            {
                if (rooms[i, j].currentRoomType != RoomType.NONE)
                {
                    tempCount++;
                }
            }
        }

        if (tempCount < 9)
        {
            RoomCreate();
        }
    }

    public void RandomRoomCreate(int x, int y)
    {
        if (8 <= count) return;

        int index = 0;
        index = NodeNeighborCheck(x, y);
        if (2 < index)
        {
            return;
        }

        if (y + 1 <= rooms.GetLength(1) - 1)
        {
            if (NodeNeighborCheck(x, y + 1) < 2 && count < 8)
            {
                if (rooms[x, y + 1].currentRoomType == RoomType.NONE)
                {
                    int randNum = Random.Range(0, 2);
                    if (randNum == 1)
                    {
                        rooms[x, y + 1].currentRoomType = RoomType.NORMAL;
                        count++;
                        Debug.Log($"RoomCreate UP : i : {x} / j : {y + 1} / Count : {count}");
                        RandomRoomCreate(x, y + 1);
                    }
                }
            }
        }
        if (x + 1 <= rooms.GetLength(0) - 1)
        {
            if (NodeNeighborCheck(x + 1, y) < 2 && count < 8)
            {
                if (rooms[x + 1, y].currentRoomType == RoomType.NONE)
                {
                    int randNum = Random.Range(0, 2);
                    if (randNum == 1)
                    {
                        rooms[x + 1, y].currentRoomType = RoomType.NORMAL;
                        count++;
                        Debug.Log($"RoomCreate RIGHT : i : {x + 1} / j : {y} / Count : {count}");
                        RandomRoomCreate(x + 1, y);
                    }
                }
            }
        }
        if (0 <= x - 1)
        {
            if (NodeNeighborCheck(x - 1, y) < 2 && count < 8)
            {
                if (rooms[x - 1, y].currentRoomType == RoomType.NONE)
                {
                    int randNum = Random.Range(0, 2);
                    if (randNum == 1)
                    {
                        rooms[x - 1, y].currentRoomType = RoomType.NORMAL;
                        count++;
                        Debug.Log($"RoomCreate LEFT : i : {x - 1} / j : {y} / Count : {count}");
                        RandomRoomCreate(x - 1, y);
                    }
                }
            }
        }
        if (0 <= y - 1)
        {
            if (NodeNeighborCheck(x, y - 1) < 2 && count < 8)
            {
                if (rooms[x, y - 1].currentRoomType == RoomType.NONE)
                {
                    int randNum = Random.Range(0, 2);
                    if (randNum == 1)
                    {
                        rooms[x, y - 1].currentRoomType = RoomType.NORMAL;
                        count++;
                        Debug.Log($"RoomCreate DOWN : i : {x} / j : {y - 1} / Count : {count}");
                        RandomRoomCreate(x, y - 1);
                    }
                }
            }
        }
    }

    public int NodeNeighborCheck(int x, int y)
    {
        int index = 0;
        if (y + 1 <= rooms.GetLength(1) - 1)
        {
            if (rooms[x, y + 1].currentRoomType != RoomType.NONE)
            {
                index++;
            }
        }
        if (x + 1 <= rooms.GetLength(0) - 1)
        {
            if (rooms[x + 1, y].currentRoomType != RoomType.NONE)
            {
                index++;
            }
        }
        if (0 <= x - 1)
        {
            if (rooms[x - 1, y].currentRoomType != RoomType.NONE)
            {
                index++;
            }
        }
        if (0 <= y - 1)
        {
            if (rooms[x, y - 1].currentRoomType != RoomType.NONE)
            {
                index++;
            }
        }

        return index;
    }

    public void RoomDFSFind(int x, int y, int previousX, int previousY)
    {
        rooms[x, y].IsChecked = true;
        rooms[x, y].Cost = rooms[previousX, previousY].Cost + 1;
        bool endCheck = true;

        if (0 <= x - 1)
        {
            if (rooms[x - 1, y].currentRoomType != RoomType.NONE && !rooms[x - 1, y].IsChecked)
            {
                endCheck = false;
                RoomDFSFind(x - 1, y, x, y);
            }
        }

        if (y + 1 <= 4)
        {

            if (rooms[x, y + 1].currentRoomType != RoomType.NONE && !rooms[x, y + 1].IsChecked)
            {
                endCheck = false;
                RoomDFSFind(x, y + 1, x, y);
            }
        }

        if (x + 1 <= 4)
        {

            if (rooms[x + 1, y].currentRoomType != RoomType.NONE && !rooms[x + 1, y].IsChecked)
            {
                endCheck = false;
                RoomDFSFind(x + 1, y, x, y);
            }
        }

        if (0 <= y - 1)
        {
            if (rooms[x, y - 1].currentRoomType != RoomType.NONE && !rooms[x, y - 1].IsChecked)
            {
                endCheck = false;
                RoomDFSFind(x, y - 1, x, y);
            }
        }

        if (endCheck)
        {
            rooms[x, y].currentRoomType = RoomType.ENDROOM;
        }
    }
}

public class RoomNode
{
    public RoomType currentRoomType = RoomType.NONE;
    public int X = 0, Y = 0;
    public int Cost = 0;
    public bool IsChecked = default;
}


