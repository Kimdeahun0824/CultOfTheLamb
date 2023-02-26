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



        //Time.timeScale = 1f;
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


}
