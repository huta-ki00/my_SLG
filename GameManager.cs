using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance{ get; private set; }
    public int playerMoney = 0;
    public TextMeshProUGUI moneyText;
    public Vector3 lastMenuPosition;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
            instance = this;
            // シーンを変更してもオブジェクトを破棄しない
            DontDestroyOnLoad(gameObject);
    }


    // Start is called before the first frame update
    private void Start()
    {
        if(GameStateManager.instance != null && GameStateManager.instance.playerState.money > 0)
        {
            playerMoney = GameStateManager.instance.playerState.money;
        }

        UpdateMoneyText();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameStateManager.instance != null)
        {
            GameStateManager.instance.playerState.money = playerMoney;
        }
    }


    // お金を増やす
    public void AddMoney(int amount)
    {
        playerMoney += amount;
        UpdateMoneyText();

        if(GameStateManager.instance != null)
        {
            GameStateManager.instance.playerState.money = playerMoney;
        }
    }

    // 購入できるかどうか
    public bool TryPurchase(int cost)
    {
        if(playerMoney >= cost)
        {
            playerMoney -= cost;
            UpdateMoneyText();
            if(GameStateManager.instance != null)
            {
                GameStateManager.instance.playerState.money = playerMoney;
            }

            return true;
        }
        else
        {
            Debug.Log("所持金が足りません");
            return false;
        }
    }

    public int GetMoney()
    {
        return playerMoney;
    }

    public void SetMoneyText(TextMeshProUGUI text)
    {
        moneyText = text;
        Debug.Log($"MoneyTextが更新されました:{moneyText.name}");
    }

    public void UpdateMoneyText()
    {
        if(moneyText != null)
        {
            moneyText.text = playerMoney + "円";
        }
        else
        {
            Debug.LogWarning("moneyTextが見つかりません");
        }
    }

    private void OnDestroy()
    {
        if(GameStateManager.instance != null)
        {
            GameStateManager.instance.playerState.money = playerMoney;
        }
    }
}
