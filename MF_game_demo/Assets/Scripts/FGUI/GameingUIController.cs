using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
//概述：标有public的方法有可能在外部调用，需要查看注释，注释中标有*******表示函数需要补充，private函数只需部分补充，不需要外部调用-----看完的或是补完的可以自行在注释上打好标记，以后更新新的注释好区分
public class GameingUIController : MonoBehaviour
{
    int CurrentLevel = 1;
    public GComponent MainUI, TalkUI,Choices;
    public float[] StateDuringTimeRemain;
    public Bag PlayerBag;
    // Use this for initialization
    void Start()
    {

        StartUpUISettings();
        StartUpOutSide();
        ChangeHpBar(100, 500);
        GetAState(5, 5);
       // PlayerBag.AddItem(1, "1", 1,1);
    }

    // Update is called once per frame
    void Update()
    {
        StateDuringTimeReduce();
        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    TalkActive();
        //    ChangeSpeakerPic("1", "23");
        //    ContinueChat(2, "瑄", "我说");
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
            
        //    ChangeSpeakerPic("1");
        //    ContinueChat(2, "瑄", "我说");
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    TalkDisable();
        //}
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerBag.AddItem(1, "1", 1, 3,"weapon");
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            List<string> a = new List<string>();
            a.Add("hqwiodj");
            a.Add("你好");
            a.Add("再见");
            GiveChoices(a);
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            List<string> a = new List<string>();
            a.Add("你说什么");
            a.Add("好的");
            GiveChoices(a);
        }
        
        
    }
    void StartUpUISettings()
    {
        UIPackage.AddPackage("FGUI/GameMainUI/GamingMain");
        MainUI = GetComponent<UIPanel>().ui;
        StateDuringTimeRemain = new float[6];
        MainUI.GetChild("playerhead").asCom.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "maruko");
        MainUI.GetChild("charactor").asButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "charactor");
        MainUI.GetChild("bag").asButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "bag");
        MainUI.GetChild("mission").asButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "mission");
        MainUI.GetChild("menu").asButton.GetChild("icon").asLoader.url = UIPackage.GetItemURL("GamingMain", "menu");
        TalkUI = MainUI.GetChild("talkcomponent").asCom;
        TalkUI.visible = false;
        Choices = MainUI.GetChild("speakchoice").asCom;
        Choices.visible = false;
        MainUI.GetChild("playerlv").asButton.onRollOver.Add(ShowNeedToLevelUp);
        MainUI.GetChild("playerlv").asButton.onRollOut.Add(() => { ChangeLevel(10); });//待修改
        PlayerBag = new Bag();
        PlayerBag.SetXY(250, 110);
        MainUI.GetChild("bag").asButton.onClick.Add(() => { PlayerBag.Show();});
        PlayerBag.contentPane = UIPackage.CreateObject("GamingMain", "bagmain").asCom;
        PlayerBag.ItemList = PlayerBag.contentPane.GetChild("baglist").asList;
        PlayerBag.UICamera = GameObject.Find("Stage Camera").GetComponent<Camera>() ;
    }
    private void StartUpOutSide()
    {
        //***************
        //初始化外部需要访问的东西，自行在开头添加需要的变量，在这个函数里初始化，以便后面补充部分需要
        //***************

        //注册UI点击事件
        TalkUI.GetChild("talktext").asCom.onClick.Add(StoryPlayer.GoOn);

    }
    public void TalkActive()//开始显示对话框时先调用这个
    {
        TalkUI.visible = true;
    }
    public void TalkDisable()//结束对话时调用这个关闭对话框
    {
        TalkUI.visible = false;
    }

    //不改变立绘更新说话方或说话内容调用此函数；
    //参数一：说话方是左边的人还是右边的人，或是画外音，不说话的人立绘会呈半透明，1代表左，2代表右，12代表都说，0代表都不说（画外音）
    //参数二：说话者的名字 
    //参数三：说话内容
    //新增：单人居中说话时参数一可以随意
    public void ContinueChat(int WhoIsTalking, string SpeakerName, string Speech)
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
    public void ChangeSpeakerPic(string LeftSpeakerPicName, string RightSpeakerPicName) //改变立绘，参数为左边角色和右边角色立绘名（在fgui中，命名规则后续进行规范）,若需要单独立绘居中，则使用单参数重载，使用双参数时中间图片会自动置空，反之亦然
    {
        TalkUI.GetChild("speakerm").asLoader.url = null;
        if (LeftSpeakerPicName != null)
            TalkUI.GetChild("speakerl").asLoader.url = UIPackage.GetItemURL("GamingMain", LeftSpeakerPicName);
        if (RightSpeakerPicName != null)
            TalkUI.GetChild("speakerr").asLoader.url = UIPackage.GetItemURL("GamingMain", RightSpeakerPicName);        
    }
    public void ChangeSpeakerPic(string MiddleSpeakerName)
    {
        TalkUI.GetChild("speakerl").asLoader.url = null;
        TalkUI.GetChild("speakerr").asLoader.url = null;
        TalkUI.GetChild("speakerm").asLoader.url = UIPackage.GetItemURL("GamingMain", MiddleSpeakerName);
    }
    public void ChangeBackGround(string Name)//改变背景图片，参数为背景图片名（在fgui中，命名规则后续进行规范）
    {
        TalkUI.GetChild("background").asLoader.url = UIPackage.GetItemURL("GamingMain", Name);
    }
    public void ChangeHpBar(int CurrentHp, int TotalHp)//改变血条当前血量、血量上限，参数为当前血量，血量上限
    {
        MainUI.GetChild("playerhpbar").asProgress.max = TotalHp;
        MainUI.GetChild("playerhpbar").asProgress.value = CurrentHp;
    }
    public void ChangeLevel(int level)//升级时或含此套ui的场景初始化时调用一次此函数，传入当前人物等级
    {
        CurrentLevel = level;
        MainUI.GetChild("playerlvtext").asTextField.text = "Lv." + CurrentLevel;
    }
    void ShowNeedToLevelUp()
    {
        //****************
        //这里获取当前经验 杠 升下一级所需总经验，填好后用访问到的东西替换下面那行等式右边
        //*****************
        MainUI.GetChild("playerlvtext").asTextField.text = "200/500";
    }
    public void GetAState(int StateNum, float DuringTime)//获取异常状态时调用此函数，参数一表示状态编号，对应ui上的横向两行1-6，参数二是持续时间，若有类似净化效果，可调用此函数，参数二传0.1f
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
    void EndState(int StateNum)
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
    public List<GButton> GiveChoices(List<string> list)//传入选择内容的list来打开选择，返回对应的gbutton list
    {
        List<GButton> ReturnList = new List<GButton>();
        ReturnList.Clear();
        Choices.visible=true;
        GList ChoiceList= Choices.GetChild("list").asList;
        int i ;
        for (i = 0; i < list.Count; i++)
        {
            GButton a = ChoiceList.AddChild(UIPackage.CreateObject("GamingMain", "choice").asCom).asButton;
            a.GetChild("text").asTextField.text = list[i];
            a.onClick.Add(() => { Choices.visible = false; ChoiceList.RemoveChildren(); });
            ReturnList.Add(a);
        }
        return ReturnList;
    }
}
