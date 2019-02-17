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

        public StoryManager(GameingUIController uiController)
        {
            storySections = new Dictionary<string, StorySection>();
            StoryPlayer.Init(uiController, this);
        }

        public StorySection GetStorySection(string storySectionName, string leftName, string rightName)
        {
            //首次加载
            if (!storySections.ContainsKey(storySectionName))
            {
                StorySection storySection = new StorySection(storySectionName, leftName, rightName);
                storySections.Add(storySectionName, storySection);
                return storySection;

            }
            return storySections[storySectionName];

        }

        //触发剧情对话时候被调用（如碰撞npc时被npc调用），调用UI组件播放对话
        public void StartStory(StorySection storySection)
        {
            StoryPlayer.Play(storySection);
        }

        //处理剧情指令
        public bool AddItemHandle(int itemId, int count)
        {
            return false;
        }


    }

}
