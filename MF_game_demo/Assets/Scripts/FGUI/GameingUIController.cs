using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class GameingUIController : MonoBehaviour {
    public GComponent MainUI,TalkUI;
	// Use this for initialization
	void Start () {
        MainUI = GetComponent<UIPanel>().ui;
        StartUpUISettings();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TalkActive();
            ChangeSpeakerPic("1", "23");
            ContinueChat(2, "瑄", "我说");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TalkDisable();
        }
	}
    void StartUpUISettings()
    {
        MainUI.GetChild("playerhead").asCom.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "maruko");
        MainUI.GetChild("charactor").asButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "charactor");
        MainUI.GetChild("bag").asButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "bag");
        MainUI.GetChild("mission").asButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "mission");
        MainUI.GetChild("menu").asButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "menu");
        TalkUI = MainUI.GetChild("talkcomponent").asCom;
        TalkUI.visible = false;
    }
    public void TalkActive()
    {
        TalkUI.visible = true; 
        
    }
    public void TalkDisable()
    {
        TalkUI.visible = false; 
    }
    public void ContinueChat(int WhoIsTalking,string SpeakerName,string Speech)
    {
        if (WhoIsTalking == 0)
        {
            TalkUI.GetChild("speakerl").asLoader.alpha = 0.5f;
            TalkUI.GetChild("speakerr").asLoader.alpha = 0.5f;
        }
        else if (WhoIsTalking == 1)
        {
            TalkUI.GetChild("speakerl").asLoader.alpha = 1f;
            TalkUI.GetChild("speakerr").asLoader.alpha = 0.5f;
        }
        else if (WhoIsTalking == 2)
        {
            TalkUI.GetChild("speakerl").asLoader.alpha = 0.5f;
            TalkUI.GetChild("speakerr").asLoader.alpha = 1f;
        }
        else if (WhoIsTalking == 12)
        {
            TalkUI.GetChild("speakerl").asLoader.alpha = 1f;
            TalkUI.GetChild("speakerr").asLoader.alpha = 1f;
        }
        else
        {
            Debug.LogError("只能是0,1,2,12");
        }
        TalkUI.GetChild("talktext").asCom.GetChild("talker").asTextField.text = SpeakerName;
        TalkUI.GetChild("talktext").asCom.GetChild("speechtext").asTextField.text = Speech;
    }
    public void ChangeSpeakerPic(string LeftSpeakerPicName, string RightSpeakerPicName)
    {
        TalkUI.GetChild("speakerl").asLoader.url = UIPackage.GetItemURL("GamingMain",LeftSpeakerPicName);
        TalkUI.GetChild("speakerr").asLoader.url = UIPackage.GetItemURL("GamingMain", RightSpeakerPicName);
    }
}
