using System;
using System.Collections.Generic;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    public class CoderDefine
    {
        public Func<ConfigBase, string> Func { get; set; }

        public string Name { get; set; }
        public string Lang { get; set; }
    }
    public class MomentCoder
    {
        private static readonly List<string> types = new List<string>();

        public static readonly Dictionary<string, CoderDefine> FindDictionary = new Dictionary<string, CoderDefine>();

        public static readonly Dictionary<string, Dictionary<string, CoderDefine>> coders = new Dictionary<string, Dictionary<string, CoderDefine>>();
        public static List<string> Types => types;


        public static string CreateCode(string type, ConfigBase cfg)
        {
            if (type != "-" && coders.ContainsKey(type))
                return FindDictionary[type].Func(cfg);
            return "无有效选择";
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

        public static string NowType { get; set; } = "未分类";

        public static void RegisteCoder(string type, string name, string lang, Func<ConfigBase, string> func)
        {
            NowType = type ?? "未分类";

            string NewFunc(ConfigBase cfg) => MomentCoderBase.DoCoder(func, cfg);

            if (!coders.ContainsKey(NowType))
                coders.Add(NowType, new Dictionary<string, CoderDefine>());

            if (coders[NowType].ContainsKey(name))
                coders[NowType][name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                coders[NowType].Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });


            if (FindDictionary.ContainsKey(name))
                FindDictionary[name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                FindDictionary.Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });

            types.Add(name);
        }


    }
}