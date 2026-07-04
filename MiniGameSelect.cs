using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameSelect : MonoBehaviour
{
    public Button backButton;
    public Button miniGame1;
    public Button miniGame2;
    public Button miniGame3;

    // Start is called before the first frame update
    void Start()
    {
        if(backButton != null)
        {
            backButton.onClick.AddListener(OnBackButtonClicked);
        }

        if(miniGame1 != null)
        {
            miniGame1.onClick.AddListener(() => OnMiniGameSelected("MiniGame_1"));
        }

        if(miniGame2 != null)
        {
            miniGame2.onClick.AddListener(() => OnMiniGameSelected("MiniGame_2"));
        }

        if(miniGame3 != null)
        {
            miniGame3.onClick.AddListener(() => OnMiniGameSelected("MiniGame_3"));
        }
    }

    private void OnBackButtonClicked()
    {
        Debug.Log("Mainシーンに戻ります");
        if(GameStateManager.instance != null && GameManager.instance != null && TimeCount.instance != null)
        {
            GameStateManager.instance.playerState.shouldRestoreTarget = true;
            GameStateManager.instance.playerState.money = GameManager.instance.playerMoney;
            GameStateManager.instance.playerState.currentTime = TimeCount.instance.GetCountTime;

            Debug.Log($"復元予定：位置={GameStateManager.instance.playerState.position}," +
                $" お金={GameStateManager.instance.playerState.money}," +
                $" 時間={GameStateManager.instance.playerState.currentTime}");
        }
        SceneManagerController.instance.GoToMain();
    }

    private void OnMiniGameSelected(string miniGameSceneName)
    {
        Debug.Log($"{miniGameSceneName}を開始します");
        SceneManagerController.instance.GoToMiniGame(miniGameSceneName);
    }

    private void SaveGameState()
    {
        if(GameStateManager.instance != null && GameManager.instance != null && TimeCount.instance != null)
        {
            GameStateManager.instance.playerState.money = GameManager.instance.playerMoney;
            GameStateManager.instance.playerState.currentTime = TimeCount.instance.GetCountTime;
            //GameStateManager.instance.playerState.shouldRestoreState = true;
            Debug.Log($"状態保存:お金={GameStateManager.instance.playerState.money},時刻={GameStateManager.instance.playerState.currentTime}");
        }
    }
}
