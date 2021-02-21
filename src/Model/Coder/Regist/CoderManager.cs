using Agebull.EntityModel.Config;
using Agebull.EntityModel.Designer;
using System;
using System.Collections.Generic;
using Cmd = System.Collections.Generic.Dictionary<string, System.Func<System.Action<System.Collections.Generic.Dictionary<string, string>>, Agebull.Common.Mvvm.CommandItemBase>>;

namespace Agebull.EntityModel.RobotCoder
{
    /// <summary>
    /// ��������������
    /// </summary>
    public class CoderManager
    {
        #region ��Ŀ����

        /// <summary>
        /// ע�����Ŀ����������
        /// </summary>
        public static readonly Dictionary<string, Cmd> Builders = new Dictionary<string, Cmd>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// ע�����Ŀ������
        /// </summary>
        /// <returns></returns>
        public static void RegistBuilder<TBuilder>()
            where TBuilder : ProjectBuilder, new()
        {
            var builder = new TBuilder();
            var type = typeof(IEntityConfig).Name;
            if (!Builders.TryGetValue(type, out var cmd))
                Builders.Add(type, cmd = new Cmd());

            if (cmd.ContainsKey(builder.Name))
                throw new ArgumentException("��ע������Ϊ" + builder.Name + "����Ŀ����������Ӧ���ظ�ע��");
            cmd.Add(builder.Name, OnCodeSuccess => new ProjectCodeCommand(() => new TBuilder())
            {
                Caption = builder.Caption,
                IconName = builder.Icon,
                OnCodeSuccess = OnCodeSuccess
            }.ToCommand(null));
        }

        #endregion
        #region ����Ƭ��

        //private static readonly List<string> types = new List<string>();

        //public static readonly Dictionary<string, CoderDefine> FindDictionary = new Dictionary<string, CoderDefine>();
        public static readonly Dictionary<string, Dictionary<string, CoderDefine>> MomentCoders = new Dictionary<string, Dictionary<string, CoderDefine>>();
        //public static List<string> Types => types;


        //public static string CreateCode(string type, ConfigBase cfg)
        //{
        //    if (type != "-" && coders.ContainsKey(type))
        //        return FindDictionary[type].Func(cfg);
        //    return "����Чѡ��";
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

        public static string NowType { get; set; } = "δ����";


        public static void RegisteCoder(string type, string name, string lang, Func<string> func)
        {
            NowType = type ?? "δ����";

            if (!MomentCoders.ContainsKey(NowType))
                MomentCoders.Add(NowType, new Dictionary<string, CoderDefine>());

            var define = new CoderDefine
            {
                Func = p => func(),
                CustomExecute = true,
                Name = name,
                Lang = lang
            };

            if (MomentCoders[NowType].ContainsKey(name))
                MomentCoders[NowType][name] = define;
            else
                MomentCoders[NowType].Add(name, define);
        }


        public static void RegisteCoder<T>(string type, string name, string lang, Func<T, string> func)
            where T : ConfigBase
        {
            NowType = type ?? "δ����";

            if (!MomentCoders.ContainsKey(NowType))
                MomentCoders.Add(NowType, new Dictionary<string, CoderDefine>());

            string NewFunc(ConfigBase cfg) => MomentCoderBase.CreateCode(cfg, func);

            if (MomentCoders[NowType].ContainsKey(name))
                MomentCoders[NowType][name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                MomentCoders[NowType].Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });
        }

        public static void RegisteCoder<T>(string type, string name, string lang, Func<T, bool> condition, Func<T, string> func)
            where T : ConfigBase
        {
            NowType = type ?? "δ����";

            if (!MomentCoders.ContainsKey(NowType))
                MomentCoders.Add(NowType, new Dictionary<string, CoderDefine>());

            string NewFunc(ConfigBase cfg) => MomentCoderBase.CreateCode(cfg, condition, func);

            if (MomentCoders[NowType].ContainsKey(name))
                MomentCoders[NowType][name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                MomentCoders[NowType].Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });
        }

        public static void RegisteCoder(string type, string name, string lang, Func<IEntityConfig, string> func)
        {
            NowType = type ?? "δ����";

            if (!MomentCoders.ContainsKey(NowType))
                MomentCoders.Add(NowType, new Dictionary<string, CoderDefine>());

            string NewFunc(ConfigBase cfg) => MomentCoderBase.CreateCode(cfg, func);

            if (MomentCoders[NowType].ContainsKey(name))
                MomentCoders[NowType][name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                MomentCoders[NowType].Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });
        }


        public static void RegisteCoder(string type, string name, string lang, Func<EntityConfig, string> func)
        {
            NowType = type ?? "δ����";

            if (!MomentCoders.ContainsKey(NowType))
                MomentCoders.Add(NowType, new Dictionary<string, CoderDefine>());

            string NewFunc(ConfigBase cfg) => MomentCoderBase.CreateCode(cfg, func);

            if (MomentCoders[NowType].ContainsKey(name))
                MomentCoders[NowType][name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                MomentCoders[NowType].Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });
        }

        public static void RegisteCoder(string type, string name, string lang, Func<EntityConfig, bool> condition, Func<EntityConfig, string> func)
        {
            NowType = type ?? "δ����";

            if (!MomentCoders.ContainsKey(NowType))
                MomentCoders.Add(NowType, new Dictionary<string, CoderDefine>());

            string NewFunc(ConfigBase cfg) => MomentCoderBase.CreateCode(cfg, condition, func);

            if (MomentCoders[NowType].ContainsKey(name))
                MomentCoders[NowType][name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                MomentCoders[NowType].Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });
        }



        public static void RegisteCoder(string type, string name, string lang, Func<ModelConfig, string> func)
        {
            NowType = type ?? "δ����";

            if (!MomentCoders.ContainsKey(NowType))
                MomentCoders.Add(NowType, new Dictionary<string, CoderDefine>());

            string NewFunc(ConfigBase cfg) => MomentCoderBase.CreateCode(cfg, func);

            if (MomentCoders[NowType].ContainsKey(name))
                MomentCoders[NowType][name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                MomentCoders[NowType].Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });
        }

        public static void RegisteCoder(string type, string name, string lang, Func<ModelConfig, bool> condition, Func<ModelConfig, string> func)
        {
            NowType = type ?? "δ����";

            if (!MomentCoders.ContainsKey(NowType))
                MomentCoders.Add(NowType, new Dictionary<string, CoderDefine>());

            string NewFunc(ConfigBase cfg) => MomentCoderBase.CreateCode(cfg, condition, func);

            if (MomentCoders[NowType].ContainsKey(name))
                MomentCoders[NowType][name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                MomentCoders[NowType].Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });
        }

        public static void RegisteCoder(string type, string name, string lang, Func<ConfigBase, string> func)
        {
            NowType = type ?? "δ����";

            if (!MomentCoders.ContainsKey(NowType))
                MomentCoders.Add(NowType, new Dictionary<string, CoderDefine>());

            string NewFunc(ConfigBase cfg) => MomentCoderBase.CreateCode(cfg, func);

            if (MomentCoders[NowType].ContainsKey(name))
                MomentCoders[NowType][name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                MomentCoders[NowType].Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });
        }

        public static void RegisteCoder(string type, string name, string lang, Func<ConfigBase, bool> condition, Func<EntityConfig, string> func)
        {
            NowType = type ?? "δ����";

            if (!MomentCoders.ContainsKey(NowType))
                MomentCoders.Add(NowType, new Dictionary<string, CoderDefine>());

            string NewFunc(ConfigBase cfg) => MomentCoderBase.CreateCode(cfg, condition, func);

            if (MomentCoders[NowType].ContainsKey(name))
                MomentCoders[NowType][name] = new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                };
            else
                MomentCoders[NowType].Add(name, new CoderDefine
                {
                    Func = NewFunc,
                    Name = name,
                    Lang = lang
                });
        }

        #endregion
    }
}