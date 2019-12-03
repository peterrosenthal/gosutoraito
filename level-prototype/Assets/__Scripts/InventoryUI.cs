using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI S;
    public Text mirrorText;
    public Text prismText;

    void Start()
    {
        S = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInterfaceText()
    {
        mirrorText.text = "Mirrors: " + PlayerBehavior.S.mirrorCount.ToString();
        prismText.text = "Prisms: " + PlayerBehavior.S.prismCount.ToString();
    }
}
