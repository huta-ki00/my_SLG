using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LinkMapBtn : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image targetImage;
    [SerializeField] private Color hoverColor = Color.yellow;
    private Color defaultColor;

    // Start is called before the first frame update
    void Start()
    {
        if(targetImage != null)
        {
            defaultColor = targetImage.color;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(targetImage != null)
        {
            targetImage.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if(targetImage != null)
        {
            targetImage.color = defaultColor;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
