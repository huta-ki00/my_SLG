using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageController : MonoBehaviour
{
    private Animator TextAnim;
    public TextMeshProUGUI MessageInfo;
    public GameObject MessagePanel;
    [SerializeField] private float closeAnim = 1f;

    // Start is called before the first frame update
    void Start()
    {
        TextAnim = MessageInfo.GetComponent<Animator>();
        MessagePanel.SetActive(false);
    }

    public void ShowMessage(string message, float duration = 3f)
    {
        MessageInfo.text = message;
        MessagePanel.SetActive(true);
        TextAnim.SetBool("isMessage", true);
        StartCoroutine(HideMessageTime(duration));
    }

    public void CloseMessage()
    {
    }

    private IEnumerator HideMessageTime(float duration)
    {
        yield return new WaitForSeconds(duration);
        TextAnim.SetBool("isMessage", false);
        yield return new WaitForSeconds(closeAnim);
        MessagePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
