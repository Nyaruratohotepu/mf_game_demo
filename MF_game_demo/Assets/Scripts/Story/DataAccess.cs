﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Story
{
    public static class DataAccess
    {
        public static string storyFolderPath = Application.dataPath + "/Resources/Story/";
        //后缀
        public static string suffix = ".txt";
        public static Queue<string> GetStorySectionLines(string storySectionName)
        {
            Queue<string> storyLines = new Queue<string>();
            try
            {
                StreamReader sr = new StreamReader(storyFolderPath + storySectionName + suffix);
                string storyLine;
                while ((storyLine = sr.ReadLine()) != null)
                {
                    storyLines.Enqueue(storyLine);
                }
            }
            catch
            {

            }
            return storyLines;
        }
    }
}