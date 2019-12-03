﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI S;
    public Text mirrorText;

    void Start()
    {
        S = this;
        mirrorText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateMirrorText(int value)
    {
        mirrorText.text = "Mirrors: " + PlayerBehavior.S.mirrorCount.ToString();
    }
}