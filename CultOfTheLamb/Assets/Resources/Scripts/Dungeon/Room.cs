using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public byte my_flag = 0b0000;
    ///<summary> 0b0001 Left</summary>
    private byte option_0 = 1 << 0;
    ///<summary> 0b0010 Top</summary>
    private byte option_1 = 1 << 1;
    ///<summary> 0b0100 Right</summary>
    private byte option_2 = 1 << 2;
    ///<summary> 0b1000 Bottom</summary>
    private byte option_3 = 1 << 3;

    public RoomType roomType;
    public int x = default;
    public int y = default;

    #region Inspector
    public bool IsRoomClear = default;
    [Space(5)]
    public List<GameObject> worldTriggerZone = default;

    [Space(5)]
    public List<GameObject> worldSpawnZone = default;

    [Space(5)]
    public List<GameObject> walls = default;

    [Space(5)]
    public List<GameObject> floors = default;

    [Space(5)]
    public List<Transform> worldSizeCheck = default;
    public Vector2 worldSize = default;
    #endregion

    public void OnEnable()
    {
        float minX, minY, maxX, maxY;

        minX = worldSizeCheck[0].localPosition.x;
        minY = worldSizeCheck[0].localPosition.z;

        maxX = worldSizeCheck[1].localPosition.x;
        maxY = worldSizeCheck[1].localPosition.z;

        float width, height;

        if (minX < 0) minX *= -1;
        if (minY < 0) minY *= -1;

        width = minX + maxX;
        height = minY + maxY;

        worldSize = new Vector2(width, height);

        AStarManager.Instance.aGrid.gridWorldSize = worldSize;
        GameManager.Instance.SetWorldSize();
        GameManager.Instance.SetCurrentRoom(this);
        AStarManager.Instance.aGrid.CreateGrid();
    }

    public void RoomCreate(byte flag)
    {
        foreach (var iterator in worldTriggerZone)
        {
            iterator.SetActive(false);
        }
        foreach (var iterator in worldSpawnZone)
        {
            iterator.SetActive(false);
        }
        foreach (var iterator in floors)
        {
            iterator.SetActive(false);
        }
        foreach (var iterator in walls)
        {
            iterator.SetActive(true);
        }

        // 왼쪽에 방이 있다면
        if ((flag & option_0) != 0b0000)
        {
            RoomInit(0);
        }

        // 위쪽에 방이 있다면
        if ((flag & option_1) != 0b0000)
        {
            RoomInit(1);
        }
        // 오른쪽에 방이 있다면
        if ((flag & option_2) != 0b0000)
        {
            RoomInit(2);
        }

        // 아래쪽에 방이 있다면
        if ((flag & option_3) != 0b0000)
        {
            RoomInit(3);
        }

        my_flag |= flag;
    }
    public void RoomInit(int num)
    {
        worldTriggerZone[num].SetActive(true);
        worldSpawnZone[num].SetActive(true);
        floors[num].SetActive(true);
        walls[num].SetActive(true);
    }

    public void RoomClear()
    {
        if ((my_flag & option_0) != 0b0000)
        {
            walls[0].SetActive(false);
        }
        if ((my_flag & option_1) != 0b0000)
        {
            walls[1].SetActive(false);
        }
        if ((my_flag & option_2) != 0b0000)
        {
            walls[2].SetActive(false);
        }
        if ((my_flag & option_3) != 0b0000)
        {
            walls[3].SetActive(false);
        }
    }

    public void RoomWallOn()
    {
        if ((my_flag & option_0) != 0b0000)
        {
            walls[0].SetActive(true);
        }
        if ((my_flag & option_1) != 0b0000)
        {
            walls[1].SetActive(true);
        }
        if ((my_flag & option_2) != 0b0000)
        {
            walls[2].SetActive(true);
        }
        if ((my_flag & option_3) != 0b0000)
        {
            walls[3].SetActive(true);
        }
    }

    public void RoomWallOff()
    {
        if ((my_flag & option_0) != 0b0000)
        {
            walls[0].SetActive(false);
        }
        if ((my_flag & option_1) != 0b0000)
        {
            walls[1].SetActive(false);
        }
        if ((my_flag & option_2) != 0b0000)
        {
            walls[2].SetActive(false);
        }
        if ((my_flag & option_3) != 0b0000)
        {
            walls[3].SetActive(false);
        }
    }
}
