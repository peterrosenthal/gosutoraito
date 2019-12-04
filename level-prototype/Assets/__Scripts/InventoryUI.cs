using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI S;
    public Text mirrorText;
    public Text prismText;
    public Image selector;

    private static float selectorMax;
    private static float selectorMin;


    void Start()
    {
        S = this;
        selectorMax = mirrorText.rectTransform.position.y + 10;
        selectorMin = prismText.rectTransform.position.y + 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateInterfaceText()
    {
        mirrorText.text = PlayerBehavior.S.mirrorCount.ToString();
        prismText.text = PlayerBehavior.S.prismCount.ToString();
    }

    void OnGUI()
    {
        Vector3 pos = selector.rectTransform.position;
        if (Input.mouseScrollDelta.y > 0)
        {
            pos.y = selectorMax;
            PlayerBehavior.S.selectedItem = (int)PlayerBehavior.equipment.mirror;
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            pos.y = selectorMin;
            PlayerBehavior.S.selectedItem = (int)PlayerBehavior.equipment.prism;
        }
        selector.rectTransform.position = pos;
    }
}
