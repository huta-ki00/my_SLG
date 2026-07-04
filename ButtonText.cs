using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private Button button;
    private bool isMenu = true;


    public void OnClickButton()
    {
        if (isMenu)
        {
            buttonText.text = "Ç‡Ç«ÇÈ";
            buttonText.fontSize = 26;
        }
        else if (!isMenu)
        {
            buttonText.text = "ÉÅÉjÉÖÅ[";
            buttonText.fontSize = 24;
        }

        isMenu = !isMenu;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
