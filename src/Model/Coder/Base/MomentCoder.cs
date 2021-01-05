using System;
using System.Collections.Generic;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// 代码片断定义
    /// </summary>
    public class CoderDefine
    {
        /// <summary>
        /// 方法
        /// </summary>
        public Func<ConfigBase, string> Func { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 语言类型
        /// </summary>
        public string Lang { get; set; }

        /// <summary>
        /// 自定义执行
        /// </summary>
        public bool CustomExecute { get; set; }
    }

    /// <summary>
    /// 代码片断生成器
    /// </summary>
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


        public static void RegisteCoder(string type, string name, string lang, Func<string> func)
        {
            NowType = type ?? "未分类";

            if (!Coders.ContainsKey(NowType))
                Coders.Add(NowType, new Dictionary<string, CoderDefine>());

            var define = new CoderDefine
            {
                Func = p => func(),
                CustomExecute = true,
                Name = name,
                Lang = lang
            };

            if (Coders[NowType].ContainsKey(name))
                Coders[NowType][name] = define;
            else
                Coders[NowType].Add(name, define);
        }


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

        public static void RegisteCoder(string type, string name, string lang, Func<IEntityConfig, string> func)
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