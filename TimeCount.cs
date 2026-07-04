using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeCount : MonoBehaviour
{
    int timeScore = 480;    // 開始時刻 8;00 480
    public float intervalSeconds = 1.0f;
    float timer = 0f;

    [SerializeField] TextMeshProUGUI timeText;
    public Color morningColor = new Color(1.0f, 1.0f, 0.8f);    // AM 朝
    public Color nightColor = new Color(0.8f, 0.6f, 0.4f);      // PM 夜

    public Camera mainCamera;
    public static TimeCount instance;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GameStateManager.instance != null && GameStateManager.instance.playerState.isStateInitialized)
        {
            timeScore = GameStateManager.instance.playerState.currentTime;
            Debug.Log("時間を復元しました");
        }

        UpdateDisplay();
        UpdateBackgroundColor();
    }

    // 現在のtimeScoreを取得するためにおいておく
    public int GetCountTime
    {
        get { return timeScore; }
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= intervalSeconds)
        {
            timer = 0f;

            timeScore += 10;

            if(timeScore >= 1440)
            {
                timeScore = 0;
            }

            UpdateDisplay();
            UpdateBackgroundColor();

            if(GameStateManager.instance != null)
            {
                GameStateManager.instance.playerState.currentTime = timeScore;
            }
        }
    }

    void UpdateDisplay()
    {
        int totalMinutes = timeScore;
        int hour = (totalMinutes / 60) % 12;
        if (hour == 0) hour = 0;    
        int minute = totalMinutes % 60;

        bool isPM = (totalMinutes / 60) >= 12;
        string ampm = isPM ? "PM" : "AM";

        //時間を表示
        string text = string.Format("{0} {1:00}:{2:00}", ampm, hour, minute);
        if(timeText != null)
        {
            timeText.text = text;
        }
    }

    void UpdateBackgroundColor()
    {
        if((timeScore / 60) >= 6 && (timeScore / 60) < 18)
        {
            mainCamera.backgroundColor = morningColor;
        }
        else
        {
            mainCamera.backgroundColor = nightColor;
        }
    }

    // 必要ない可能性あり
    // 他スクリプト用
    public string GetCurrentTime()
    {
        // 現在時刻を返す
        int totalMinutes = timeScore;
        int hour = (totalMinutes / 60) % 12;
        if (hour == 0) hour = 12;
        int minute = totalMinutes % 60;

        bool isPM = (totalMinutes / 60) >= 12;
        string ampm = isPM ? "PM" : "AM";
        return
        string.Format("{0}{1:00}:{2:00}",ampm, hour, minute);
    }

    private void OnDestroy()
    {
        if(GameStateManager.instance != null)
        {
            GameStateManager.instance.playerState.currentTime = timeScore;
        }
    }
}
