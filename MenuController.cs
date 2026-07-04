using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject MenuPanel;
    public GameObject BackPanel;
    private Animator MenuAnim;  // メニューのアニメーション
    private bool isOpen = false;    // メニューが開いているか

    // Start is called before the first frame update
    void Start()
    {
        MenuAnim = MenuPanel.GetComponent<Animator>();
        MenuPanel.SetActive(false); // 最初は非表示
        BackPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchMenu()
    {
        if (isOpen)
        {
            CloseMenu();
        }
        else if(!isOpen)
        {
            OpenMenu();
        }

    }

    public void OpenMenu()
    {
        isOpen = true;
        MenuPanel.SetActive(true);  // メニュー表示
        BackPanel.SetActive(true);
        MenuAnim.SetBool("MenuPanel", true);
        Time.timeScale = 0; // ゲームを一時停止
    }

    public void CloseMenu()
    {
        PanelManager.Instance.CloseAllPanel();
        isOpen = false;
        BackPanel.SetActive(false);
        MenuAnim.SetBool("MenuPanel", false);
        StartCoroutine(ClosePanelAnim());
        Time.timeScale = 1; // ゲームを再開
    }

    private IEnumerator ClosePanelAnim()
    {
        yield return new WaitForSecondsRealtime(0.8f);
        MenuPanel.SetActive(false);
    }

}
