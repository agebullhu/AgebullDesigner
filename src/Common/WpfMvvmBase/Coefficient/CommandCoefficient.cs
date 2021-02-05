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
        /// 缓存获取
        /// </summary>
        /// <typeparam name="TCommandModel"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static NotificationList<CommandItemBase> GetCacheCommands<TCommandModel>(TCommandModel model)
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


        #region 类型过滤

        /// <summary>
        /// 类型过滤
        /// </summary>
        /// <param name="editor">编辑器</param>
        /// <returns></returns>
        public static List<CommandItemBase> Filter<T>()
        {
            var list = new List<CommandItemBase>();

            var dictionary = new HashSet<string>();
            foreach (var item in list)
                dictionary.Add(item.Key);
            var type = typeof(T);
            foreach (var item in CommandBuilders)
            {
                if (!IsType(item.Key, type))
                    continue;
                foreach (var func in item.Value)
                {
                    var cmds = func();
                    foreach (var action in cmds)
                    {
                        var key = $"{action.Catalog}_{action.Caption}";
                        if (dictionary.Contains(key))
                            continue;
                        dictionary.Add(key);
                        list.Add(action.ToCommand(key, null, null));
                    }
                }
            }
            return list;
        }
        #endregion
        #region 树节点菜单

        /// <summary>
        /// 树节点菜单类型判断
        /// </summary>
        public static Func<Type, Type, bool, bool> TreeItemCheckType { get; set; }


        /// <summary>
        /// 树节点菜单
        /// </summary>
        /// <param name="view">视角</param>
        /// <param name="binding">对象</param>
        /// <returns></returns>
        public static List<CommandItemBase> TreeItem(object binding, string view)
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
                        if (action.TargetType != null && TreeItemCheckType != null &&
                            !TreeItemCheckType.Invoke(type, action.TargetType, action.SignleSoruce))
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
        #endregion

        #region 扩展编辑器


        static bool IsType(Type p, Type type)
        {
            return p == type || type.IsSubclassOf(p) || type.IsSupperInterface(p);
        }

        /// <summary>
        /// 编辑器命令对象匹配
        /// </summary>
        /// <param name="list">命令</param>
        /// <param name="type">匹配类型</param>
        /// <param name="cmdFilter">过滤器</param>
        /// <param name="arg">当前对象</param>
        /// <returns></returns>
        public static void EditorToolbar(IList<CommandItemBase> list,Func<ICommandItemBuilder, bool> cmdFilter,Type type)
        {
            var dictionary = new HashSet<string>();
            foreach (var item in list)
                dictionary.Add(item.Key);

            foreach (var item in CommandBuilders)
            {
                if (!IsType(item.Key,type))
                    continue;
                foreach (var func in item.Value)
                {
                    var cmds = func();
                    foreach (var action in cmds.Where(cmdFilter))
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
        #endregion
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
