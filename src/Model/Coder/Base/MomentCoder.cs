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
        //private static readonly List<string> types = new List<string>();

        //public static readonly Dictionary<string, CoderDefine> FindDictionary = new Dictionary<string, CoderDefine>();

        public static readonly Dictionary<string, Dictionary<string, CoderDefine>> Coders = new Dictionary<string, Dictionary<string, CoderDefine>>();
        //public static List<string> Types => types;


        //public static string CreateCode(string type, ConfigBase cfg)
        //{
        //    if (type != "-" && coders.ContainsKey(type))
        //        return FindDictionary[type].Func(cfg);
        //    return "无有效选择";
        //}
        //public static void RegisteType(string type)
        //{
        //    if (!coders.ContainsKey(type))
        //        coders.Add(type, null);
        //}
        //public static void RegisteLine()
        //{
        //    types.Add("-");
        //}

        public static string NowType { get; set; } = "未分类";

        public static void RegisteCoder<T>(string type, string name, string lang, Func<T, string> func)
            where T : ConfigBase
        {
            NowType = type ?? "未分类";

            if (!Coders.ContainsKey(NowType))
                Coders.Add(NowType, new Dictionary<string, CoderDefine>());

            string NewFunc(ConfigBase cfg) => MomentCoderBase.CreateCode(cfg, func);

            if (Coders[NowType].ContainsKey(name))
                Coders[NowType][name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                Coders[NowType].Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });
        }

        public static void RegisteCoder<T>(string type, string name, string lang, Func<T, bool> condition, Func<T, string> func)
            where T : ConfigBase
        {
            NowType = type ?? "未分类";

            if (!Coders.ContainsKey(NowType))
                Coders.Add(NowType, new Dictionary<string, CoderDefine>());

            string NewFunc(ConfigBase cfg) => MomentCoderBase.CreateCode(cfg, condition, func);

            if (Coders[NowType].ContainsKey(name))
                Coders[NowType][name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                Coders[NowType].Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });
        }


        public static void RegisteCoder(string type, string name, string lang, Func<EntityConfig, string> func)
        {
            NowType = type ?? "未分类";

            if (!Coders.ContainsKey(NowType))
                Coders.Add(NowType, new Dictionary<string, CoderDefine>());

            string NewFunc(ConfigBase cfg) => MomentCoderBase.CreateCode(cfg, func);

            if (Coders[NowType].ContainsKey(name))
                Coders[NowType][name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                Coders[NowType].Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });
        }

        public static void RegisteCoder(string type, string name, string lang, Func<EntityConfig, bool> condition, Func<EntityConfig, string> func)
        {
            NowType = type ?? "未分类";

            if (!Coders.ContainsKey(NowType))
                Coders.Add(NowType, new Dictionary<string, CoderDefine>());

            string NewFunc(ConfigBase cfg) => MomentCoderBase.CreateCode(cfg, condition, func);

            if (Coders[NowType].ContainsKey(name))
                Coders[NowType][name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                Coders[NowType].Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });
        }



        public static void RegisteCoder(string type, string name, string lang, Func<ModelConfig, string> func)
        {
            NowType = type ?? "未分类";

            if (!Coders.ContainsKey(NowType))
                Coders.Add(NowType, new Dictionary<string, CoderDefine>());

            string NewFunc(ConfigBase cfg) => MomentCoderBase.CreateCode(cfg, func);

            if (Coders[NowType].ContainsKey(name))
                Coders[NowType][name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                Coders[NowType].Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });
        }

        public static void RegisteCoder(string type, string name, string lang, Func<ModelConfig, bool> condition, Func<ModelConfig, string> func)
        {
            NowType = type ?? "未分类";

            if (!Coders.ContainsKey(NowType))
                Coders.Add(NowType, new Dictionary<string, CoderDefine>());

            string NewFunc(ConfigBase cfg) => MomentCoderBase.CreateCode(cfg, condition, func);

            if (Coders[NowType].ContainsKey(name))
                Coders[NowType][name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                Coders[NowType].Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });
        }

        public static void RegisteCoder(string type, string name, string lang, Func<ConfigBase, string> func)
        {
            NowType = type ?? "未分类";

            if (!Coders.ContainsKey(NowType))
                Coders.Add(NowType, new Dictionary<string, CoderDefine>());

            string NewFunc(ConfigBase cfg) => MomentCoderBase.CreateCode(cfg, func);

            if (Coders[NowType].ContainsKey(name))
                Coders[NowType][name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                Coders[NowType].Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });
        }

        public static void RegisteCoder(string type, string name, string lang, Func<ConfigBase, bool> condition, Func<EntityConfig, string> func)
        {
            NowType = type ?? "未分类";

            if (!Coders.ContainsKey(NowType))
                Coders.Add(NowType, new Dictionary<string, CoderDefine>());

            string NewFunc(ConfigBase cfg) => MomentCoderBase.CreateCode(cfg, condition, func);

            if (Coders[NowType].ContainsKey(name))
                Coders[NowType][name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                Coders[NowType].Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });
        }
    }
}