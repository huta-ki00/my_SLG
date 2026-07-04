using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUIManager : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    // Start is called before the first frame update
    void Start()
    {
        if(moneyText == null)
        {
            moneyText = GetComponent<TextMeshProUGUI>();
        }

        if(GameManager.instance != null)
        {
            GameManager.instance.SetMoneyText(moneyText);
            GameManager.instance.UpdateMoneyText();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
