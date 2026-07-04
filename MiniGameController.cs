using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    public void EndMiniGame()
    {
        Debug.Log("ミニゲーム終了。状態を保存してMainシーンに戻ります");
        
        if(GameStateManager.instance != null && GameManager.instance != null && TimeCount.instance != null)
        {
            GameStateManager.instance.playerState.shouldRestoreTarget = true;
            GameStateManager.instance.playerState.money = GameManager.instance.playerMoney;
            GameStateManager.instance.playerState.currentTime = TimeCount.instance.GetCountTime;
        }

        Time.timeScale = 1f;
        SceneManagerController.instance.GoToMain();
    }
}
