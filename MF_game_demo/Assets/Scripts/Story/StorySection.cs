using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Story
{
    //StorySection封装一个StoryCMD的序列字典，记录一段对话包含的指令，由StoryPlayer显示
    public class StorySection
    {
        //默认参数分割符号是制表符和空格
        private char[] separators = { '\t', ' ' };

        //指令(包含对话和操作)，从0开始计算序号
        private Dictionary<int, StoryCMD> storyCMDs;
        public string LeftName { get; set; }
        public string RightName { get; set; }

        //构造过程是字符串队列中的每一行转成一个故事指令
        public StorySection(string storySectionName, string leftName, string rightName)
        {
            LeftName = leftName;
            RightName = rightName;

            Queue<string> storyLines = DataAccess.GetStorySectionLines(storySectionName);
            storyCMDs = new Dictionary<int, StoryCMD>();

            while (storyLines.Count > 0)
            {
                //解析命令
                AddStoryCMD(storyLines.Dequeue());
            }

        }

        //对外访问接口

        //由行号获取故事指令，找不到返回null
        public StoryCMD GetStoryCMD(int lineNum)
        {
            if (storyCMDs.ContainsKey(lineNum))
                return storyCMDs[lineNum];
            else return null;
        }

        //字符串转故事指令
        private bool AddStoryCMD(string cmdString)
        {
            if (cmdString[0] != '@') return false;
            string[] values = cmdString.Split(separators);
            switch (values[0].ToUpper())
            {
                //玩家背包加物品
                case "@ADDITEM":
                    return ADDITEM(values);
                //玩家背包扣物品
                case "@RMVITEM":
                    return RMVITEM(values);
                //检查玩家背包有无某物品
                case "@CHKITEM":
                    return CHKITEM(values);
                //检查玩家背包有无某物体（符合数量）
                case "@CHKITEMRAG":
                    return CHKITEMRAG(values);
                //检查某FLAG值是否在范围内
                case "@CHKFLAG":
                    return CHKFLAG(values);
                //设定某FLAG值
                case "@SETGLAG":
                    return SETFLAG(values);
                //某FLAG值++
                case "@INCFLAG":
                    return INCFLAG(values);
                //某FLAG值--
                case "@DECFLAG":
                    return DECFLAG(values);
                //给正在对话的NPC背包加入某物体
                case "@ADDITEMNPC":
                    return ADDITEMNPC(values);
                //给正在对话的NPC背包减去某物体
                case "@RMVITEMNPC":
                    return RMVITEMNPC(values);
                //对话框显示文字
                case "@SAY":
                    return SAY(values);
                //改变人物立绘
                case "@CHGCHAIMG":
                    return CHGCHAIMG(values);
                //改变背景图片
                case "@CHGBGIMG":
                    return CHGBGIMG(values);
                //分支开始
                case "@OPTBEG":
                    return OPTBEG(values);
                //分支项
                case "@OPTITEM":
                    return OPTITEM(values);
                //分支结束
                case "@OPTEND":
                    return OPTEND(values);
                //无法解析的命令
                default:
                    return false;
            }
        }

        //以下若干函数均用来从字符串中获取对应故事指令参数列表
        private bool ADDITEM(string[] values)
        {
            //至少包含3个项（含命令+2个必要参数）
            if (values.Length < 3) return false;
            //这次的行号
            int lineNum = storyCMDs.Count;
            int itemId, count, falseJumpOffset, trueJumpOffset;

            if (!int.TryParse(values[1], out itemId)) return false;
            if (!int.TryParse(values[2], out count)) return false;

            switch (values.Length)
            {
                //两参数时
                case 3:
                    storyCMDs.Add(lineNum, new AddItemCMD(lineNum, itemId, count));
                    return true;

                //三参数时
                case 4:
                    if (!int.TryParse(values[3], out falseJumpOffset)) return false;
                    storyCMDs.Add(lineNum, new AddItemCMD(lineNum, itemId, count, falseJumpOffset));
                    return true;

                //四参数及更多时
                default:

                    if (!int.TryParse(values[3], out falseJumpOffset)) return false;
                    if (!int.TryParse(values[4], out trueJumpOffset)) return false;
                    storyCMDs.Add(lineNum, new AddItemCMD(lineNum, itemId, count, falseJumpOffset, trueJumpOffset));
                    return true;
            }
        }
        private bool RMVITEM(string[] values)
        {
            //至少包含3个项（含命令+2个必要参数）
            if (values.Length < 3) return false;
            //这次的行号
            int lineNum = storyCMDs.Count;
            int itemId, count, falseJumpOffset, trueJumpOffset;

            if (!int.TryParse(values[1], out itemId)) return false;
            if (!int.TryParse(values[2], out count)) return false;

            switch (values.Length)
            {
                //两参数时
                case 3:

                    storyCMDs.Add(lineNum, new RemoveItemCMD(lineNum, itemId, count));
                    return true;

                //三参数时
                case 4:
                    if (!int.TryParse(values[3], out falseJumpOffset)) return false;
                    storyCMDs.Add(lineNum, new RemoveItemCMD(lineNum, itemId, count, falseJumpOffset));
                    return true;

                //四参数及更多时
                default:
                    if (!int.TryParse(values[3], out falseJumpOffset)) return false;
                    if (!int.TryParse(values[4], out trueJumpOffset)) return false;
                    storyCMDs.Add(lineNum, new RemoveItemCMD(lineNum, itemId, count, falseJumpOffset, trueJumpOffset));
                    return true;
            }
        }
        private bool ADDITEMNPC(string[] values)
        {
            //至少包含3个项（含命令+2个必要参数）
            if (values.Length < 3) return false;
            //这次的行号
            int lineNum = storyCMDs.Count;
            int itemId, count, falseJumpOffset, trueJumpOffset;

            if (!int.TryParse(values[1], out itemId)) return false;
            if (!int.TryParse(values[2], out count)) return false;

            switch (values.Length)
            {
                //两参数时
                case 3:

                    storyCMDs.Add(lineNum, new AddItemNpcCMD(lineNum, itemId, count));
                    return true;

                //三参数时
                case 4:
                    if (!int.TryParse(values[3], out falseJumpOffset)) return false;
                    storyCMDs.Add(lineNum, new AddItemNpcCMD(lineNum, itemId, count, falseJumpOffset));
                    return true;

                //四参数及更多时
                default:
                    if (!int.TryParse(values[3], out falseJumpOffset)) return false;
                    if (!int.TryParse(values[4], out trueJumpOffset)) return false;
                    storyCMDs.Add(lineNum, new AddItemNpcCMD(lineNum, itemId, count, falseJumpOffset, trueJumpOffset));
                    return true;
            }
        }
        private bool RMVITEMNPC(string[] values)
        {
            //至少包含3个项（含命令+2个必要参数）
            if (values.Length < 3) return false;
            //这次的行号
            int lineNum = storyCMDs.Count;
            int itemId, count, falseJumpOffset, trueJumpOffset;

            if (!int.TryParse(values[1], out itemId)) return false;
            if (!int.TryParse(values[2], out count)) return false;

            switch (values.Length)
            {
                //两参数时
                case 3:
                    storyCMDs.Add(lineNum, new RemoveItemNpcCMD(lineNum, itemId, count));
                    return true;

                //三参数时
                case 4:

                    if (!int.TryParse(values[3], out falseJumpOffset)) return false;
                    storyCMDs.Add(lineNum, new RemoveItemNpcCMD(lineNum, itemId, count, falseJumpOffset));
                    return true;

                //四参数及更多时
                default:
                    if (!int.TryParse(values[3], out falseJumpOffset)) return false;
                    if (!int.TryParse(values[4], out trueJumpOffset)) return false;
                    storyCMDs.Add(lineNum, new RemoveItemNpcCMD(lineNum, itemId, count, falseJumpOffset, trueJumpOffset));
                    return true;
            }
        }
        private bool CHKITEM(string[] values)
        {
            //至少包含2个项（含命令+1个必要参数）
            if (values.Length < 2) return false;
            //这次的行号
            int lineNum = storyCMDs.Count;
            int itemId, falseJumpOffset, trueJumpOffset;

            switch (values.Length)
            {
                //一参数时
                case 2:
                    if (!int.TryParse(values[1], out itemId)) return false;
                    storyCMDs.Add(lineNum, new CheckItemCMD(lineNum, itemId));
                    return true;

                //二参数时
                case 3:
                    if (!int.TryParse(values[1], out itemId)) return false;
                    if (!int.TryParse(values[2], out falseJumpOffset)) return false;
                    storyCMDs.Add(lineNum, new CheckItemCMD(lineNum, itemId, falseJumpOffset));
                    return true;

                //三参数及更多时
                default:
                    if (!int.TryParse(values[1], out itemId)) return false;
                    if (!int.TryParse(values[2], out falseJumpOffset)) return false;
                    if (!int.TryParse(values[3], out trueJumpOffset)) return false;
                    storyCMDs.Add(lineNum, new CheckItemCMD(lineNum, itemId, falseJumpOffset, trueJumpOffset));
                    return true;

            }
        }
        private bool CHKITEMRAG(string[] values)
        {
            //至少包含4个项（含命令+3个必要参数）
            if (values.Length < 4) return false;
            //这次的行号
            int lineNum = storyCMDs.Count;
            int itemId, min, max, falseJumpOffset, trueJumpOffset;

            if (!int.TryParse(values[1], out itemId)) return false;
            if (!int.TryParse(values[2], out min)) return false;
            if (!int.TryParse(values[3], out max)) return false;

            switch (values.Length)
            {
                //三参数时
                case 4:
                    storyCMDs.Add(lineNum, new CheckItemRangeCMD(lineNum, itemId, min, max));
                    return true;

                //四参数时
                case 5:
                    if (!int.TryParse(values[4], out falseJumpOffset)) return false;
                    storyCMDs.Add(lineNum, new CheckItemRangeCMD(lineNum, itemId, min, max, falseJumpOffset));
                    return true;

                //五参数及更多时
                default:
                    if (!int.TryParse(values[4], out falseJumpOffset)) return false;
                    if (!int.TryParse(values[5], out trueJumpOffset)) return false;
                    storyCMDs.Add(lineNum, new CheckItemRangeCMD(lineNum, itemId, min, max, falseJumpOffset, trueJumpOffset));
                    return true;

            }
        }
        private bool CHKFLAG(string[] values)
        {
            //至少包含4个项（含命令+3个必要参数）
            if (values.Length < 4) return false;
            //这次的行号
            int lineNum = storyCMDs.Count;
            string flagName = values[1];
            int min, max, falseJumpOffset, trueJumpOffset;

            if (flagName.Length == 0) return false;
            if (!int.TryParse(values[2], out min)) return false;
            if (!int.TryParse(values[3], out max)) return false;

            switch (values.Length)
            {
                //三参数时
                case 4:
                    storyCMDs.Add(lineNum, new CheckFlagCMD(lineNum, flagName, min, max));
                    return true;

                //四参数时
                case 5:
                    if (!int.TryParse(values[4], out falseJumpOffset)) return false;
                    storyCMDs.Add(lineNum, new CheckFlagCMD(lineNum, flagName, min, max, falseJumpOffset));
                    return true;

                //五参数及更多时
                default:
                    if (!int.TryParse(values[4], out falseJumpOffset)) return false;
                    if (!int.TryParse(values[5], out trueJumpOffset)) return false;
                    storyCMDs.Add(lineNum, new CheckFlagCMD(lineNum, flagName, min, max, falseJumpOffset, trueJumpOffset));
                    return true;

            }
        }
        private bool SETFLAG(string[] values)
        {
            //至少包含3个项（含命令+2个必要参数）
            if (values.Length < 3) return false;
            //这次的行号
            int lineNum = storyCMDs.Count;
            string flagName = values[1];
            int value, jumpOffset;

            if (flagName.Length == 0) return false;
            if (!int.TryParse(values[2], out value)) return false;

            switch (values.Length)
            {
                //二参数时
                case 2:
                    storyCMDs.Add(lineNum, new SetFlagCMD(lineNum, flagName, value));
                    return true;

                //三参数及更多时
                default:
                    if (!int.TryParse(values[3], out jumpOffset)) return false;
                    storyCMDs.Add(lineNum, new SetFlagCMD(lineNum, flagName, value, jumpOffset));
                    return true;

            }
        }
        private bool INCFLAG(string[] values)
        {
            //至少包含3个项（含命令+2个必要参数）
            if (values.Length < 3) return false;
            //这次的行号
            int lineNum = storyCMDs.Count;
            string flagName = values[1];
            int value, jumpOffset;

            if (flagName.Length == 0) return false;
            if (!int.TryParse(values[2], out value)) return false;

            switch (values.Length)
            {
                //二参数时
                case 2:
                    storyCMDs.Add(lineNum, new IncreaseFlagCMD(lineNum, flagName, value));
                    return true;

                //三参数及更多时
                default:
                    if (!int.TryParse(values[3], out jumpOffset)) return false;
                    storyCMDs.Add(lineNum, new IncreaseFlagCMD(lineNum, flagName, value, jumpOffset));
                    return true;

            }
        }
        private bool DECFLAG(string[] values)
        {
            //至少包含3个项（含命令+2个必要参数）
            if (values.Length < 3) return false;
            //这次的行号
            int lineNum = storyCMDs.Count;
            string flagName = values[1];
            int value, jumpOffset;

            if (flagName.Length == 0) return false;
            if (!int.TryParse(values[2], out value)) return false;

            switch (values.Length)
            {
                //二参数时
                case 2:
                    storyCMDs.Add(lineNum, new DecreaseFlagCMD(lineNum, flagName, value));
                    return true;

                //三参数及更多时
                default:
                    if (!int.TryParse(values[3], out jumpOffset)) return false;
                    storyCMDs.Add(lineNum, new DecreaseFlagCMD(lineNum, flagName, value, jumpOffset));
                    return true;

            }
        }
        private bool SAY(string[] values)
        {
            //至少2项（一个参数）
            if (values.Length < 2) return false;
            int lineNum = storyCMDs.Count;

            int left;
            bool isFromLeft;

            int jumpToLineOffset;

            switch (values.Length)
            {
                //1参数
                case 2:
                    storyCMDs.Add(lineNum, new SayCMD(lineNum, values[1]));
                    return true;

                //2参数
                case 3:
                    if (!int.TryParse(values[2], out left)) return false;
                    isFromLeft = Convert.ToBoolean(left);
                    storyCMDs.Add(lineNum, new SayCMD(lineNum, values[1], isFromLeft));
                    return true;


                //3个参数时
                default:
                    if (!int.TryParse(values[2], out left)) return false;
                    if (!int.TryParse(values[3], out jumpToLineOffset)) return false;
                    //非0转true
                    isFromLeft = Convert.ToBoolean(left);
                    storyCMDs.Add(lineNum, new SayCMD(lineNum, values[1], isFromLeft, jumpToLineOffset));
                    return true;

            }
        }
        private bool CHGCHAIMG(string[] values)
        {
            //至少包含2个项（含命令+1个必要参数）
            if (values.Length < 2) return false;

            int lineNum = storyCMDs.Count;
            string imgPath = values[1];
            bool isLeft;
            int jumpOffset;

            switch (values.Length)
            {
                case 2:
                    storyCMDs.Add(lineNum, new ChangeCharacterImageCMD(lineNum, imgPath));
                    return true;
                case 3:
                    if (!bool.TryParse(values[2], out isLeft)) return false;
                    storyCMDs.Add(lineNum, new ChangeCharacterImageCMD(lineNum, imgPath, isLeft));
                    return true;
                default:
                    if (!bool.TryParse(values[2], out isLeft)) return false;
                    if (!int.TryParse(values[3], out jumpOffset)) return false;
                    storyCMDs.Add(lineNum, new ChangeCharacterImageCMD(lineNum, imgPath, isLeft, jumpOffset));
                    return true;
            }
        }
        private bool CHGBGIMG(string[] values)
        {
            //至少包含2个项（含命令+1个必要参数）
            if (values.Length < 2) return false;

            int lineNum = storyCMDs.Count;
            string imgPath = values[1];
            int jumpOffset;

            switch (values.Length)
            {
                case 2:
                    storyCMDs.Add(lineNum, new ChangeBackgroundImageCMD(lineNum, imgPath));
                    return true;
                default:
                    if (!int.TryParse(values[2], out jumpOffset)) return false;
                    storyCMDs.Add(lineNum, new ChangeBackgroundImageCMD(lineNum, imgPath, jumpOffset));
                    return true;
            }
        }
        private bool OPTBEG(string[] values)
        {
            int lineNum = storyCMDs.Count;
            storyCMDs.Add(lineNum, new OptionBeginCMD());
            return true;
        }
        private bool OPTITEM(string[] values)
        {
            //至少包含2个项（含命令+1个必要参数）
            if (values.Length < 2) return false;

            int lineNum = storyCMDs.Count;
            string text = values[1];
            int jumpOffset;

            switch (values.Length)
            {
                case 2:
                    storyCMDs.Add(lineNum, new OptionItemCMD(lineNum, text));
                    return true;
                default:
                    if (!int.TryParse(values[2], out jumpOffset)) return false;
                    storyCMDs.Add(lineNum, new OptionItemCMD(lineNum, text, jumpOffset));
                    return true;
            }
        }
        private bool OPTEND(string[] values)
        {
            int lineNum = storyCMDs.Count;
            storyCMDs.Add(lineNum, new OptionEndCMD());
            return true;
        }
    }

    //一条剧情控制指令
    public class StoryCMD
    {
        //类型
        public StoryItemType Type { get; set; }
    }

    //剧情控制指令类型
    public enum StoryItemType
    {
        //玩家背包加物品
        ADDITEM,
        //玩家背包扣物品
        RMVITEM,
        //检查玩家背包有无某物品
        CHKITEM,
        //检查玩家背包有无某物体（符合数量）
        CHKITEMRAG,
        //检查某FLAG值是否在范围内
        CHKFLAG,
        //设定某FLAG值
        SETFLAG,
        //某FLAG值++
        INCFLAG,
        //某FLAG值--
        DECFLAG,
        //给正在对话的NPC背包加入某物体
        ADDITEMNPC,
        //给正在对话的NPC背包减去某物体
        RMVITEMNPC,
        //对话框显示文字
        SAY,
        //改变人物立绘
        CHGCHAIMG,
        //改变背景图片
        CHGBGIMG,
        //分支开始
        OPTBEG,
        //分支项
        OPTITEM,
        //分支结束
        OPTEND
    }

    public class AddItemCMD : StoryCMD
    {
        public int ItemId { get; set; }
        public int Count { get; set; }
        public int FalseJumpToLine { get; set; }
        public int TrueJumpToLine { get; set; }

        public AddItemCMD(int lineNum, int itemId, int count, int falseJumpOffset = 1, int trueJumpOffset = 1)
        {
            //lineNum传入但不存在类中，只用来构造跳转结果

            Type = StoryItemType.ADDITEM;

            ItemId = itemId;
            Count = count;

            //添加失败，向后跳转多少行，无效、缺省为跳一行
            //相对值转绝对行号
            FalseJumpToLine = falseJumpOffset + lineNum;

            //添加成功，向后跳转多少行，无效、缺省为跳一行
            TrueJumpToLine = trueJumpOffset + lineNum;
        }
    }
    public class RemoveItemCMD : StoryCMD
    {
        public int ItemId { get; set; }
        public int Count { get; set; }
        public int FalseJumpToLine { get; set; }
        public int TrueJumpToLine { get; set; }

        public RemoveItemCMD(int lineNum, int itemId, int count, int falseJumpOffset = 1, int trueJumpOffset = 1)
        {
            //lineNum传入但不存在类中，只用来构造跳转结果

            Type = StoryItemType.RMVITEM;

            ItemId = itemId;
            Count = count;

            //减少失败，向后跳转多少行，无效、缺省为跳一行
            //相对值转绝对行号
            FalseJumpToLine = falseJumpOffset + lineNum;

            //减少成功，向后跳转多少行，无效、缺省为跳一行
            TrueJumpToLine = trueJumpOffset + lineNum;
        }
    }
    public class CheckItemCMD : StoryCMD
    {
        public int ItemId { get; set; }
        public int FalseJumpToLine { get; set; }
        public int TrueJumpToLine { get; set; }
        public CheckItemCMD(int lineNum, int itemId, int falseJumpOffset = 1, int trueJumpOffset = 1)
        {
            Type = StoryItemType.CHKITEM;

            ItemId = itemId;
            FalseJumpToLine = falseJumpOffset + lineNum;
            TrueJumpToLine = trueJumpOffset + lineNum;
        }
    }
    public class CheckItemRangeCMD : StoryCMD
    {
        public int ItemId { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }
        public int FalseJumpToLine { get; set; }
        public int TrueJumpToLine { get; set; }
        public CheckItemRangeCMD(int lineNum, int itemId, int min, int max, int falseJumpOffset = 1, int trueJumpOffset = 1)
        {
            Type = StoryItemType.CHKITEMRAG;

            ItemId = itemId;
            Min = min;
            Max = max;
            FalseJumpToLine = falseJumpOffset + lineNum;
            TrueJumpToLine = trueJumpOffset + lineNum;
        }
    }
    public class CheckFlagCMD : StoryCMD
    {
        public string FlagName { set; get; }
        public int Min { set; get; }
        public int Max { set; get; }
        public int FalseJumpToLine { get; set; }
        public int TrueJumpToLine { get; set; }
        public CheckFlagCMD(int lineNum, string flagName, int min, int max, int falseJumpOffset = 1, int trueJumpOffset = 1)
        {
            Type = StoryItemType.CHKFLAG;

            FlagName = flagName;
            Min = min;
            Max = max;

            FalseJumpToLine = falseJumpOffset + lineNum;
            TrueJumpToLine = trueJumpOffset + lineNum;
        }
    }
    public class SetFlagCMD : StoryCMD
    {
        public string FlagName { set; get; }
        public int Value { set; get; }
        public int JumpToLine { get; set; }
        public SetFlagCMD(int lineNum, string flagName, int value, int jumpOffset = 1)
        {
            Type = StoryItemType.SETFLAG;

            FlagName = flagName;
            Value = value;

            JumpToLine = jumpOffset + lineNum;
        }
    }
    public class IncreaseFlagCMD : StoryCMD
    {
        public string FlagName { set; get; }
        public int Value { set; get; }
        public int JumpToLine { get; set; }
        public IncreaseFlagCMD(int lineNum, string flagName, int value, int jumpOffset = 1)
        {
            Type = StoryItemType.INCFLAG;

            FlagName = flagName;
            Value = value;

            JumpToLine = jumpOffset + lineNum;
        }
    }
    public class DecreaseFlagCMD : StoryCMD
    {
        public string FlagName { set; get; }
        public int Value { set; get; }
        public int JumpToLine { get; set; }
        public DecreaseFlagCMD(int lineNum, string flagName, int value, int jumpOffset = 1)
        {
            Type = StoryItemType.DECFLAG;

            FlagName = flagName;
            Value = value;

            JumpToLine = jumpOffset + lineNum;
        }
    }
    public class AddItemNpcCMD : StoryCMD
    {
        public int ItemId { get; set; }
        public int Count { get; set; }
        public int FalseJumpToLine { get; set; }
        public int TrueJumpToLine { get; set; }

        public AddItemNpcCMD(int lineNum, int itemId, int count, int falseJumpOffset = 1, int trueJumpOffset = 1)
        {
            Type = StoryItemType.ADDITEMNPC;

            ItemId = itemId;
            Count = count;

            FalseJumpToLine = falseJumpOffset + lineNum;
            TrueJumpToLine = trueJumpOffset + lineNum;
        }
    }
    public class RemoveItemNpcCMD : StoryCMD
    {
        public int ItemId { get; set; }
        public int Count { get; set; }
        public int FalseJumpToLine { get; set; }
        public int TrueJumpToLine { get; set; }

        public RemoveItemNpcCMD(int lineNum, int itemId, int count, int falseJumpOffset = 1, int trueJumpOffset = 1)
        {
            Type = StoryItemType.RMVITEMNPC;

            ItemId = itemId;
            Count = count;

            FalseJumpToLine = falseJumpOffset + lineNum;
            TrueJumpToLine = trueJumpOffset + lineNum;
        }
    }
    public class SayCMD : StoryCMD
    {
        //文本
        public string Text { get; set; }
        //是否是左边的人在说话
        public bool IsFromLeft { get; set; }
        //下一跳
        public int JumpToLine { get; set; }
        public SayCMD(int lineNum, string text, bool isFromLeft = true, int jumpOffset = 1)
        {
            Type = StoryItemType.SAY;
            Text = text;
            IsFromLeft = isFromLeft;
            JumpToLine = jumpOffset + lineNum;
        }
    }
    public class ChangeCharacterImageCMD : StoryCMD
    {
        public bool IsLeft { set; get; }
        public string ImgPath { set; get; }
        public int JumpToLine { set; get; }
        public ChangeCharacterImageCMD(int lineNum, string imgPath, bool isLeft = true, int jumpOffset = 1)
        {
            Type = StoryItemType.CHGCHAIMG;

            ImgPath = imgPath;
            IsLeft = isLeft;

            JumpToLine = jumpOffset + lineNum;
        }
    }
    public class ChangeBackgroundImageCMD : StoryCMD
    {
        public string ImgPath { set; get; }
        public int JumpToLine { set; get; }
        public ChangeBackgroundImageCMD(int lineNum, string imgPath, int jumpOffset = 1)
        {
            Type = StoryItemType.CHGBGIMG;
            ImgPath = imgPath;
            JumpToLine = jumpOffset + lineNum;
        }
    }
    public class OptionBeginCMD : StoryCMD
    {
        public OptionBeginCMD()
        {
            Type = StoryItemType.OPTBEG;
        }
    }
    public class OptionItemCMD : StoryCMD
    {
        public string Text { set; get; }
        public int JumpToLine { set; get; }
        public OptionItemCMD(int lineNum, string text, int jumpOffset = 1)
        {
            Type = StoryItemType.OPTITEM;

            Text = text;
            JumpToLine = jumpOffset + lineNum;
        }
    }
    public class OptionEndCMD : StoryCMD
    {
        public OptionEndCMD()
        {
            Type = StoryItemType.OPTEND;
        }
    }
}
