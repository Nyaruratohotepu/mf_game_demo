using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class FairyGUITest : MonoBehaviour
{
    private GComponent mainCom;
    public GameingUIController controller;
    private GProgressBar hpBar;
    // Use this for initialization
    void Start()
    {
        mainCom = controller.MainUI;
        hpBar= mainCom.GetChild("playerhpbar").asProgress;
    }

    // Update is called once per frame
    void Update()
    {
        
        
    }
}
