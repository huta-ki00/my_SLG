using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AutoMove : MonoBehaviour
{
    public Tilemap tilemap; // 歩くタイルマップ
    [Header("目的地設定")]
    public Transform seat;  // 席
    public Transform counter;   // レジ
    public Transform entrance;  // 入口
    public Transform exit;  // 出口


    public float speed = 4.0f;  // 移動速度
    private Transform currentTarget;    // 現在の目的地
    Vector3Int currentCell; // 今いるグリッド座標
    Vector3 targetWorldPos; // 今向かっているワールド座標
    private bool isWorking = false; // 作業中かどうか
    private int phase = 0;

    // Start is called before the first frame update
    void Start()
    {
        currentTarget = entrance;
        currentCell = tilemap.WorldToCell(transform.position);
        targetWorldPos = tilemap.GetCellCenterWorld(currentCell);
        transform.position = targetWorldPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTarget == null || isWorking) return;

        Vector3 currentPos = transform.position;    // 現在地の座標
        Vector3 targetPos = currentTarget.position;        // 目的地の座標

        // X軸座標の移動
        if(Mathf.Abs(currentPos.x - targetPos.x) > 0.001f)
        {
            float nextX = Mathf.MoveTowards(currentPos.x, targetPos.x, speed * Time.deltaTime);
            transform.position = new Vector3(nextX, currentPos.y, currentPos.z);
        }
        // Y軸座標の移動
        else if(Mathf.Abs(currentPos.y - targetPos.y) > 0.001f)
        {
            float nextY = Mathf.MoveTowards(currentPos.y, targetPos.y, speed * Time.deltaTime);
            transform.position = new Vector3(currentPos.x, nextY, currentPos.z);
        }
        else
        {
            StartCoroutine(reAction());
        }
    }

    IEnumerator reAction()
    {
        isWorking = true;
        if(phase == 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentTarget = seat;
            phase = 1;
        }
        else if (phase == 1)
        {
            Debug.Log("注文を頼みます");
            yield return new WaitForSeconds(3.0f);
            Debug.Log("ごちそうさまでした");


            currentTarget = counter;
            phase = 2;
        }
        else if (phase == 2)
        {
            Debug.Log("会計中");
            yield return new WaitForSeconds(1.0f);

            AddMoney(100);

            currentTarget = exit;
            phase = 3;
        }
        else if(phase == 3)
        {
            yield return new WaitForSeconds(1.0f);
            Debug.Log("また来ます");
            Destroy(gameObject, 0.5f);
        }

        isWorking = false;
    }

    void AddMoney(int amount)
    {
        Debug.Log($"お金が {amount}円増えました");
    }
}
