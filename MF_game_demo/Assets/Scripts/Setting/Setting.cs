using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts.Setting
{
    public static class Setting
    {
        //存储路径相关的设置
        private static Dictionary<string, string> pathSettings;
        //存储显示相关的设置
        private static Dictionary<string, string> displaySettings;

        private static bool loaded = false;

        //setting文件位置（固定）
        public static string SettingPath = Application.dataPath + "/setting.xml";

        //加载设定文件
        private static void LoadSetting()
        {
            pathSettings = new Dictionary<string, string>();
            displaySettings = new Dictionary<string, string>();

            XmlDocument settingXml = new XmlDocument();
            settingXml.Load(SettingPath);

            //读取Path
            XmlNodeList pathNodelist = settingXml.SelectNodes("/Setting/PathSetting/Path");
            if (pathNodelist != null)
            {
                foreach (XmlNode pathNode in pathNodelist)
                {
                    pathSettings.Add(pathNode["PathName"].InnerText, pathNode["Value"].InnerText);
                }
            }

            //读取显示设置
            XmlNodeList displayNodelist = settingXml.SelectNodes("/Setting/DisplaySetting/Display");
            if (displayNodelist != null)
            {
                foreach (XmlNode displayNode in displayNodelist)
                {
                    displaySettings.Add(displayNode["Key"].InnerText, displayNode["Value"].InnerText);
                }
            }

            loaded = true;
        }

        public static string GetPathValue(string pathSettingName)
        {
            if (!loaded) LoadSetting();
            try
            {
                return pathSettings[pathSettingName];
            }
            catch
            {
                return null;
            }
        }

        public static string GetDisplayValue(string displaySettingName)
        {
            if (!loaded) LoadSetting();
            try
            {
                return displaySettings[displaySettingName];
            }
            catch
            {
                return null;
            }
        }

    }

}
