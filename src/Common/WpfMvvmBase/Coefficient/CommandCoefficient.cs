using System;
using System.Collections.Generic;
using System.Linq;

namespace Agebull.Common.Mvvm
{
    /// <summary>
    /// 对象与命令的适配器
    /// </summary>
    public class CommandCoefficient
    {
        private static readonly Dictionary<Type, List<ICommandItemBuilder>> CommandBuilders = new Dictionary<Type, List<ICommandItemBuilder>>();

        /// <summary>
        /// 注册命令
        /// </summary>
        /// <typeparam name="TCommandItemBuilder"></typeparam>
        public static void RegisterCommand<TCommandItemBuilder>()
            where TCommandItemBuilder : ICommandItemBuilder, new()
        {
            RegisterCommand(new TCommandItemBuilder());
        }

        /// <summary>
        /// 注册命令
        /// </summary>
        public static void RegisterCommand(ICommandItemBuilder builder)
        {
            var type = builder.TargetType ?? typeof(object);
            if (CommandBuilders.TryGetValue(type, out var cmds))
            {
                cmds.Add(builder);
            }
            else
            {
                CommandBuilders.Add(type, new List<ICommandItemBuilder> { builder });
            }
        }

        private static readonly Dictionary<string, List<CommandItemBase>> Commands = new Dictionary<string, List<CommandItemBase>>();

        /// <summary>
        /// 菜单命令对象匹配
        /// </summary>
        /// <param name="view">视角</param>
        /// <param name="binding">对象</param>
        /// <returns></returns>
        public static List<CommandItemBase> Coefficient(object binding, string view)
        {
            if (binding == null)
                return new List<CommandItemBase>();
            var type = binding.GetType();
            string key = $"{type.FullName}:{view}";
            List<CommandItemBase> result;
            if (Commands.TryGetValue(key, out result))
            {
                foreach (var action in result)
                    action.Source = binding;
                return result;
            }
            result = new List<CommandItemBase>();
            Commands.Add(key, result);

            var dictionary = new Dictionary<ICommandItemBuilder, bool>();
            foreach (var item in CommandBuilders)
            {
                foreach (var action in item.Value.Where(p => p.Editor == null || !p.SignleSoruce))
                {
                    if (action.TargetType != null && CheckType != null && !CheckType.Invoke(type, action.TargetType, action.SignleSoruce))
                    {
                        //System.Diagnostics.Debug.WriteLine($"No Type. caption:{action.Caption}|view:{action.SoruceView}|target:{action.TargetType.FullName} - {type.FullName}");
                        continue;
                    }

                    if (action.SoruceView == null)
                    {
                        AddCommand(binding, dictionary, result, action);
                        continue;
                    }
                    if (view != null && view.Contains(action.SoruceView))
                        AddCommand(binding, dictionary, result, action);
                    //else
                    //    System.Diagnostics.Debug.WriteLine($"No SoruceView. caption:{action.Caption}|view:{action.SoruceView}-{view}|target:{action.TargetType.FullName}");
                }
            }
            return result;
        }
        public static Func<Type, Type, bool, bool> CheckType { get; set; }


        private static void AddCommand(object binding, Dictionary<ICommandItemBuilder, bool> dictionary, List<CommandItemBase> result, ICommandItemBuilder action)
        {
            if (dictionary.ContainsKey(action))
                return;
            result.Add(action.ToCommand(binding, null));
            dictionary.Add(action, true);
        }


        /// <summary>
        /// 编辑器命令对象匹配
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="editor">编辑器</param>
        /// <returns></returns>
        public static List<CommandItemBase> CoefficientEditor(Type type, string editor = null)
        {
            var result = new Dictionary<ICommandItemBuilder, CommandItemBase>();
            foreach (var item in CommandBuilders)
            {
                if (item.Key != type && !type.IsSubclassOf(item.Key))
                    continue;
                foreach (var action in item.Value.Where(p => editor == null || p.Editor != null && p.Editor.Contains(editor)))
                {
                    if (!result.ContainsKey(action))
                        result.Add(action, action.ToCommand(null, null));
                }
            }
            return result.Values.ToList();
        }
    }
}
