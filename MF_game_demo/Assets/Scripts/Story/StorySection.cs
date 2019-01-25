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


        //构造过程是字符串队列中的每一行转成一个故事指令
        public StorySection(string storySectionName)
        {
            Queue<string> storyLines = DataAccess.GetStorySectionLines(storySectionName);
            storyCMDs = new Dictionary<int, StoryCMD>();


            List<int> errorLinesNum = new List<int>();

            for (int i = 0; i < storyLines.Count; i++)
            {
                //解析命令，记录错误行号
                if (!AddStoryCMD(storyLines.Dequeue()))
                    errorLinesNum.Add(i);
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
                    return addADDITEM(values);
                //玩家背包扣物品
                case "@RMVITEM":
                //检查玩家背包有无某物品
                case "@CHKITEM":
                //检查玩家背包有无某物体（符合数量）
                case "@CHKITEMRAG":
                //检查某FLAG值是否在范围内
                case "@CHKFLAG":
                //设定某FLAG值
                case "@SETGLAG":
                //某FLAG值++
                case "@INCFLAG":
                //某FLAG值--
                case "@DECFLAG":
                //给正在对话的NPC背包加入某物体
                case "@ADDITEMNPC":
                //给正在对话的NPC背包减去某物体
                case "@RMVITEMNPC":
                //对话框显示文字
                case "@SAY":
                    return addSAY(values);
                //改变人物立绘
                case "@CHGCHAIMG":
                //改变背景图片
                case "@CHGBGIMG":
                //分支开始
                case "@OPTBEG":
                //分支项
                case "@OPTITEM":
                //分支结束
                case "@OPTEND":

                //无法解析的命令
                default:
                    return false;
            }
        }

        //以下若干函数均用来从字符串中获取对应故事指令参数列表
        private bool addADDITEM(string[] values)
        {
            //至少包含3个项（含命令+2个必要参数）
            if (values.Length < 3) return false;
            //这次的行号
            int lineNum = storyCMDs.Count;
            int itemId, count, falseJumpOffset, trueJumpOffset;

            switch (values.Length)
            {
                //两参数时
                case 3:
                    if (!int.TryParse(values[1], out itemId)) return false;
                    if (!int.TryParse(values[2], out count)) return false;
                    storyCMDs.Add(lineNum, new AddItemCMD(lineNum, itemId, count));
                    return true;

                //三参数时
                case 4:
                    if (!int.TryParse(values[1], out itemId)) return false;
                    if (!int.TryParse(values[2], out count)) return false;
                    if (!int.TryParse(values[3], out falseJumpOffset)) return false;
                    storyCMDs.Add(lineNum, new AddItemCMD(lineNum, itemId, count, falseJumpOffset));
                    return true;

                //四参数及更多时
                default:
                    if (!int.TryParse(values[1], out itemId)) return false;
                    if (!int.TryParse(values[2], out count)) return false;
                    if (!int.TryParse(values[3], out falseJumpOffset)) return false;
                    if (!int.TryParse(values[4], out trueJumpOffset)) return false;
                    storyCMDs.Add(lineNum, new AddItemCMD(lineNum, itemId, count, falseJumpOffset, trueJumpOffset));
                    return true;
            }
        }
        private bool addSAY(string[] values)
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
        SETGLAG,
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
}
