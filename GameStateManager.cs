using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager instance { get; private set; }

    [System.Serializable]
    public class PlayerState
    {
        public int money = 0;
        public int currentTime = 0;
        public Vector3 position = Vector3.zero;
        public int targetIndex = -1;
        public bool isInBed = false;
        public bool isSleep = false;
        public bool isWakeUp = false;
        public int wakeBedTime = -1;
        public float waitTime = 3f;
        public bool isStateInitialized = false;
        public bool shouldRestoreTarget = true;
    }

    public PlayerState playerState = new PlayerState();

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveState(IsometricMove player, int money, int time)
    {
        playerState.money = money;
        playerState.currentTime = time;
        playerState.position = player.transform.position;
        playerState.isInBed = player.isInBed;
        playerState.isSleep = player.isSleep;
        playerState.isWakeUp = player.isWakeUp;
        playerState.wakeBedTime = player.wakeBedTime;
        playerState.waitTime = player.waitTime;
        playerState.isStateInitialized = true;
        playerState.targetIndex = player.GetCurrentTargetIndex();
    }

    public void LoadState(IsometricMove player)
    {
        player.transform.position = playerState.position;
        player.isInBed = playerState.isInBed;
        player.isSleep = playerState.isSleep;
        player.isWakeUp = playerState.isWakeUp;
        player.wakeBedTime = playerState.wakeBedTime;
        player.waitTime = playerState.waitTime;
        player.SetCurrentTargetIndex(playerState.targetIndex);
        player.SetShouldRestoreTarget(playerState.shouldRestoreTarget);
    }

    public void ResetState()
    {
        playerState = new PlayerState();
    }
}
