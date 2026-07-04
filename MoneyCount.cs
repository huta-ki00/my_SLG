using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyCount : MonoBehaviour
{
    // NPCの上部に表示するお金

    [SerializeField] private GameObject target;
    private Animator NPCMoneyCount;         // アニメーション
    public TextMeshProUGUI npcMoneyText;
    public GameObject MoneyCountPanel;
    [SerializeField] private float closeAnim = 1f;

    void Start()
    {
        MoneyCountPanel.SetActive(false);
    }

    public void PopUpMoney(int a)
    {
        MoneyCountPanel.SetActive(true);
        NPCMoneyCount.SetTrigger("PopUP");
    }

    private IEnumerator PopUpCloseTime()
    {
        yield return new WaitForSeconds(closeAnim);
        MoneyCountPanel.SetActive(false);
    }

}
