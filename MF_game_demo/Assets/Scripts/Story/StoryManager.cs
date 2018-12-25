using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Story
{
    public class StoryManager
    {
        //key:故事名 
        private Dictionary<string, StorySection> storySections;
        //标志表（FLAG），用来存储故事上下文信息
        private Dictionary<string, int> flags;


        // StorySectionPlayer由Manager在Awake中填写
        public StoryPlayer StorySectionPlayer { set; get; }

        public StoryManager()
        {
            storySections = new Dictionary<string, StorySection>();
            //flags以后换成从存档文件里面读取
            flags = new Dictionary<string, int>();
            // StorySectionPlayer由Manager在Awake中填写
        }

        public StorySection GetStorySection(string storySectionName)
        {
            //首次加载
            if (!storySections.ContainsKey(storySectionName))
            {
                StorySection storySection = new StorySection(storySectionName);
                storySections.Add(storySectionName, storySection);
                return storySection;

            }
            return storySections[storySectionName];

        }

        //调用UI组件播放对话
        public void StartStory(StorySection storySection)
        {
            StorySectionPlayer.Play(storySection);
        }
    }

}
