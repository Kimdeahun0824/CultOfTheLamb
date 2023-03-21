using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using State;

public class GameManager : SingletonBase<GameManager>
{
    [Space(5)]
    public List<GameObject> List_Enemy;
    [Space(5)]
    public List<GameObject> List_Boss;
    [Space(5)]
    public List<Enemy> currentRoomEnemy;
    [Space(5)]
    public Vector3 startPos = default;
    public float minX = default;
    public float minY = default;
    public float maxX = default;
    public float maxY = default;

    public Room previousRoom = default;
    public Room currentRoom = default;

    private Player player;
    private new void Awake()
    {
        base.Awake();
        ConvenienceFunc.SetOnSceneLoaded(OnSceneLoaded);
        currentRoomEnemy = new List<Enemy>();
        SetCursor();
    }
    private void Start()
    {

    }

    public void SetWorldSize()
    {
        AGrid grid = AStarManager.Instance.GetComponent<AGrid>();
        minX = grid.gridWorldSize.x * 0.5f * -1;
        maxX = grid.gridWorldSize.x * 0.5f;
        minY = grid.gridWorldSize.y * 0.5f * -1;
        maxY = grid.gridWorldSize.y * 0.5f;
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "00. InitScene":
                ConvenienceFunc.LoadScene("01. TitleScene");
                break;
            case "01. TitleScene":
                //ConvenienceFunc.LoadScene("02. StageScene");
                break;
            case "02. StageScene":
                player = GameObject.Find("Player").GetComponent<Player>();
                SetUimanager();
                break;
            default:
                break;
        }
    }

    public List<GameObject> List_CreateRooms = new List<GameObject>();
    public void SetRoom(List<GameObject> rooms)
    {
        this.List_CreateRooms = rooms;
    }
    public void SetCurrentRoom(Room room)
    {
        currentRoom = room;
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
                previousRoom = temp;
            }
            if (temp.x == x - 1 && temp.y == y)
            {
                nextRoom = temp;
                currentRoom = nextRoom;
                temp.gameObject.SetActive(true);
            }
        }
        nextRoom.RoomWallOff();
        result = nextRoom.worldSpawnZone[2].transform.position;
        return result;
    }

    public Vector3 RoomChangeTop(int x, int y)
    {
        Vector3 result = default;
        Room nextRoom = default;
        foreach (var room in List_CreateRooms)
        {
            Room temp = room.GetComponent<Room>();
            if (temp.x == x && temp.y == y)
            {
                temp.gameObject.SetActive(false);
                previousRoom = temp;
            }
            if (temp.x == x && temp.y == y + 1)
            {
                nextRoom = temp;
                temp.gameObject.SetActive(true);
                currentRoom = nextRoom;
            }
        }
        nextRoom.RoomWallOff();
        result = nextRoom.worldSpawnZone[3].transform.position;
        return result;
    }
    public Vector3 RoomChangeRight(int x, int y)
    {
        Vector3 result = default;
        Room nextRoom = default;
        foreach (var room in List_CreateRooms)
        {
            Room temp = room.GetComponent<Room>();
            if (temp.x == x && temp.y == y)
            {
                temp.gameObject.SetActive(false);
                previousRoom = temp;
            }
            if (temp.x == x + 1 && temp.y == y)
            {
                temp.gameObject.SetActive(true);
                nextRoom = temp;
                currentRoom = nextRoom;
            }
        }
        nextRoom.RoomWallOff();
        result = nextRoom.worldSpawnZone[0].transform.position;
        return result;
    }
    public Vector3 RoomChangeBottom(int x, int y)
    {
        Vector3 result = default;
        Room nextRoom = default;
        foreach (var room in List_CreateRooms)
        {
            Room temp = room.GetComponent<Room>();
            if (temp.x == x && temp.y == y)
            {
                temp.gameObject.SetActive(false);
                previousRoom = temp;
            }
            if (temp.x == x && temp.y == y - 1)
            {
                temp.gameObject.SetActive(true);
                nextRoom = temp;
                currentRoom = nextRoom;
            }
        }
        nextRoom.RoomWallOff();
        result = nextRoom.worldSpawnZone[1].transform.position;
        return result;
    }

    public void RoomWallOn()
    {
        currentRoom.RoomWallOn();
    }

    public void RoomWallOff()
    {
        currentRoom.RoomWallOff();
    }

    public void RoomClear()
    {
        currentRoom.RoomWallOff();
        currentRoom.IsRoomClear = true;
        currentRoomEnemy.Clear();
    }

    public void RoomMoveComplete()
    {
        if (currentRoom.IsRoomClear)
        {
            RoomWallOff();
        }
        else
        {
            RoomWallOn();
            if (currentRoom.roomType == RoomType.BOSS)
            {
                BossSpawn();
            }
            else
            {
                EnemySpawn();
            }
        }
    }

    public void EnemySpawn()
    {
        int count = Random.Range(1, 6);
        for (int i = 0; i < count; i++)
        {
            Vector3 randomPos = default;
            float x = Random.Range(minX + 5, maxX - 5);
            float z = Random.Range(minY + 5, maxY - 5);
            randomPos = new Vector3(x, 0f, z);

            int randomIndex = Random.Range(0, List_Enemy.Count);
            Enemy tempEnemy = Instantiate(List_Enemy[randomIndex], randomPos, Quaternion.identity).GetComponent<Enemy>();
            currentRoomEnemy.Add(tempEnemy);
        }
    }

    public void BossSpawn()
    {
        Enemy tempEnemy = Instantiate(List_Boss[0], Vector3.zero, Quaternion.identity).GetComponent<Enemy>();
        currentRoomEnemy.Add(tempEnemy);
    }

    public void EnemyDie(Enemy enemy)
    {
        currentRoomEnemy.Remove(enemy);
        if (currentRoomEnemy.Count <= 0)
        {
            RoomClear();
        }
    }

    public void PlayerRegisterObserver(IObserver observer)
    {
        player.RegisterObserver(observer);
    }

    public void PlayerRemoveObserver(IObserver observer)
    {
        player.RemoveObserver(observer);
    }

    UIManager uiManager;
    public void SetUimanager()
    {
        uiManager = GameObject.Find("UiCanvas").GetComponent<UIManager>();
    }

    public ForestWorm forestWorm;
    public void SetBossRegisterObserver()
    {
        forestWorm.RegisterObserver(uiManager);
        uiManager.ActiveBossHpBar();
    }

    public void GameClear()
    {
        ConvenienceFunc.LoadScene("03. EndScene");
    }

    public Texture2D cursorImg;
    public void SetCursor()
    {
        Cursor.SetCursor(cursorImg, Vector2.zero, CursorMode.ForceSoftware);
    }
}
