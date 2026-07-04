using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerController : MonoBehaviour
{
    public static SceneManagerController instance { get; private set; }

    ////internal static void LoadScene(string v)
    ////{
    ////    throw new NotImplementedException();
    ////}

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //internal static void SetActiveScene(object v)
    //{
    //    throw new NotImplementedException();
    //}

    //internal static object GetSceneByName(string sceneName)
    //{
    //    throw new NotImplementedException();
    //}

    // ミニゲーム選択シーンへ
    public void GoToMiniGameSelect(string selectScene)
    {
        Time.timeScale = 1f;  // タイムスケールをリセット
        SceneManager.LoadScene(selectScene);
    }

    // Mainシーンへ戻る
    public void GoToMain()
    {
        Time.timeScale = 1f;  // タイムスケールをリセット
        SceneManager.LoadScene("Main");
    }

    // ミニゲームシーンへ（シーン名を指定）
    public void GoToMiniGame(string miniGameSceneName)
    {
        Time.timeScale = 1f;  // タイムスケールをリセット
        SceneManager.LoadScene(miniGameSceneName);
    }

    // 現在のシーン名を取得
    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}