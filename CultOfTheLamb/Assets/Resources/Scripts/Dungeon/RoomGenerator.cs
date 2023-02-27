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

        //StartCoroutine(RoomDeployment());
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

        int index = 0;
        index = NodeNeighborCheck(x, y);
        if (2 < index)
        {
            return;
        }
        byte flag = NodeNeighborByteCheck(x, y);
        if ((flag & option_0) == 0 && count < 8 && 0 <= x - 1)
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
        if ((flag & option_1) == 0 && count < 8 && y + 1 <= rooms.GetLength(1) - 1)
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
        if ((flag & option_2) == 0 && count < 8 && x + 1 <= rooms.GetLength(0) - 1)
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
        if ((flag & option_3) == 0 && count < 8 && 0 <= y - 1)
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
                    rooms[i, j].X = i;
                    rooms[i, j].Y = j;
                    byte flag = NodeNeighborByteCheck(i, j);
                    // 상 하 좌 우 방 존재에 따라서 각기 다른 맵 생성
                    GameObject tempRoom = Instantiate(mapPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity, parent);
                    tempRoom.GetComponent<Room>().RoomCreate(flag);
                    tempRoom.GetComponent<Room>().x = i;
                    tempRoom.GetComponent<Room>().y = j;
                    tempRoom.SetActive(false);
                    List_CreateRooms.Add(tempRoom);
                }
                else if (rooms[i, j].currentRoomType == RoomType.START)
                {
                    rooms[i, j].X = i;
                    rooms[i, j].Y = j;
                    byte flag = NodeNeighborByteCheck(i, j);
                    GameObject tempRoom = Instantiate(mapPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity, parent);
                    tempRoom.GetComponent<Room>().RoomCreate(flag);
                    tempRoom.GetComponent<Room>().RoomClear();
                    tempRoom.GetComponent<Room>().x = i;
                    tempRoom.GetComponent<Room>().y = j;
                    tempRoom.SetActive(true);
                    List_CreateRooms.Add(tempRoom);
                }
                else if (rooms[i, j].currentRoomType == RoomType.ENDROOM)
                {
                    rooms[i, j].X = i;
                    rooms[i, j].Y = j;
                    endRooms.Add(rooms[i, j]);
                }
                //yield return null;
                Debug.Log($"room Status i : {i} / j : {j} / roomType : {rooms[i, j].currentRoomType} / roomCost : {rooms[i, j].Cost}");
            }
        }
        RoomNode[] endRoomArray;
        int maxRoomCount = 0;
        int maxRoomCost = 0;
        //EndRoom이 1개밖에 없을 때
        if (endRooms.Count < 2)
        {
            byte flag = NodeNeighborByteCheck(endRooms[0].X, endRooms[0].Y);
            GameObject tempRoom = Instantiate(mapPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity, parent);
            tempRoom.GetComponent<Room>().RoomCreate(flag);
            tempRoom.GetComponent<Room>().x = endRooms[0].X;
            tempRoom.GetComponent<Room>().y = endRooms[0].Y;
            tempRoom.SetActive(false);
            List_CreateRooms.Add(tempRoom);
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
            Debug.Log($"Map Random Generator Debug : maxRoomCost : {maxRoomCost} / maxRoomCount : {maxRoomCount}");
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
                        byte flag = NodeNeighborByteCheck(endRooms[i].X, endRooms[i].Y);
                        GameObject tempRoom = Instantiate(mapPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity, parent);
                        tempRoom.GetComponent<Room>().RoomCreate(flag);
                        tempRoom.GetComponent<Room>().x = endRooms[i].X;
                        tempRoom.GetComponent<Room>().y = endRooms[i].Y;
                        tempRoom.SetActive(false);
                        List_CreateRooms.Add(tempRoom);
                    }
                }
                int bossRoomIndex = Random.Range(0, endRoomArray.Length);
                Debug.Log($"Map Random Generator Debug : bossRoomIndex {bossRoomIndex} / endRoomArray.Length : {endRoomArray.Length}");
                for (int i = 0; i < endRoomArray.Length; i++)
                {
                    if (i == bossRoomIndex)
                    {
                        byte flag = NodeNeighborByteCheck(endRoomArray[i].X, endRoomArray[i].Y);
                        GameObject tempRoom = Instantiate(mapPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity, parent);
                        tempRoom.GetComponent<Room>().RoomCreate(flag);
                        tempRoom.GetComponent<Room>().x = endRoomArray[i].X;
                        tempRoom.GetComponent<Room>().y = endRoomArray[i].Y;
                        tempRoom.SetActive(false);
                        List_CreateRooms.Add(tempRoom);
                    }
                    else
                    {
                        byte flag = NodeNeighborByteCheck(endRoomArray[i].X, endRoomArray[i].Y);
                        GameObject tempRoom = Instantiate(mapPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity, parent);
                        tempRoom.GetComponent<Room>().RoomCreate(flag);
                        tempRoom.GetComponent<Room>().x = endRoomArray[i].X;
                        tempRoom.GetComponent<Room>().y = endRoomArray[i].Y;
                        tempRoom.SetActive(false);
                        List_CreateRooms.Add(tempRoom);
                    }
                }
            }
            else    // 같은 거리에 있는 방이 없을 시
            {
                foreach (var obj in endRooms)
                {
                    if (maxRoomCost == obj.Cost)
                    {
                        byte flag = NodeNeighborByteCheck(obj.X, obj.Y);
                        GameObject tempRoom = Instantiate(mapPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity, parent);
                        tempRoom.GetComponent<Room>().RoomCreate(flag);
                        tempRoom.GetComponent<Room>().x = obj.X;
                        tempRoom.GetComponent<Room>().y = obj.Y;
                        tempRoom.SetActive(false);
                        List_CreateRooms.Add(tempRoom);
                    }
                    else
                    {
                        byte flag = NodeNeighborByteCheck(obj.X, obj.Y);
                        GameObject tempRoom = Instantiate(mapPrefab, new Vector3(0, 0.1f, 0), Quaternion.identity, parent);
                        tempRoom.GetComponent<Room>().RoomCreate(flag);
                        tempRoom.GetComponent<Room>().x = obj.X;
                        tempRoom.GetComponent<Room>().y = obj.Y;
                        tempRoom.SetActive(false);
                        List_CreateRooms.Add(tempRoom);
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


