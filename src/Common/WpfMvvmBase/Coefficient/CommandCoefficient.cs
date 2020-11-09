using Agebull.EntityModel;
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
        private static readonly Dictionary<Type, List<Func<IEnumerable<ICommandItemBuilder>>>> CommandBuilders = new Dictionary<Type, List<Func<IEnumerable<ICommandItemBuilder>>>>();

        /// <summary>
        /// 注册命令
        /// </summary>
        /// <typeparam name="TCommandItemBuilder"></typeparam>
        public static void RegisterCommand<TDestConfig>(Func<IEnumerable<ICommandItemBuilder>> func)
        {
            var type = typeof(TDestConfig);
            if (!CommandBuilders.TryGetValue(type, out var cmds))
            {
                CommandBuilders.Add(type, cmds = new List<Func<IEnumerable<ICommandItemBuilder>>>());
            }
            cmds.Add(func);
        }

        /// <summary>
        /// 注册命令
        /// </summary>
        /// <typeparam name="TDestConfig"></typeparam>
        public static void RegisterItem<TDestConfig>(params ICommandItemBuilder[] items)
        {
            var type = typeof(TDestConfig);
            if (!CommandBuilders.TryGetValue(type, out var cmds))
            {
                CommandBuilders.Add(type, cmds = new List<Func<IEnumerable<ICommandItemBuilder>>>());
            }
            cmds.Add(() => items);
        }
        /// <summary>
        /// 清除命令对象
        /// </summary>
        public static void ClearCommand()
        {
            Commands.Clear();
            FriendCommands.Clear();
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
            if (Commands.TryGetValue(key, out List<CommandItemBase> result))
            {
                foreach (var action in result)
                    action.Source = binding;
                return result;
            }
            result = new List<CommandItemBase>();
            Commands.Add(key, result);


            var dictionary = new HashSet<string>();
            foreach (var item in CommandBuilders)
            {
                foreach (var func in item.Value)
                {
                    var cmds = func();
                    foreach (var action in cmds.Where(p => p.Editor == null || !p.SignleSoruce))
                    {
                        if (action.TargetType != null && CheckType != null &&
                            !CheckType.Invoke(type, action.TargetType, action.SignleSoruce))
                        {
                            //System.Diagnostics.Debug.WriteLine($"No Type. caption:{action.Caption}|view:{action.SoruceView}|target:{action.TargetType.FullName} - {type.FullName}");
                            continue;
                        }
                        key = $"{action.Catalog}_{action.Caption}";
                        if (dictionary.Contains(key))
                            continue;
                        dictionary.Add(key);
                        if (action.SoruceView == null || view == null || view.Contains(action.SoruceView))
                        {
                            result.Add(action.ToCommand(key, binding, null));
                        }
                        //System.Diagnostics.Debug.WriteLine($"No SoruceView. caption:{action.Caption}|view:{action.SoruceView}-{view}|target:{action.TargetType?.FullName}");
                    }
                }
            }
            return result;
        }
        public static Func<Type, Type, bool, bool> CheckType { get; set; }

        /// <summary>
        /// 编辑器命令对象匹配
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="editor">编辑器</param>
        /// <returns></returns>
        public static void CoefficientEditor<TType>(IList<CommandItemBase> list, string editor = null)
        {
            var type = typeof(TType);
            CoefficientEditor(list, p => p != type && !type.IsSubclassOf(p), editor);
        }

        /// <summary>
        /// 编辑器命令对象匹配
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="editor">编辑器</param>
        /// <returns></returns>
        public static List<CommandItemBase> CoefficientEditor(Type type, string editor = null)
        {
            var result = new List<CommandItemBase>();
            CoefficientEditor(result, p => p != type && !type.IsSubclassOf(p), editor);
            return result;
        }

        /// <summary>
        /// 编辑器命令对象匹配
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="editor">编辑器</param>
        /// <returns></returns>
        public static void CoefficientEditor(IList<CommandItemBase> list, Func<Type, bool> filter, string editor = null)
        {
            var dictionary = new HashSet<string>();
            foreach (var item in list)
                dictionary.Add(item.Key);

            foreach (var item in CommandBuilders)
            {
                if (!filter(item.Key))
                    continue;
                foreach (var func in item.Value)
                {
                    var cmds = func();
                    foreach (var action in cmds.Where(p => editor == null || p.Editor != null && p.Editor.Contains(editor)))
                    {
                        var key = $"{action.Catalog}_{action.Caption}";
                        if (dictionary.Contains(key))
                            continue;
                        dictionary.Add(key);
                        list.Add(action.ToCommand(key, null, null));
                    }
                }
            }
        }
        /// <summary>
        /// 对象今天托管获取
        /// </summary>
        /// <typeparam name="TCommandModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static NotificationList<CommandItemBase> GetFriendCommands<TCommandModel>(TCommandModel model)
            where TCommandModel : ICommandModel
        {
            if (!FriendCommands.TryGetValue(model, out var commands))
            {
                model.CreateCommands(commands = new NotificationList<CommandItemBase>());
                FriendCommands[model] = commands;
            }
            return commands;
        }

        private static readonly Dictionary<object, NotificationList<CommandItemBase>> FriendCommands = new Dictionary<object, NotificationList<CommandItemBase>>();

    }

    /// <summary>
    /// 命令模型
    /// </summary>
    public interface ICommandModel
    {
        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <param name="commands"></param>
        void CreateCommands(IList<CommandItemBase> commands);
    }

}
