using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IsometricMove : MonoBehaviour
{
    public Grid grid;
    public Transform[] targets;
    public float speed = 2f;
    public float waitTime = 3f;        // 滞在時間
    public Transform bedTarget;         // ベッド
    private Transform currentTarget;    // 現在のターゲット
    private bool isMoving = true;       // 移動中か
    private bool staying = false;       // 待機中か
    public bool isSleep = false;       // 睡眠
    //private bool targetGiveMoney = false;   // デフォルトでお金は増えない
    private int countIndex = -1;        // 
    public int wakeBedTime = -1;       // 
    public bool isInBed = false;
    public bool isWakeUp = false;

    public MessageController message;
    //public MoneyCount moneyCount;

    private bool shouldRestoreTarget = true;
    private bool hasInitialized = false;

    // Start is called before the first frame update
    void Start()
    {
        grid = FindFirstObjectByType<Grid>();
        if (!hasInitialized)
        {
            hasInitialized = true;
            if (GameStateManager.instance != null && GameStateManager.instance.playerState.isStateInitialized)
            {
                Debug.Log("保存された状態から復元します");
                GameStateManager.instance.LoadState(this);
                if (shouldRestoreTarget && countIndex >= 0 && countIndex < targets.Length)
                {
                    currentTarget = targets[countIndex];
                }
                else if (!shouldRestoreTarget)
                {
                    SelectNewTarget();
                }
            }
            else
            {
                Debug.Log("初回スタート");
                SelectNewTarget();
                isInBed = false;
            }
        }

        StartCoroutine(MoveToSeatRoutine());
    }

    private void OnEnable()
    {
        if(hasInitialized && GameManager.instance != null && GameStateManager.instance.playerState.isStateInitialized)
        {
            Debug.Log("シーン復帰時：shouldRestoreTarget=" +
                GameStateManager.instance.playerState.shouldRestoreTarget);

            if(GameStateManager.instance.playerState.shouldRestoreTarget && countIndex >= 0 && countIndex < targets.Length)
            {
                currentTarget = targets[countIndex];
                GameStateManager.instance.playerState.shouldRestoreTarget = false;
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        int currentTime = TimeCount.instance.GetCountTime;

        if (isInBed)
        {
            Debug.Log("AM7:30 ～ AM8:30の間に起きます");
            //int currentTime = TimeCount.instance.GetCountTime;
            if(!isWakeUp && currentTime == wakeBedTime)
            {
                Debug.Log("起きました");
                Time.timeScale = 1f;
                isWakeUp = true;
                WakeUpBed();
                speed = 2f;
            }
            return;
        }

        if (isMoving) return;

        int sleepTime = 23 * 60 + 30;   // 1410 (PM11:30)

        if (currentTime >= sleepTime)
        {
            isSleep = true;
        }

        if(!staying && isSleep && !isInBed)
        {
            MoveToBed();
            return;
        }

        if(!staying)
        {
            StartCoroutine(StayAtTarget());
        }

        if(GameStateManager.instance != null && GameManager.instance != null && TimeCount.instance != null)
        {
            GameStateManager.instance.SaveState(
                this,
                GameManager.instance.playerMoney,
                TimeCount.instance.GetCountTime);
        }
    }

    IEnumerator MoveToSeatRoutine()
    {
        //GameObject seat = GameObject.FindWithTag("Seat");
        if (currentTarget == null) yield break;

        Vector3Int currentCell = grid.WorldToCell(transform.position);
        Vector3Int targetCell = grid.WorldToCell(currentTarget.transform.position);


        // x方向の移動
        while(currentCell.x != targetCell.x)
        {
            currentCell.x += (int)Mathf.Sign(targetCell.x - currentCell.x);
            yield return StartCoroutine(MoveToCell(currentCell));
        }

        // y方向の移動
        while (currentCell.y != targetCell.y)
        {
            currentCell.y += (int)Mathf.Sign(targetCell.y - currentCell.y);
            yield return StartCoroutine(MoveToCell(currentCell));
        }

        Debug.Log("目標につきました");
        if(currentTarget == bedTarget)
        {
            isInBed = true;
            // 睡眠中は時間経過を速くする
            Time.timeScale = 2.5f;
        }
        isMoving = false;
    }

    IEnumerator MoveToCell(Vector3Int targetCell)
    {
        isMoving = true;
        Vector3 targetWorldPos = grid.GetCellCenterWorld(targetCell);

        // ターゲットのセルに移動
        while(Vector3.Distance(transform.position, targetWorldPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetWorldPos, speed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetWorldPos;
        isMoving = false;
    }

    IEnumerator StayAtTarget()
    {
        if (staying) yield break;
        staying = true;

        TargetProperties targetProp = currentTarget.GetComponent<TargetProperties>();
        if(targetProp != null)
        {
            waitTime = targetProp.stayDuration;
            //targetGiveMoney = true;
        }
        else
        {
            waitTime = 3f;  // デフォルトの滞在時間
            //targetGiveMoney = false;
        }

        yield return new WaitForSeconds(waitTime);

        // お金を取得
        if (targetProp.giveMoney && targetProp != null)
        {
            int randomMoney = targetProp.RandomMoney();
            GameManager.instance.AddMoney(randomMoney);
            message.ShowMessage(randomMoney + "円スパチャをもらった！");
            //moneyCount.PopUpMoney(randomMoney);
            Debug.Log($"{randomMoney}円を獲得");
        }

        SelectNewTarget();
        StartCoroutine(MoveToSeatRoutine());

        staying = false;
    }


    // TimeCountのtimeScoreが PM11:30になったら強制でベッドへ
    void SelectNewTarget()
    {
        if (targets.Length == 0) return;    // ターゲットが存在しない場合は処理を終了

        int newIndex;
        do
        {
            newIndex = Random.Range(0, targets.Length);
        } while (newIndex == countIndex);   // 前のターゲットと同じ場合は再選択する

        countIndex = newIndex;
        // 現在のターゲットを更新
        currentTarget = targets[countIndex];

        isMoving = true;
        staying = false;
    }

    void MoveToBed()
    {
        Debug.Log("ベッドに向かいます");
        currentTarget = bedTarget;  // ベッドを目標に設定する
        isMoving = true;
        staying = false;
        isWakeUp = false;
        speed = 2.7f;

        // 450, 460, 470, 480, 490, 500, 510 (AM7:30 ～ AM8:30)
        wakeBedTime = 450 + 10 * Random.Range(0, 6);

        StartCoroutine(MoveToSeatRoutine());
    }

    void WakeUpBed()
    {
        isInBed = false;
        isSleep = false;

        SelectNewTarget();  // 新しいターゲットを選択
        StartCoroutine(MoveToSeatRoutine());
    }

    public int GetCurrentTargetIndex()
    {
        return countIndex;
    }

    public void SetCurrentTargetIndex(int index)
    {
        countIndex = index;
    }

    public void SetShouldRestoreTarget(bool restore)
    {
        shouldRestoreTarget = restore;
    }

    private void OnDestroy()
    {
        if(GameStateManager.instance != null && GameManager.instance != null && TimeCount.instance != null)
        {
            GameStateManager.instance.SaveState(
                this,
                GameManager.instance.playerMoney,
                TimeCount.instance.GetCountTime);
        }
    }
}
