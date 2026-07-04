using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelButton : MonoBehaviour
{
    [SerializeField] private GameObject eachPanel;
    private bool isPanelOpen;

    // Start is called before the first frame update
    void Start()
    {
        eachPanel.SetActive(false);
        PanelManager.Instance.RegisterPanel(this);  // PanelManager‚É“o˜^
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchPanel()
    {
        if (isPanelOpen)
        {
            isClosePanel();
        }
        else if (!isPanelOpen)
        {
            PanelManager.Instance.OpenPanel(this);
            isOpenPanel();
        }
    }

    public void isOpenPanel()
    {
        isPanelOpen = true;
        eachPanel.SetActive(true);
    }

    public void isClosePanel()
    {
        isPanelOpen = false;
        eachPanel.SetActive(false);
    }

}
