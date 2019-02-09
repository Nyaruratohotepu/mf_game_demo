using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class GameingUIController : MonoBehaviour {
    GComponent MainUI;
	// Use this for initialization
	void Start () {
        MainUI = GetComponent<UIPanel>().ui;
        StartUpUISettings();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void StartUpUISettings()
    {
        MainUI.GetChild("playerhead").asCom.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "maruko");
        MainUI.GetChild("charactor").asButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "charactor");
        MainUI.GetChild("bag").asButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "bag");
        MainUI.GetChild("mission").asButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "mission");
        MainUI.GetChild("menu").asButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "menu");
    }
}
