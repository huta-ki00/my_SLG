using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    public Button miniGameSelectButton;
    [SerializeField] private string selectScene;

    // Start is called before the first frame update
    void Start()
    {
        if(miniGameSelectButton != null)
        {
            miniGameSelectButton.onClick.AddListener(OnMiniGameSelectButtonClicked);
        }
    }

    public void OnMiniGameSelectButtonClicked()
    {
        Debug.Log("ミニゲーム選択シーンへ移動");
        IsometricMove playerNPC = FindFirstObjectByType<IsometricMove>();
        GameManager gameManager = GameManager.instance;
        TimeCount timeCount = TimeCount.instance;

        if(GameStateManager.instance != null && playerNPC != null && gameManager != null && timeCount != null)
        {
            GameStateManager.instance.playerState.shouldRestoreTarget = true;
            GameStateManager.instance.SaveState(
                playerNPC,
                gameManager.playerMoney,
                timeCount.GetCountTime);

            Debug.Log($"状態保存：位置={playerNPC.transform.position},お金={gameManager.playerMoney},時間={timeCount.GetCountTime}");
        }
        else
        {
            Debug.Log("状態保存に失敗しました");
        }

        SceneManagerController.instance.GoToMiniGameSelect(selectScene);
    }
}
