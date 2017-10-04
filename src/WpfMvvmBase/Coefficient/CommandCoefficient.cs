using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Gboxt.Common.WpfMvvmBase.Commands;

namespace WpfMvvmBase.Coefficient
{
    /// <summary>
    /// 对象与命令的适配器
    /// </summary>
    public class CommandCoefficient
    {
        private static readonly Dictionary<Type, List<ICommandItemBuilder>> Commands = new Dictionary<Type, List<ICommandItemBuilder>>();

        /// <summary>
        /// 注册命令
        /// </summary>
        /// <typeparam name="TArgument"></typeparam>
        /// <typeparam name="TCommandItemBuilder"></typeparam>
        /// <param name="command"></param>
        public static void RegisterCommand<TArgument, TCommandItemBuilder>(TCommandItemBuilder command)
            where TCommandItemBuilder : ICommandItemBuilder
        {
            var type = typeof(TArgument);
            if (Commands.ContainsKey(type))
            {
                Commands[type].Add(command);
            }
            else
            {
                Commands.Add(type, new List<ICommandItemBuilder> { command });
            }
        }

        /// <summary>
        /// 注册命令
        /// </summary>
        /// <typeparam name="TArgument"></typeparam>
        public static void RegisterCommand<TArgument>(ICommandItemBuilder builder)
        {
            var type = typeof(TArgument);
            if (Commands.ContainsKey(type))
            {
                Commands[type].Add(builder);
            }
            else
            {
                Commands.Add(type, new List<ICommandItemBuilder> { builder });
            }
        }
        /// <summary>
        /// 注册命令
        /// </summary>
        /// <typeparam name="TArgument"></typeparam>
        /// <typeparam name="TCommandItemBuilder"></typeparam>
        public static void RegisterCommand<TArgument, TCommandItemBuilder>()
            where TCommandItemBuilder : ICommandItemBuilder, new()
        {
            var type = typeof(TArgument);
            if (Commands.ContainsKey(type))
            {
                Commands[type].Add(new TCommandItemBuilder());
            }
            else
            {
                Commands.Add(type, new List<ICommandItemBuilder> { new TCommandItemBuilder() });
            }
        }
        /// <summary>
        /// 对象转换器
        /// </summary>
        private static readonly Dictionary<Type, Dictionary<Type, Func<object, IEnumerator>>> SourceTypeMap = new Dictionary<Type, Dictionary<Type, Func<object, IEnumerator>>>();

        /// <summary>
        /// 注册对象转换器
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TTarget"></typeparam>
        /// <param name="enumerable"></param>
        public static void RegisterConvert<TSource, TTarget>(Func<object, IEnumerator> enumerable)
        {
            var target = typeof(TTarget);
            var source = typeof(TSource);

            if (!SourceTypeMap.ContainsKey(source))
            {
                SourceTypeMap.Add(source, new Dictionary<Type, Func<object, IEnumerator>>());
            }
            if (!SourceTypeMap[source].ContainsKey(target))
            {
                SourceTypeMap[source].Add(target, enumerable);
            }
            else
            {
                SourceTypeMap[source][target] = enumerable;
            }
        }
        /// <summary>
        /// 对象匹配
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static List<CommandItem> Coefficient(object arg)
        {
            if (arg == null)
                return null;
            var result = new Dictionary<ICommandItemBuilder, CommandItem>();
            var type = arg.GetType();
            foreach (var item in Commands)
            {
                if (item.Key != type && !type.IsSubclassOf(item.Key))
                    continue;
                foreach (var action in item.Value)
                {
                    if (action.Catalog == null && !result.ContainsKey(action))
                        result.Add(action, action.ToCommand(arg, null));
                }
            }

            foreach (var item in SourceTypeMap)
            {
                if (item.Key != type && !type.IsSubclassOf(item.Key))
                    continue;
                foreach (var convert in item.Value)
                {
                    foreach (var cmd in Commands)
                    {
                        foreach (var action in cmd.Value.Where(p => !p.Signle && p.Catalog == null))
                        {
                            if (result.ContainsKey(action))
                                continue;
                            if (cmd.Key == convert.Key || convert.Key.IsSubclassOf(cmd.Key))
                            {
                                result.Add(action, action.ToCommand(arg, convert.Value));
                            }
                            else if (action.SourceType == convert.Key.FullName)
                            {
                                result.Add(action, action.ToCommand(arg, convert.Value));
                            }
                            else if (action.SourceType != null && action.SourceType.Contains(convert.Key.Name))
                            {
                                result.Add(action, action.ToCommand(arg, convert.Value));
                            }
                        }
                    }
                }
            }
            return result.Values.ToList();
        }

        /// <summary>
        /// 对象匹配
        /// </summary>
        /// <param name="arg"></param>
        /// <param name="catalog">分类</param>
        /// <returns></returns>
        public static List<CommandItem> Coefficient(object arg,string catalog)
        {
            if (arg == null)
                return null;
            var result = new Dictionary<ICommandItemBuilder, CommandItem>();
            var type = arg.GetType();
            foreach (var item in Commands)
            {
                if (item.Key != type && !type.IsSubclassOf(item.Key))
                    continue;
                foreach (var action in item.Value)
                {
                    if (action.Catalog  != null && action.Catalog.Contains(catalog) && !result.ContainsKey(action))
                        result.Add(action, action.ToCommand(arg, null));
                }
            }

            foreach (var item in SourceTypeMap)
            {
                if (item.Key != type && !type.IsSubclassOf(item.Key))
                    continue;
                foreach (var convert in item.Value)
                {
                    foreach (var cmd in Commands)
                    {
                        foreach (var action in cmd.Value.Where(p => !p.Signle && p.Catalog != null && p.Catalog.Contains(catalog)))
                        {
                            if (result.ContainsKey(action))
                                continue;
                            if (cmd.Key == convert.Key || convert.Key.IsSubclassOf(cmd.Key))
                            {
                                result.Add(action, action.ToCommand(arg, convert.Value));
                            }
                            else if (action.SourceType == convert.Key.FullName)
                            {
                                result.Add(action, action.ToCommand(arg, convert.Value));
                            }
                            else if (action.SourceType != null && action.SourceType.Contains(convert.Key.Name))
                            {
                                result.Add(action, action.ToCommand(arg, convert.Value));
                            }
                        }
                    }
                }
            }
            return result.Values.ToList();
        }


        /// <summary>
        /// 对象匹配
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="catalog">分类</param>
        /// <returns></returns>
        public static List<CommandItem> Coefficient(Type type, string catalog)
        {
            var result = new Dictionary<ICommandItemBuilder, CommandItem>();
            foreach (var item in Commands)
            {
                if (item.Key != type && !type.IsSubclassOf(item.Key))
                    continue;
                foreach (var action in item.Value)
                {
                    if (action.Catalog != null && action.Catalog.Contains(catalog) && !result.ContainsKey(action))
                        result.Add(action, action.ToCommand(null, null));
                }
            }

            foreach (var item in SourceTypeMap)
            {
                if (item.Key != type && !type.IsSubclassOf(item.Key))
                    continue;
                foreach (var convert in item.Value)
                {
                    foreach (var cmd in Commands)
                    {
                        foreach (var action in cmd.Value.Where(p => !p.Signle && p.Catalog != null && p.Catalog.Contains(catalog)))
                        {
                            if (result.ContainsKey(action))
                                continue;
                            if (cmd.Key == convert.Key || convert.Key.IsSubclassOf(cmd.Key))
                            {
                                result.Add(action, action.ToCommand(null, convert.Value));
                            }
                            else if (action.SourceType == convert.Key.FullName)
                            {
                                result.Add(action, action.ToCommand(null, convert.Value));
                            }
                            else if (action.SourceType != null && action.SourceType.Contains(convert.Key.Name))
                            {
                                result.Add(action, action.ToCommand(null, convert.Value));
                            }
                        }
                    }
                }
            }
            return result.Values.ToList();
        }
    }
}
