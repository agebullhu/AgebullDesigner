using System;
using System.Collections;
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
        /// <typeparam name="TArgument"></typeparam>
        /// <typeparam name="TCommandItemBuilder"></typeparam>
        /// <param name="command"></param>
        public static void RegisterCommand<TArgument, TCommandItemBuilder>(TCommandItemBuilder command)
            where TCommandItemBuilder : ICommandItemBuilder
        {
            var type = typeof(TArgument);
            if (CommandBuilders.ContainsKey(type))
            {
                CommandBuilders[type].Add(command);
            }
            else
            {
                CommandBuilders.Add(type, new List<ICommandItemBuilder> { command });
            }
        }

        /// <summary>
        /// 注册命令
        /// </summary>
        /// <typeparam name="TArgument"></typeparam>
        public static void RegisterCommand<TArgument>(ICommandItemBuilder builder)
        {
            var type = typeof(TArgument);
            if (CommandBuilders.ContainsKey(type))
            {
                CommandBuilders[type].Add(builder);
            }
            else
            {
                CommandBuilders.Add(type, new List<ICommandItemBuilder> { builder });
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
            if (CommandBuilders.ContainsKey(type))
            {
                CommandBuilders[type].Add(new TCommandItemBuilder());
            }
            else
            {
                CommandBuilders.Add(type, new List<ICommandItemBuilder> { new TCommandItemBuilder() });
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

        private static readonly Dictionary<Type, List<CommandItem>> Commands = new Dictionary<Type, List<CommandItem>>();

        /// <summary>
        /// 对象匹配
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static List<CommandItem> Coefficient(object arg)
        {
            if (arg == null)
                return null;
            var dictionary = new Dictionary<ICommandItemBuilder, bool>();
            var type = arg.GetType();
            if (Commands.TryGetValue(type, out var result))
                return result;
            Commands.Add(type, result = new List<CommandItem>());

            foreach (var item in CommandBuilders)
            {
                if (item.Key != type && !type.IsSubclassOf(item.Key))
                    continue;

                foreach (var action in item.Value.Where(p => p.Editor == null))
                {
                    if (!dictionary.ContainsKey(action))
                    {
                        result.Add(action.ToCommand(arg, null));
                        dictionary.Add(action, true);
                    }
                }
            }

            foreach (var item in SourceTypeMap)
            {
                if (item.Key != type && !type.IsSubclassOf(item.Key))
                    continue;
                foreach (var convert in item.Value)
                {
                    foreach (var cmd in CommandBuilders)
                    {
                        result.Add(CommandItem.Line);
                        foreach (var action in cmd.Value.Where(p => p.Editor == null))
                        {
                            if (dictionary.ContainsKey(action))
                                continue;
                            if (cmd.Key == convert.Key || convert.Key.IsSubclassOf(cmd.Key))
                            {
                                dictionary.Add(action, true);
                                result.Add(action.ToCommand(arg, convert.Value));
                            }
                            else if (action.SourceType == convert.Key)
                            {
                                dictionary.Add(action, true);
                                result.Add(action.ToCommand(arg, convert.Value));
                            }
                            else if (action.SourceType != null && action.SourceType.IsSubclassOf(convert.Key))
                            {
                                dictionary.Add(action, true);
                                result.Add(action.ToCommand(arg, convert.Value));
                            }
                        }
                    }
                }
            }
            return result;
        }


        /// <summary>
        /// 对象匹配
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="editor">编辑器</param>
        /// <returns></returns>
        public static List<CommandItem> CoefficientEditor(Type type, string editor)
        {
            var result = new Dictionary<ICommandItemBuilder, CommandItem>();
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

            foreach (var item in SourceTypeMap)
            {
                if (item.Key != type && !type.IsSubclassOf(item.Key))
                    continue;
                foreach (var convert in item.Value)
                {
                    foreach (var cmd in CommandBuilders)
                    {
                        foreach (var action in cmd.Value.Where(p => editor == null || p.Editor != null && p.Editor.Contains(editor)))
                        {
                            if (result.ContainsKey(action))
                                continue;
                            if (cmd.Key == convert.Key || convert.Key.IsSubclassOf(cmd.Key))
                            {
                                result.Add(action, action.ToCommand(null, convert.Value));
                            }
                            else if (action.SourceType == convert.Key)
                            {
                                result.Add(action, action.ToCommand(null, convert.Value));
                            }
                            else if (action.SourceType != null && action.SourceType.IsSupperInterface(convert.Key))
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
