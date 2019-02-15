using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class GameingUIController : MonoBehaviour {
    public GComponent MainUI,TalkUI;
    public float[] StateDuringTimeRemain;
	// Use this for initialization
	void Start () {
        MainUI = GetComponent<UIPanel>().ui;
        StartUpUISettings();
        ChangeHpBar(100, 500);
        GetAState(5, 5);
	}
	
	// Update is called once per frame
	void Update () {
        StateDuringTimeReduce();
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
        StateDuringTimeRemain = new float[6];
        MainUI.GetChild("playerhead").asCom.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "maruko");
        MainUI.GetChild("charactor").asButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "charactor");
        MainUI.GetChild("bag").asButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "bag");
        MainUI.GetChild("mission").asButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "mission");
        MainUI.GetChild("menu").asButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "menu");
        TalkUI = MainUI.GetChild("talkcomponent").asCom;
        TalkUI.visible = false;
        MainUI.GetChild("playerlv").asButton.onRollOver.Add(ShowNeedToLevelUp);
        MainUI.GetChild("playerlv").asButton.onRollOut.Add(() => { ChangeLevel(10); });
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
        switch (WhoIsTalking)
        {
            case 0:
                TalkUI.GetChild("speakerl").asLoader.alpha = 0.5f;
                TalkUI.GetChild("speakerr").asLoader.alpha = 0.5f;
                break;
            case 1:
                TalkUI.GetChild("speakerl").asLoader.alpha = 1f;
                TalkUI.GetChild("speakerr").asLoader.alpha = 0.5f;
                break;
            case 2:
                TalkUI.GetChild("speakerl").asLoader.alpha = 0.5f;
                TalkUI.GetChild("speakerr").asLoader.alpha = 1f;
                break;
            case 12:
                TalkUI.GetChild("speakerl").asLoader.alpha = 1f;
                TalkUI.GetChild("speakerr").asLoader.alpha = 1f;
                break;
            default:
                Debug.LogError("只能是0,1,2,12");
                break;
        }
        TalkUI.GetChild("talktext").asCom.GetChild("talker").asTextField.text = SpeakerName;
        TalkUI.GetChild("talktext").asCom.GetChild("speechtext").asTextField.text = Speech;
    }
    public void ChangeSpeakerPic(string LeftSpeakerPicName, string RightSpeakerPicName)
    {
        TalkUI.GetChild("speakerl").asLoader.url = UIPackage.GetItemURL("GamingMain",LeftSpeakerPicName);
        TalkUI.GetChild("speakerr").asLoader.url = UIPackage.GetItemURL("GamingMain", RightSpeakerPicName);
    }
    public void ChangeHpBar(int CurrentHp, int TotalHp)
    {
        MainUI.GetChild("playerhpbar").asProgress.max = TotalHp;
        MainUI.GetChild("playerhpbar").asProgress.value = CurrentHp;
    }
    public void ChangeLevel(int level)
    {
        MainUI.GetChild("playerlvtext").asTextField.text = "Lv." + level;
    }
    void ShowNeedToLevelUp()
    {
        MainUI.GetChild("playerlvtext").asTextField.text = "200/500";
    }
    public void GetAState(int StateNum,float DuringTime)
    {
        switch (StateNum)
        {
            case 1:
                MainUI.GetChild("poisoned").asButton.alpha = 1;
                MainUI.GetChild("n20").asTextField.alpha = 1;
                StateDuringTimeRemain[0] = DuringTime;
                break;
            case 2:
                MainUI.GetChild("powered").asButton.alpha = 1;
                MainUI.GetChild("n21").asTextField.alpha = 1;
                StateDuringTimeRemain[1] = DuringTime;
                break;
            case 3:
                MainUI.GetChild("slowed").asButton.alpha = 1;
                MainUI.GetChild("n22").asTextField.alpha = 1;
                StateDuringTimeRemain[2] = DuringTime;
                break;
            case 4:
                MainUI.GetChild("bleeding").asButton.alpha = 1;
                MainUI.GetChild("n23").asTextField.alpha = 1;
                StateDuringTimeRemain[3] = DuringTime;
                break;
            case 5:
                MainUI.GetChild("diziness").asButton.alpha = 1;
                MainUI.GetChild("n24").asTextField.alpha = 1;
                StateDuringTimeRemain[4] = DuringTime;
                break;
            case 6:
                MainUI.GetChild("speedup").asButton.alpha = 1;
                MainUI.GetChild("n25").asTextField.alpha = 1;
                StateDuringTimeRemain[5] = DuringTime;
                break;
            default:
                break;
        }
    }
    public void EndState(int StateNum)
    {
        switch (StateNum)
        {
            case 1:
                MainUI.GetChild("poisoned").asButton.alpha = 0.3f;
                MainUI.GetChild("n20").asTextField.alpha = 0.3f;
                break;
            case 2:
                MainUI.GetChild("powered").asButton.alpha = 0.3f;
                MainUI.GetChild("n21").asTextField.alpha = 0.3f;
                break;
            case 3:
                MainUI.GetChild("slowed").asButton.alpha = 0.3f;
                MainUI.GetChild("n22").asTextField.alpha = 0.3f;
                break;
            case 4:
                MainUI.GetChild("bleeding").asButton.alpha = 0.3f;
                MainUI.GetChild("n23").asTextField.alpha = 0.3f;
                break;
            case 5:
                MainUI.GetChild("diziness").asButton.alpha = 0.3f;
                MainUI.GetChild("n24").asTextField.alpha = 0.3f;
                break;
            case 6:
                MainUI.GetChild("speedup").asButton.alpha = 0.3f;
                MainUI.GetChild("n25").asTextField.alpha = 0.3f;
                break;
            default:
                break;
        }
    }
    void StateDuringTimeReduce()
    {
        int i = 0;
        for (i = 0; i < 6; i++)
        {
            if (StateDuringTimeRemain[i] > 0)
            {
                StateDuringTimeRemain[i] -= Time.deltaTime;
                if (StateDuringTimeRemain[i] <= 0)
                {
                    EndState(i + 1);
                }
            }
        }
    }
}
