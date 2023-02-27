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

    public int x = default;
    public int y = default;

    #region Inspector
    public bool IsRoomClear = default;
    [Space(5)]
    public List<GameObject> worldTriggerZone = default;

    [Space(5)]
    public List<GameObject> worldSpwanZone = default;

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
        AStarManager.Instance.aGrid.CreateGrid();
    }

    public void RoomCreate(byte flag)
    {
        foreach (var iterator in worldTriggerZone)
        {
            iterator.SetActive(false);
        }
        foreach (var iterator in worldSpwanZone)
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
            worldTriggerZone[0].SetActive(true);
            worldSpwanZone[0].SetActive(true);
            floors[0].SetActive(true);
            walls[0].SetActive(true);
        }
        Debug.Log($"flag & Option_0 : {flag & option_0}");

        // 위쪽에 방이 있다면
        if ((flag & option_1) != 0b0000)
        {
            worldTriggerZone[1].SetActive(true);
            worldSpwanZone[1].SetActive(true);
            floors[1].SetActive(true);
            walls[1].SetActive(true);
        }
        Debug.Log($"flag & Option_1 : {flag & option_1}");
        // 오른쪽에 방이 있다면
        if ((flag & option_2) != 0b0000)
        {
            worldTriggerZone[2].SetActive(true);
            worldSpwanZone[2].SetActive(true);
            floors[2].SetActive(true);
            walls[2].SetActive(true);
        }
        Debug.Log($"flag & Option_2 : {flag & option_2}");

        // 아래쪽에 방이 있다면
        if ((flag & option_3) != 0b0000)
        {
            worldTriggerZone[3].SetActive(true);
            worldSpwanZone[3].SetActive(true);
            floors[3].SetActive(true);
            walls[3].SetActive(true);
        }
        Debug.Log($"flag & Option_3 : {flag & option_3}");

        my_flag |= flag;
    }

    public void RoomClear()
    {
        Debug.Log($"RoomClear my_flag : {my_flag}");
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Room OnTriggerEnter Debug(other.name : {other.name})");
    }
}
