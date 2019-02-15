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



        // StorySectionPlayer由Manager在Awake中填写
        public StoryPlayer StorySectionPlayer { set; get; }

        public StoryManager()
        {
            storySections = new Dictionary<string, StorySection>();
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

        //触发剧情对话时候被调用（如碰撞npc时被npc调用），调用UI组件播放对话
        public void StartStory(StorySection storySection)
        {
            StorySectionPlayer.Play(storySection);
        }

        //处理剧情指令
        public bool AddItemHandle(int itemId, int count)
        {
            return false;
        }


    }

}
