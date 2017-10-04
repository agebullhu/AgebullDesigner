using System;
using System.Collections.Generic;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public class MomentCoder
    {
        private static readonly List<string> types = new List<string>();

        public static readonly Dictionary<string, Func<ConfigBase, string>> FindDictionary = new Dictionary<string, Func<ConfigBase, string>>();

        public static readonly Dictionary<string, Dictionary<string, Func<ConfigBase, string>>> coders = new Dictionary<string, Dictionary<string, Func<ConfigBase, string>>>();
        public static List<string> Types => types;


        public static string CreateCode(string type, ConfigBase cfg)
        {
            if (type != "-" && coders.ContainsKey(type))
                return FindDictionary[type](cfg);
            return "����Чѡ��";
        }
        public static void RegisteType(string type)
        {
            if (!coders.ContainsKey(type))
                coders.Add(type, null);
        }
        public static void RegisteLine()
        {
            types.Add("-");
        }

        public static string NowType { get; set; } = "δ����";
        public static void RegisteCoder(string name, Func<ConfigBase, string> func)
        {
            RegisteCoder(NowType, name, func);
        }

        public static void RegisteCoder(string type, string name, Func<ConfigBase, string> func)
        {
            NowType = type ?? "δ����";
            if (name == "-")
            {
                return;
            }

            if (!coders.ContainsKey(NowType))
                coders.Add(NowType, new Dictionary<string, Func<ConfigBase, string>>());

            if (coders[NowType].ContainsKey(name))
                coders[NowType][name] = func;
            else
                coders[NowType].Add(name, func);


            if (FindDictionary.ContainsKey(name))
                FindDictionary[name] = func;
            else
                FindDictionary.Add(name, func);

            types.Add(name);
        }
        

    }
}