using Assets.Scripts.Story;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;



//由StoryManager创建
public static class StoryPlayer
{
    private static StoryManager storyManager;
    private static GameingUIController uiController;

    private static StorySection story;
    private static int nextCMDNum = 0;
    public static bool IsPlaying = false;

    //初始化，指定UI控制器和storyManager，必须在最初被storyManager调用
    public static void Init(GameingUIController gamingUIController, StoryManager caller)
    {
        uiController = gamingUIController;
        storyManager = caller;
    }


    //收到命令播放某段对话
    public static void Play(StorySection storySection)
    {
        story = storySection;
        nextCMDNum = 0;
        IsPlaying = true;
        Show();
        GoOn();
    }

    //继续对话
    public static void GoOn()
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
                    nextCMDNum = ChangeBackgroundImage(cmd as ChangeBackgroundImageCMD);
                    break;
                case StoryItemType.CHGCHAIMG:
                    nextCMDNum = ChangeCharacterImage(cmd as ChangeCharacterImageCMD);
                    break;
                case StoryItemType.CHKFLAG:
                    nextCMDNum = CheckFlag(cmd as CheckFlagCMD);
                    break;
                case StoryItemType.CHKITEM:
                case StoryItemType.CHKITEMRAG:
                case StoryItemType.DECFLAG:
                    nextCMDNum = DecreaseFlag(cmd as DecreaseFlagCMD);
                    break;
                case StoryItemType.INCFLAG:
                    nextCMDNum = IncreaseFlag(cmd as IncreaseFlagCMD);
                    break;
                case StoryItemType.OPTBEG:
                    nextCMDNum = OptionBegin(cmd as OptionBeginCMD);
                    break;
                case StoryItemType.OPTEND:
                    nextCMDNum = OptionEnd(cmd as OptionEndCMD);
                    break;
                case StoryItemType.OPTITEM:
                    nextCMDNum = OptionItem(cmd as OptionItemCMD);
                    break;
                case StoryItemType.RMVITEM:
                case StoryItemType.RMVITEMNPC:
                case StoryItemType.SAY:
                    nextCMDNum = Say(cmd as SayCMD);
                    break;
                case StoryItemType.SETFLAG:
                    nextCMDNum = SetFlag(cmd as SetFlagCMD);
                    break;
                default:
                    nextCMDNum++;
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
    private static void Show()
    {
        uiController.TalkActive();
        Time.timeScale = 0f;

    }
    //隐藏对话界面
    private static void Hide()
    {
        uiController.TalkDisable();
        Time.timeScale = 1f;
    }

    //以下为某类指令对应的处理机制，返回下一指令的序号
    //对背包的处理放置到StoryManager中
    //对Flag表直接处理
    private static int AddItem(AddItemCMD cmd)
    {
        if (storyManager.AddItemHandle(cmd.ItemId, cmd.Count))
            return cmd.TrueJumpToLine;
        else
            return cmd.FalseJumpToLine;
    }
    private static int CheckFlag(CheckFlagCMD cmd)
    {
        int value = Flag.GetValue(cmd.FlagName);
        if ((value >= cmd.Min) && (value <= cmd.Max))
            return cmd.TrueJumpToLine;
        else
            return cmd.FalseJumpToLine;
    }
    private static int SetFlag(SetFlagCMD cmd)
    {
        Flag.SetValue(cmd.FlagName, cmd.Value);
        return cmd.JumpToLine;
    }
    private static int IncreaseFlag(IncreaseFlagCMD cmd)
    {
        Flag.IncreaseValue(cmd.FlagName, cmd.Value);
        return cmd.JumpToLine;
    }
    private static int DecreaseFlag(DecreaseFlagCMD cmd)
    {
        Flag.DecreaseValue(cmd.FlagName, cmd.Value);
        return cmd.JumpToLine;
    }
    private static int Say(SayCMD cmd)
    {
        if (cmd.IsFromLeft)
            uiController.ContinueChat(1, story.LeftName, cmd.Text);
        else
            uiController.ContinueChat(2, story.RightName, cmd.Text);
        return cmd.JumpToLine;
    }
    private static int ChangeCharacterImage(ChangeCharacterImageCMD cmd)
    {
        if (cmd.IsLeft)
            uiController.ChangeSpeakerPic(cmd.ImgPath, null);
        else
            uiController.ChangeSpeakerPic(null, cmd.ImgPath);

        return cmd.JumpToLine;
    }
    private static int ChangeBackgroundImage(ChangeBackgroundImageCMD cmd)
    {
        uiController.ChangeBackGround(cmd.ImgPath);
        return cmd.JumpToLine;
    }
    //分支，待完成
    private static int OptionBegin(OptionBeginCMD cmd)
    {
        //分支开始
        return nextCMDNum + 1;
    }
    private static int OptionItem(OptionItemCMD cmd)
    {
        //分支项
        return nextCMDNum + 1;
    }
    private static int OptionEnd(OptionEndCMD cmd)
    {
        //分支读取结束
        return nextCMDNum + 1;
    }
}
