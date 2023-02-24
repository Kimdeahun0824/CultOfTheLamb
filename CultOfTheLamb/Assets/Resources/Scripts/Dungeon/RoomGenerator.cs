using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum RoomType
{
    NONE, START, NORMAL, BOSS, TAROT
}
public class RoomGenerator : MonoBehaviour
{
    public RoomNode[,] rooms = default;
    public GameObject test;
    int count;
    int tempCount;
    void Start()
    {
        rooms = new RoomNode[5, 5];
        tempCount = 0;
        //rooms[0, 0].currentRoomType = RoomType.NONE;
        RoomCreate();


    }

    public void RoomCreate()
    {
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
        for (int i = 0; i < rooms.GetLength(0); i++)
        {
            for (int j = 0; j < rooms.GetLength(1); j++)
            {
                Debug.Log($"room Status i:{i} j:{j} : {rooms[i, j].currentRoomType}");
                if (rooms[i, j].currentRoomType != RoomType.NONE)
                {
                    Instantiate(test, new Vector3(i, 0, j), Quaternion.identity);
                    tempCount++;
                }
            }
        }
        if (tempCount < 8)
        {
            RoomCreate();
        }
    }

    public void RandomRoomCreate(int x, int y)
    {
        if (8 <= count) return;
        // int randX = Random.Range(0, 2);
        // int randY = Random.Range(0, 2);
        // if (randX == 0) randX = -1;
        // if (randY == 0) randY = -1;
        // int currentX = x + randX;
        // int currentY = y + randY;
        // if (currentX < 0 || 4 < currentX || currentY < 0 || 4 < currentY)
        // {
        //     RandomRoomCreate(x, y);
        //     return;
        // }
        // if (rooms[currentX, currentY].currentRoomType == RoomType.NONE)
        // {
        //     rooms[currentX, currentY].currentRoomType = RoomType.NORMAL;
        //     count++;
        //     RandomRoomCreate(currentX, currentY);
        // }
        // if (0 < x - 1)
        // {
        //     if (rooms[x - 1, y].currentRoomType == RoomType.NONE)
        //     {
        //         int randNum = Random.Range(0, 2);
        //         if (randNum == 1)
        //         {
        //             rooms[x - 1, y].currentRoomType = RoomType.NORMAL;
        //             RandomRoomCreate(x - 1, y);
        //             count++;
        //         }
        //     }
        // }
        // if (0 < x - 1 && y + 1 < 4)
        // {
        //     if (rooms[x - 1, y + 1].currentRoomType == RoomType.NONE)
        //     {
        //         int randNum = Random.Range(0, 2);
        //         if (randNum == 1)
        //         {
        //             rooms[x - 1, y + 1].currentRoomType = RoomType.NORMAL;
        //             RandomRoomCreate(x - 1, y + 1);
        //             count++;
        //         }
        //     }
        // }
        // if (x + 1 < 4)
        // {
        //     if (rooms[x + 1, y].currentRoomType == RoomType.NONE)
        //     {
        //         int randNum = Random.Range(0, 2);
        //         if (randNum == 1)
        //         {
        //             rooms[x + 1, y].currentRoomType = RoomType.NORMAL;
        //             RandomRoomCreate(x + 1, y);
        //             count++;
        //         }
        //     }
        // }
        // if (x + 1 < 4 && 0 < y - 1)
        // {
        //     if (rooms[x + 1, y - 1].currentRoomType == RoomType.NONE)
        //     {
        //         int randNum = Random.Range(0, 2);
        //         if (randNum == 1)
        //         {
        //             rooms[x + 1, y - 1].currentRoomType = RoomType.NORMAL;
        //             RandomRoomCreate(x + 1, y - 1);
        //             count++;
        //         }
        //     }
        // }
        int index = 0;
        if (y + 1 < 4)
        {
            if (rooms[x, y + 1].currentRoomType != RoomType.NONE)
            {
                index++;
            }
        }
        if (x + 1 < 4)
        {
            if (rooms[x + 1, y].currentRoomType != RoomType.NONE)
            {
                index++;
            }
        }
        if (0 < x - 1)
        {
            if (rooms[x - 1, y].currentRoomType != RoomType.NONE)
            {
                index++;
            }
        }
        if (0 < y - 1)
        {
            if (rooms[x, y - 1].currentRoomType != RoomType.NONE)
            {
                index++;
            }
        }
        if (2 < index)
        {
            return;
        }
        if (y + 1 <= 4)
        {
            if (rooms[x, y + 1].currentRoomType == RoomType.NONE)
            {
                int randNum = Random.Range(0, 2);
                if (randNum == 1)
                {
                    rooms[x, y + 1].currentRoomType = RoomType.NORMAL;
                    count++;
                    RandomRoomCreate(x, y + 1);
                }
            }
        }
        if (x + 1 <= 4)
        {
            if (rooms[x + 1, y].currentRoomType == RoomType.NONE)
            {
                int randNum = Random.Range(0, 2);
                if (randNum == 1)
                {
                    rooms[x + 1, y].currentRoomType = RoomType.NORMAL;
                    count++;
                    RandomRoomCreate(x + 1, y);
                }
            }
        }
        if (0 <= x - 1)
        {
            if (rooms[x - 1, y].currentRoomType == RoomType.NONE)
            {
                int randNum = Random.Range(0, 2);
                if (randNum == 1)
                {
                    rooms[x - 1, y].currentRoomType = RoomType.NORMAL;
                    count++;
                    RandomRoomCreate(x - 1, y);
                }
            }
        }
        if (0 <= y - 1)
        {
            if (rooms[x, y - 1].currentRoomType == RoomType.NONE)
            {
                int randNum = Random.Range(0, 2);
                if (randNum == 1)
                {
                    rooms[x, y - 1].currentRoomType = RoomType.NORMAL;
                    count++;
                    RandomRoomCreate(x, y - 1);
                }
            }
        }


    }
}

public class RoomNode
{
    public RoomType currentRoomType = RoomType.NONE;
}


