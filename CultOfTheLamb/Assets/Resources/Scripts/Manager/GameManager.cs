using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    public float maxX = default;
    public float maxY = default;
    private new void Awake()
    {
        base.Awake();
        ConvenienceFunc.SetOnSceneLoaded(OnSceneLoaded);
    }

    void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        switch (scene.name)
        {
            default:
                break;
        }
    }


}
