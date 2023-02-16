using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public static partial class ConvenienceFunc
{
    public static void SetOnSceneLoaded(UnityAction<Scene, LoadSceneMode> sceneLoadedFunc)
    {
        SceneManager.sceneLoaded += sceneLoadedFunc;
    }
}
