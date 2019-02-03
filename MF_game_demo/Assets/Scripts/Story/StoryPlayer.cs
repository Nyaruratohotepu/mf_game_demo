using Assets.Scripts.Story;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


//挂载在对话UI组件上
public class StoryPlayer : MonoBehaviour
{
    public Manager manager;
    private StoryManager storyManager;
    public Image storyChaLeft;
    public Image storyChaRight;
    public Text storyText;
    private CanvasGroup canvasGroup;

    private StorySection story;
    private int nextCMDNum;
    public bool IsPlaying;
    // Use this for initialization
    void Start()
    {

        storyManager = manager.storyManager;
        IsPlaying = false;
        nextCMDNum = 0;
        canvasGroup = gameObject.GetComponentInParent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //收到命令播放某段对话
    public void Play(StorySection storySection)
    {
        story = storySection;
        nextCMDNum = 0;
        IsPlaying = true;
        Show();
        GoOn();
    }

    //继续对话
    public void GoOn()
    {
        if (!IsPlaying) return;

        //取指令
        StoryCMD cmd = story.GetStoryCMD(nextCMDNum);
        if (cmd != null)
        {
            //执行指令
            switch (cmd.Type)
            {
                case StoryItemType.ADDITEM:
                    nextCMDNum = AddItem(cmd as AddItemCMD);
                    break;
                case StoryItemType.ADDITEMNPC:
                case StoryItemType.CHGBGIMG:
                case StoryItemType.CHGCHAIMG:
                case StoryItemType.CHKFLAG:
                case StoryItemType.CHKITEM:
                case StoryItemType.CHKITEMRAG:
                case StoryItemType.DECFLAG:
                case StoryItemType.INCFLAG:
                case StoryItemType.OPTBEG:
                case StoryItemType.OPTEND:
                case StoryItemType.OPTITEM:
                case StoryItemType.RMVITEM:
                case StoryItemType.RMVITEMNPC:
                case StoryItemType.SAY:
                    nextCMDNum = Say(cmd as SayCMD);
                    break;
                case StoryItemType.SETFLAG:
                default:
                    break;
            }
        }
        else
        {
            IsPlaying = false;
            //终了
            Hide();
        }
    }


    //显示对话界面
    private void Show()
    {
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        Time.timeScale = 0f;
        
    }
    //隐藏对话界面
    private void Hide()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        Time.timeScale = 1f;
    }


    //以下为某类指令对应的处理机制，返回下一指令的序号
    private int AddItem(AddItemCMD cmd)
    {
        if (cmd == null)
            return nextCMDNum;

        if (storyManager.AddItemHandle(cmd.ItemId, cmd.Count))
            return cmd.TrueJumpToLine;
        else
            return cmd.FalseJumpToLine;
    }
    private int Say(SayCMD cmd)
    {
        if (cmd == null)
            return nextCMDNum;

        if (cmd.IsFromLeft)
        {
            //淡化右边立绘
            storyChaRight.color = new Color(0.53f,0.53f,0.53f);
            storyChaLeft.color = new Color(1,1,1);
        }
        else
        {
            //淡化左边立绘
            storyChaRight.color = new Color(1,1,1);
            storyChaLeft.color = new Color(0.53f, 0.53f, 0.53f);
        }

        storyText.text = cmd.Text;

        return cmd.JumpToLine;
    }
}
