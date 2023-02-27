using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{

    public Vector3 startPos = default;
    public float minX = default;
    public float minY = default;
    public float maxX = default;
    public float maxY = default;


    private new void Awake()
    {
        base.Awake();
        ConvenienceFunc.SetOnSceneLoaded(OnSceneLoaded);
    }

    public void SetWorldSize()
    {
        AGrid grid = AStarManager.Instance.GetComponent<AGrid>();
        minX = grid.gridWorldSize.x * 0.5f * -1;
        maxX = grid.gridWorldSize.x * 0.5f;
        minY = grid.gridWorldSize.y * 0.5f * -1;
        maxY = grid.gridWorldSize.y * 0.5f;

        Debug.Log($"minX : {minX}");
        Debug.Log($"minY : {minY}");
        Debug.Log($"maxX : {maxX}");
        Debug.Log($"maxY : {maxY}");
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "00. InitScene":
                ConvenienceFunc.LoadScene("01. TitleScene");
                break;
            case "01. TitleScene":
                ConvenienceFunc.LoadScene("02. StageScene");
                break;
            default:
                break;
        }
    }

    public List<GameObject> List_CreateRooms = new List<GameObject>();
    public void SetRoom(List<GameObject> rooms)
    {
        this.List_CreateRooms = rooms;

        foreach (var room in this.List_CreateRooms)
        {
            Debug.Log($"SetRoom Debug(room : {room} / {room.GetComponent<Room>()} / x : {room.GetComponent<Room>().x} / y : {room.GetComponent<Room>().y})");
        }
    }

    public Vector3 RoomChangeLeft(int x, int y)
    {
        Vector3 result = default;
        Room nextRoom = default;
        foreach (var room in List_CreateRooms)
        {
            Room temp = room.GetComponent<Room>();
            if (temp.x == x && temp.y == y)
            {
                temp.gameObject.SetActive(false);
            }
            if (temp.x == x - 1 && temp.y == y)
            {
                nextRoom = temp;
                temp.gameObject.SetActive(true);
            }
        }
        result = nextRoom.worldSpwanZone[2].transform.position;
        return result;
    }

    public void RoomChangeTop(int x, int y)
    {
        foreach (var room in List_CreateRooms)
        {
            Room temp = room.GetComponent<Room>();
            if (temp.x == x && temp.y == y)
            {
                temp.gameObject.SetActive(false);
            }
            if (temp.x == x && temp.y == y + 1)
            {
                temp.gameObject.SetActive(true);
            }
        }
    }
    public void RoomChangeRight(int x, int y)
    {
        foreach (var room in List_CreateRooms)
        {
            Room temp = room.GetComponent<Room>();
            if (temp.x == x && temp.y == y)
            {
                temp.gameObject.SetActive(false);
            }
            if (temp.x == x + 1 && temp.y == y)
            {
                temp.gameObject.SetActive(true);
            }
        }
    }
    public void RoomChangeBottom(int x, int y)
    {
        foreach (var room in List_CreateRooms)
        {
            Room temp = room.GetComponent<Room>();
            if (temp.x == x && temp.y == y)
            {
                temp.gameObject.SetActive(false);
            }
            if (temp.x == x && temp.y == y - 1)
            {
                temp.gameObject.SetActive(true);
            }
        }
    }

}
