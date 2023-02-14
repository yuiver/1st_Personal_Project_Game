using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public static partial class Util
{
    public static void QuitThisGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
    }       // QuitThisGame()

    public static void LoadScene(string sceneName_)
    {
        SceneManager.LoadScene(sceneName_);
    }       // LoadScene()
}
