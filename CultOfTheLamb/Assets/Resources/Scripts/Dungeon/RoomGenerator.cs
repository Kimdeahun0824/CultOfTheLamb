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

    ///<summary> 0b0001 Left</summary>
    byte option_0 = 1 << 0;
    ///<summary> 0b0010 Top</summary>
    byte option_1 = 1 << 1;
    ///<summary> 0b0100 Right</summary>
    byte option_2 = 1 << 2;
    ///<summary> 0b1000 Bottom</summary>
    byte option_3 = 1 << 3;

    [Space(5)]
    public Transform parent;
    public GameObject startRoomPrefab;
    public GameObject normalRoomPrefab;
    public GameObject bossRoomPrefab;

    public GameObject mapPrefab;

    public List<RoomNode> endRooms;

    public List<GameObject> List_CreateRooms = default;

    int count;
    int tempCount;
    int cost;

    void Start()
    {
        List_CreateRooms = new List<GameObject>();
        endRooms = new List<RoomNode>();
        rooms = new RoomNode[5, 5];
        count = 0;
        tempCount = 0;
        cost = 0;
        RoomCreate();

        RoomDeployment();
        GameManager.Instance.SetRoom(List_CreateRooms);
    }

    ///<summary>
    ///방을 만드는 함수
    ///</summary>
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

    ///<summary>랜덤으로 방을 만드는 함수summary>
    ///<param name="x">새롭게 만들 방의 X 좌표</param>
    ///<param name="y">새롭게 만들 방의 Y 좌표</param>
    public void RandomRoomCreate(int x, int y)
    {
        if (8 <= count) return;
        byte flag = NodeNeighborByteCheck(x, y);
        if (bool_NodeNeighborCheck(flag))
        {
            return;
        }

        if (0 <= x - 1)
        {
            flag = NodeNeighborByteCheck(x - 1, y);
            RandomRoomCreate(x - 1, y, flag, option_0);
        }

        if (y + 1 <= rooms.GetLength(1) - 1)
        {
            flag = NodeNeighborByteCheck(x, y + 1);
            RandomRoomCreate(x, y + 1, flag, option_1);
        }

        if (x + 1 <= rooms.GetLength(0) - 1)
        {
            flag = NodeNeighborByteCheck(x + 1, y);
            RandomRoomCreate(x + 1, y, flag, option_2);
        }

        if (0 <= y - 1)
        {
            flag = NodeNeighborByteCheck(x, y - 1);
            RandomRoomCreate(x, y - 1, flag, option_3);
        }
    }

    public void RandomRoomCreate(int x, int y, byte flag, byte option)
    {
        int randNum = Random.Range(0, 2);
        if (randNum == 0) return;

        if ((flag & option) == 0 && count < 8 && !bool_NodeNeighborCheck(flag))
        {
            rooms[x, y].currentRoomType = RoomType.NORMAL;
            count++;
            RandomRoomCreate(x, y);
        }
    }

    ///<summary>
    /// 방을 배치하는 함수
    ///</summary>
    void RoomDeployment()
    {
        for (int i = 0; i < rooms.GetLength(0); i++)
        {
            for (int j = 0; j < rooms.GetLength(1); j++)
            {
                if (rooms[i, j].currentRoomType == RoomType.NORMAL)
                {
                    RoomNode tempRoomNode = rooms[i, j];
                    int x = i;
                    int y = j;
                    RoomDeployment(tempRoomNode, x, y);
                }
                else if (rooms[i, j].currentRoomType == RoomType.START)
                {
                    RoomNode tempRoomNode = rooms[i, j];
                    int x = i;
                    int y = j;
                    GameObject StartRoom = RoomDeployment(tempRoomNode, x, y);
                    StartRoom.SetActive(true);
                    StartRoom.GetComponent<Room>().IsRoomClear = true;
                    StartRoom.GetComponent<Room>().RoomClear();
                }
                else if (rooms[i, j].currentRoomType == RoomType.ENDROOM)
                {
                    rooms[i, j].X = i;
                    rooms[i, j].Y = j;
                    endRooms.Add(rooms[i, j]);
                }
            }
        }
        RoomNode[] endRoomArray;
        int maxRoomCount = 0;
        int maxRoomCost = 0;

        //EndRoom이 1개밖에 없을 때
        if (endRooms.Count < 2)
        {
            RoomNode tempRoomNode = endRooms[0];
            int x = endRooms[0].X;
            int y = endRooms[0].Y;
            tempRoomNode.currentRoomType = RoomType.BOSS;
            RoomDeployment(tempRoomNode, x, y);
        }
        else    // EneRoom이 2개 이상 있을 때 그 중 가장 먼 곳에 있는 Room을 보스방으로 지정 , 같은 거리에 있는 방이 2개 이상 있을 시 그 중 1개를 랜덤으로 보스방으로 지정
        {
            foreach (var obj in endRooms)
            {
                if (maxRoomCost < obj.Cost)
                {
                    maxRoomCost = obj.Cost;
                }
            }
            foreach (var obj in endRooms)
            {
                if (maxRoomCost == obj.Cost)
                {
                    maxRoomCount++;
                }
            }

            // 같은 거리에 있는 방이 2개 이상 있을 시
            if (1 < maxRoomCount)
            {
                int endRoomArrayIndex = 0;
                endRoomArray = new RoomNode[maxRoomCount];
                for (int i = 0; i < endRooms.Count; i++)
                {
                    if (maxRoomCost == endRooms[i].Cost)
                    {
                        endRoomArray[endRoomArrayIndex] = endRooms[i];
                        endRoomArrayIndex++;
                    }
                    else
                    {
                        RoomNode tempRoomNode = endRooms[i];
                        int x = endRooms[i].X;
                        int y = endRooms[i].Y;
                        RoomDeployment(tempRoomNode, x, y);
                    }
                }
                int bossRoomIndex = Random.Range(0, endRoomArray.Length);
                for (int i = 0; i < endRoomArray.Length; i++)
                {
                    if (i == bossRoomIndex)
                    {
                        RoomNode tempRoomNode = endRoomArray[i];
                        int x = endRoomArray[i].X;
                        int y = endRoomArray[i].Y;
                        tempRoomNode.currentRoomType = RoomType.BOSS;
                        RoomDeployment(tempRoomNode, x, y);
                    }
                    else
                    {
                        RoomNode tempRoomNode = endRoomArray[i];
                        int x = endRoomArray[i].X;
                        int y = endRoomArray[i].Y;
                        RoomDeployment(tempRoomNode, x, y);
                    }
                }
            }
            else    // 같은 거리에 있는 방이 없을 시
            {
                foreach (var obj in endRooms)
                {
                    if (maxRoomCost == obj.Cost)
                    {
                        obj.currentRoomType = RoomType.BOSS;
                        RoomDeployment(obj, obj.X, obj.Y);
                    }
                    else
                    {
                        RoomDeployment(obj, obj.X, obj.Y);
                    }
                }
            }
        }
    }

    // 상 하 좌 우 방 존재에 따라서 각기 다른 맵 생성
    public GameObject RoomDeployment(RoomNode roomNode, int x, int y)
    {
        roomNode.X = x;
        roomNode.Y = y;
        byte flag = NodeNeighborByteCheck(x, y);

        GameObject tempRoom = default;
        GameObject tempPrefab = default;
        switch (roomNode.currentRoomType)
        {
            case RoomType.NONE:
                break;
            case RoomType.NORMAL:
                tempPrefab = mapPrefab;
                break;
            case RoomType.ENDROOM:
                tempPrefab = mapPrefab;
                break;
            case RoomType.BOSS:
                tempPrefab = mapPrefab;
                break;
            case RoomType.TAROT:
                break;
            default:
                break;
        }
        tempRoom = Instantiate(mapPrefab, new Vector3(0f, 0.1f, 0f), Quaternion.identity, parent);
        tempRoom.GetComponent<Room>().RoomCreate(flag);
        tempRoom.GetComponent<Room>().x = x;
        tempRoom.GetComponent<Room>().y = y;
        tempRoom.GetComponent<Room>().roomType = roomNode.currentRoomType;
        tempRoom.SetActive(false);
        List_CreateRooms.Add(tempRoom);
        return tempRoom;
    }

    ///<summary>입력 한 방의 인접 방이 있는지 체크해주는 함수</summary>
    ///<param name ="x">확인할 방의 X좌표</param>
    ///<param name ="y">확인할 방의 Y좌표</param>
    public byte NodeNeighborByteCheck(int x, int y)
    {
        byte result = 0b0000;
        // Left
        if (0 <= x - 1)
        {
            if (rooms[x - 1, y].currentRoomType != RoomType.NONE)
            {
                result |= option_0;
            }
        }
        // Top
        if (y + 1 <= rooms.GetLength(1) - 1)
        {
            if (rooms[x, y + 1].currentRoomType != RoomType.NONE)
            {
                result |= option_1;
            }
        }
        // Right
        if (x + 1 <= rooms.GetLength(0) - 1)
        {
            if (rooms[x + 1, y].currentRoomType != RoomType.NONE)
            {
                result |= option_2;
            }
        }
        // Bottom
        if (0 <= y - 1)
        {
            if (rooms[x, y - 1].currentRoomType != RoomType.NONE)
            {
                result |= option_3;
            }
        }
        return result;
    }

    public bool bool_NodeNeighborCheck(byte flag)
    {
        int count = 0;

        while (0 < flag)
        {
            if ((flag & 1) == 1)
            {
                count++;
            }
            flag >>= 1;
            if (1 < count)
            {
                return true;
            }
        }

        return false;
    }

    public int int_NodeNeighborCheck(byte flag)
    {
        int count = 0;

        while (flag > 0)
        {
            if ((flag & 1) == 1)
            {
                count++;
            }
            flag >>= 1;
        }

        return count;
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