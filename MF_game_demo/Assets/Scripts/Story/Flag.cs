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

        public static int GetValue(string FlagName)
        {
            if (!loaded) LoadFlag();
            try
            {
                return flags[FlagName];
            }
            catch
            {
                return 0;
            }
        }
            
    }
}
