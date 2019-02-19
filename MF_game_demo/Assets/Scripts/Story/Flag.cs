using System.Collections.Generic;
using System.Xml;
using UnityEngine;

namespace Assets.Scripts.Story
{
    public static class Flag
    {
        private static Dictionary<string, int> flags;
        private static bool loaded = false;
        private static void LoadFlag()
        {
            flags = new Dictionary<string, int>();
            XmlDocument flagXml = new XmlDocument();

            //获取flag文件地址，加载
            flagXml.Load(Application.dataPath + Setting.Setting.GetPathValue("FlagFile"));
            XmlNodeList flagNodelist = flagXml.SelectSingleNode("/Flags").ChildNodes;

            foreach (XmlNode flag in flagNodelist)
            {
                string flagName = flag["FlagName"].InnerText;
                int value;
                if (!int.TryParse(flag["Value"].InnerText, out value)) break;
                flags.Add(flagName, value);
            }

            loaded = true;
        }
        public static void SaveFlag()
        {
            flags = new Dictionary<string, int>();
            XmlDocument flagXml = new XmlDocument();

            //获取flag文件地址，加载
            flagXml.Load(Application.dataPath + Setting.Setting.GetPathValue("FlagFile"));
            XmlNode flagNode = flagXml.DocumentElement;
            flagNode.RemoveAll();
            //更新xml文档
            foreach (KeyValuePair<string, int> flag in flags)
            {
                XmlNode flagNew = flagXml.CreateNode(XmlNodeType.Element, "Flag", "");
                flagNode.AppendChild(flagNew);

                XmlNode flagNameNew = flagXml.CreateNode(XmlNodeType.Element, "FlagName", "");
                flagNameNew.InnerText = flag.Key;

                XmlNode valueNew = flagXml.CreateNode(XmlNodeType.Element, "Value", "");
                flagNameNew.InnerText = flag.Value.ToString();

                flagNew.AppendChild(flagNameNew);
                flagNew.AppendChild(valueNew);

            }
            //保存
            flagXml.Save(Application.dataPath + Setting.Setting.GetPathValue("FlagFile"));
        }

        public static int GetValue(string flagName)
        {
            if (!loaded) LoadFlag();
            if (flags.ContainsKey(flagName))
            {
                return flags[flagName];
            }
            else
            {
                return 0;
            }
        }
        public static void SetValue(string flagName, int value)
        {
            if (!loaded) LoadFlag();
            if (flags.ContainsKey(flagName))
            {
                flags[flagName] = value;
            }
            else
            {
                flags.Add(flagName, value);
            }
        }

        public static void IncreaseValue(string flagName, int deltaValue)
        {
            if (!loaded) LoadFlag();
            if (flags.ContainsKey(flagName))
            {
                flags[flagName] += deltaValue;
            }
        }

        public static void DecreaseValue(string flagName, int deltaValue)
        {
            if (!loaded) LoadFlag();
            if (flags.ContainsKey(flagName))
            {
                flags[flagName] -= deltaValue;
            }
        }


    }
}
