using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelManager : MonoBehaviour
{
    private static PanelManager instance;
    private List<PanelButton> panelButtons = new List<PanelButton>();

    public static PanelManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<PanelManager>();
            }
            return instance;
        }
    }

    public void RegisterPanel(PanelButton panel)
    {
        if (!panelButtons.Contains(panel))
        {
            panelButtons.Add(panel);
        }
    }

    public void OpenPanel(PanelButton activePanel)
    {
        foreach (var panel in panelButtons)
        {
            if(panel != activePanel)
            {
                panel.isClosePanel();
            }
        }
    }

    public void CloseAllPanel()
    {
        foreach (var panel in panelButtons)
        {
            panel.isClosePanel();
        }
    }

}
