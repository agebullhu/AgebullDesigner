using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Agebull.EntityModel.Config
{

    /// <summary>
    ///     配置遍历器
    /// </summary>
    public static class ConfigIteratorExtension
    {
        /// <summary>
        /// 是否需要向上匹配
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="types"></param>
        /// <returns></returns>
        public static bool IsParentOrFriendType<T>(params Type[] types)
        {
            var destType = typeof(T);

            foreach (var type in types)
            {
                if (type == destType || type.IsSubclassOf(destType) || type.IsSupperInterface(destType))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="TDest"></typeparam>
        /// <returns></returns>
        internal static void DoForeach<TDest, T>(this IEnumerable<TDest> enumerable, Action<T> action, bool doAction)
            where TDest : IConfigIterator
        {
            if (enumerable == null)
                return;
            foreach (var item in enumerable)
            {
                item.Look(action, doAction);
            }
        }


        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="TDest"></typeparam>
        /// <returns></returns>
        public static void OnLoad<TDest>(this IEnumerable<TDest> enumerable, ConfigBase parent)
            where TDest : ConfigBase, IChildrenConfig
        {
            if (enumerable == null)
                return;
            foreach (var item in enumerable)
            {
                item.Parent = parent;
                item.OnLoad();
            }
        }

        #region Temlpate
        /*
        #region 遍历
        
        /// <summary>
        /// 向下遍历
        /// </summary>
        /// <typeparam name="TDest"></typeparam>
        /// <returns></returns>
        public static void Foreach<TDest, T>(this IEnumerable<TDest> enumerable, Action<T> action)
            where TDest : IConfigIterator
        {
            if (enumerable == null)
                return;
            foreach (var item in enumerable)
            {
                item.Foreach(action);
            }
        }
        #endregion

        /// <summary>
        /// 是否需要向上匹配
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="types"></param>
        /// <returns></returns>
        public static bool IsParentOrFriendType<T>(params Type[] types)
        {
            var destType = typeof(T);

            foreach (var type in types)
            {
                if (type == destType || type.IsSubclassOf(destType) || type.IsSupperInterface(destType))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 是否需要向上匹配
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="types"></param>
        /// <returns></returns>
        static bool IsParentOrFriendType<T>(this object now, params Type[] types)
        {
            var destType = typeof(T);
            if (destType == now.GetType())
                return false;
            foreach (var type in types)
            {
                if (type == destType || type.IsSubclassOf(destType) || type.IsSupperInterface(destType))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 向上匹配
        /// </summary>
        /// <returns>true表示匹配成功</returns>
        public static bool ForeachUp<TSource, TDest, T>(this object starting, Func<TSource, T> getParentOrFriend, Action<T> action, params Type[] types)
            where T : IConfigIterator
        {
            if (starting is TSource source && IsParentOrFriendType<T>(starting, types))
            {
                getParentOrFriend(source).Foreach(action);
                return true;
            }
            return false;
        }

        [ThreadStatic] private static int level;
        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="starting"></param>
        /// <param name="action"></param>
        public static void Foreach<T>(this ConfigBase starting, Action<T> action)
            where T : class
        {
            if (starting.IsDiscard || starting.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{starting.Caption}({starting.Name})", "配置遍历");
            level = 0;

            if (starting is ProjectChildConfigBase projectChild && IsParentOrFriendType<T>(starting, typeof(ProjectConfig)))
            {
                Foreach(projectChild.Parent, action);
                return;
            }


            switch (starting)
            {
                case SolutionConfig solution:
                    Foreach(action, solution);
                    return;
                case ProjectConfig project:
                    if (IsParentOrFriendType<T>(starting, typeof(SolutionConfig)))
                    {
                        DoAction(action, GlobalConfig.CurrentSolution);
                    }
                    else
                    {
                        DoAction(action, starting);
                    }
                    return;
                case EntityClassify classify:
                    if (IsParentOrFriendType<T>(starting, typeof(ProjectConfig)))
                    {
                        DoAction(action, classify.Project);
                    }
                    else
                    {
                        DoAction(action, starting);
                    }
                    return;
                case EntityConfig item:
                    if (IsParentOrFriendType<T>(starting, typeof(ProjectConfig), typeof(SolutionConfig)))
                    {
                        DoAction(action, item.Parent);
                    }
                    else
                    {
                        Foreach(action, item);
                    }
                    return;
                case FieldConfig field:
                    if (IsParentOrFriendType<T>(starting, typeof(EntityConfig), typeof(ProjectConfig), typeof(SolutionConfig)))
                    {
                        DoAction(action, field.Parent);
                    }
                    else
                    {
                        DoAction(action, starting);
                    }
                    return;
                case ModelConfig item:
                    if (IsParentOrFriendType<T>(starting, typeof(ProjectConfig), typeof(SolutionConfig)))
                    {
                        DoAction(action, item.Parent);
                    }
                    else
                    {
                        Foreach(action, item);
                    }
                    return;
                case PropertyConfig property:
                    if (IsParentOrFriendType<T>(starting, typeof(ModelConfig), typeof(ProjectConfig), typeof(SolutionConfig)))
                    {
                        DoAction(action, property.Parent);
                    }
                    else
                    {
                        DoAction(action, starting);
                    }
                    return;
                case UserCommandConfig cmd:
                    if (IsParentOrFriendType<T>(starting, typeof(IEntityConfig), typeof(ProjectConfig), typeof(SolutionConfig)))
                    {
                        DoAction(action, cmd.Parent);
                    }
                    else
                    {
                        DoAction(action, starting);
                    }
                    return;
                case EnumConfig enumConfig:
                    if (IsParentOrFriendType<T>(starting, typeof(ProjectConfig), typeof(SolutionConfig)))
                    {
                        DoAction(action, enumConfig.Parent);
                    }
                    else
                    {
                        DoAction(action, starting);
                    }
                    return;

                case DataBaseFieldConfig dataBaseField:
                    if (IsParentOrFriendType<T>(starting, typeof(DataTableConfig), typeof(EntityConfigBase), typeof(ProjectConfig)))
                    {
                        DoAction(action, dataBaseField.Field as ConfigBase);
                    }
                    else
                    {
                        DoAction(action, starting);
                    }
                    return;
            }
        }

        private static void DoAction<T>(Action<T> action, ConfigBase config)
            where T : class
        {
            if (config.IsDiscard || config.IsDelete)
                return;
            if (config is T t)
            {
                action(t);
            }
        }

        private static void Foreach<T>(Action<T> action, EntityConfig item)
            where T : class
        {
            if (item.IsDiscard || item.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{item.Caption}({item.Name})", "配置遍历");
            int lv = level;
            DoAction(action, item);
            if (typeof(EntityConfig) == typeof(T))
                return;
            foreach (var property in item.Properties)
            {
                level = lv + 1;
                DoAction(action, property);
            }
            level = lv;
        }
        private static void Foreach<T>(Action<T> action, ModelConfig item)
            where T : class
        {
            if (item.IsDiscard || item.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{item.Caption}({item.Name})", "配置遍历");
            int lv = level;
            DoAction(action, item);
            if (typeof(ModelConfig) == typeof(T))
                return;
            foreach (var property in item.Properties)
            {
                level = lv + 1;
                DoAction(action, property);
            }

            foreach (var cmd in item.Commands)
            {
                level = lv + 1;
                DoAction(action, cmd);
            }
            level = lv;
        }

        private static void Foreach<T>(Action<T> action, EntityClassify classify)
            where T : class
        {
            if (classify.IsDiscard || classify.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{classify.Caption}({classify.Name})", "配置遍历");
            int lv = level;
            DoAction(action, classify);
            if (typeof(EntityClassify) == typeof(T))
                return;
            foreach (var item in classify.Items)
            {
                level = lv + 1;
                Foreach(action, item);
            }
            level = lv;
        }

        private static void Foreach<T>(Action<T> action, ProjectConfig project)
            where T : class
        {
            if (project.IsDiscard || project.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{project.Caption}({project.Name})", "配置遍历");
            int lv = level;
            DoAction(action, project);

            foreach (var item in project.Classifies)
            {
                level = lv + 1;
                Foreach(action, item);
            }
            foreach (var item in project.Entities)
            {
                level = lv + 1;
                Foreach(action, item);
            }

            foreach (var e in project.Models)
            {
                level = lv + 1;
                Foreach(action, e);
            }

            foreach (var e in project.Enums)
            {
                level = lv + 1;
                Foreach(action, e);
            }

            foreach (var a in project.ApiItems)
            {
                level = lv + 1;
                DoAction(action, a);
            }

            level = lv;
        }
        private static void Foreach<T>(Action<T> action, EnumConfig @enum)
            where T : class
        {
            if (@enum.IsDiscard || @enum.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{@enum.Caption}({@enum.Name})", "配置遍历");
            int lv = level;
            DoAction(action, @enum);
            foreach (var item in @enum.Items)
            {
                level = lv + 1;
                DoAction(action, item);
            }
            level = lv;
        }

        private static void Foreach<T>(Action<T> action, SolutionConfig solution)
            where T : class
        {
            if (solution.IsDiscard || solution.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{solution.Caption}({solution.Name})", "配置遍历");
            int lv = level;
            DoAction(action, solution);
            foreach (var project in solution.Projects)
            {
                level = lv + 1;
                Foreach(action, project);
            }
            level = lv;
        }


        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="starting"></param>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        public static void Foreach<T>(this ConfigBase starting, Func<T, bool> condition, Action<T> action)
            where T : class
        {
            if (starting.IsDiscard || starting.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{starting.Caption}({starting.Name})", "配置遍历");
            level = 0;

            switch (starting)
            {
                case EntityConfig item:
                    if (typeof(T) == typeof(ProjectConfig))
                    {
                        DoAction(condition, action, item.Parent);
                    }
                    else
                    {
                        Foreach(condition, action, item);
                    }
                    return;
                case FieldConfig field:
                    if (typeof(T) == typeof(IEntityConfig) || typeof(T) == typeof(EntityConfig))
                    {
                        DoAction(condition, action, field.Parent);
                    }
                    else
                    {
                        DoAction(condition, action, starting);
                    }
                    return;
                case ModelConfig item:
                    if (typeof(T) == typeof(ProjectConfig))
                    {
                        DoAction(condition, action, item.Parent);
                    }
                    else
                    {
                        Foreach(condition, action, item);
                    }
                    return;
                case PropertyConfig property:
                    if (typeof(T) == typeof(IEntityConfig) || typeof(T) == typeof(ModelConfig))
                    {
                        DoAction(condition, action, property.Parent);
                    }
                    else
                    {
                        DoAction(condition, action, starting);
                    }
                    return;
                case UserCommandConfig cmd:
                    if (typeof(T) == typeof(IEntityConfig) || typeof(T) == typeof(ModelConfig))
                    {
                        DoAction(condition, action, cmd.Parent);
                    }
                    else
                    {
                        DoAction(condition, action, starting);
                    }
                    return;
                case EnumConfig enumConfig:
                    Foreach(condition, action, enumConfig);
                    return;
                case ProjectConfig project:
                    Foreach(condition, action, project);
                    return;
                case EntityClassify classify:
                    Foreach(condition, action, classify);
                    return;
                case SolutionConfig solution:
                    Foreach(condition, action, solution);
                    return;
            }
        }

        private static void DoAction<T>(Func<T, bool> condition, Action<T> action, ConfigBase config)
            where T : class
        {
            if (config.IsDiscard || config.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{config.Caption}({config.Name})", "配置遍历");
            if (!(config is T t))
                return;
            int lv = level;
            if (condition(t))
                action(t);
            level = lv;
        }

        private static void Foreach<T>(Func<T, bool> condition, Action<T> action, EntityClassify classify)
            where T : class
        {
            if (classify.IsDiscard || classify.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{classify.Caption}({classify.Name})", "配置遍历");
            int lv = level;
            DoAction(condition, action, classify);
            if (typeof(EntityClassify) == typeof(T))
                return;
            foreach (var item in classify.Items)
            {
                level = lv + 1;
                Foreach(condition, action, item);
            }
            level = lv;
        }


        private static void Foreach<T>(Func<T, bool> condition, Action<T> action, EntityConfig item)
            where T : class
        {
            if (item.IsDiscard || item.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{item.Caption}({item.Name})", "配置遍历");
            DoAction(condition, action, item);
            if (typeof(T) == typeof(EntityConfig))
                return;
            int lv = level;
            foreach (var property in item.Properties)
            {
                level = lv + 1;
                DoAction(condition, action, property);
            }
            level = lv;
        }

        private static void Foreach<T>(Func<T, bool> condition, Action<T> action, ModelConfig item)
            where T : class
        {
            if (item.IsDiscard || item.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{item.Caption}({item.Name})", "配置遍历");
            DoAction(condition, action, item);
            if (typeof(T) == typeof(EntityConfig))
                return;
            int lv = level;
            foreach (var property in item.Properties)
            {
                level = lv + 1;
                DoAction(condition, action, property);
            }
            level = lv;
        }
        private static void Foreach<T>(Func<T, bool> condition, Action<T> action, EnumConfig @enum)
            where T : class
        {
            if (@enum.IsDiscard || @enum.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{@enum.Caption}({@enum.Name})", "配置遍历");
            DoAction(action, @enum);
            if (typeof(T) == typeof(EnumConfig))
                return;
            int lv = level;
            foreach (var item in @enum.Items)
            {
                level = lv + 1;
                DoAction(condition, action, item);
            }
            level = lv;
        }

        private static void Foreach<T>(Func<T, bool> condition, Action<T> action, ProjectConfig project)
            where T : class
        {
            if (project.IsDiscard || project.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{project.Caption}({project.Name})", "配置遍历");
            DoAction(condition, action, project);
            if (typeof(T) == typeof(ProjectConfig))
                return;
            int lv = level;
            foreach (var item in project.Entities)
            {
                level = lv + 1;
                Foreach(condition, action, item);
            }

            foreach (var item in project.Enums)
            {
                level = lv + 1;
                Foreach(condition, action, item);
            }

            foreach (var item in project.ApiItems)
            {
                level = lv + 1;
                DoAction(condition, action, item);
            }

            level = lv;
        }

        private static void Foreach<T>(Func<T, bool> condition, Action<T> action, SolutionConfig solution)
            where T : class
        {
            if (solution.IsDiscard || solution.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{solution.Caption}({solution.Name})", "配置遍历");
            DoAction(condition, action, solution);
            if (typeof(T) == typeof(ProjectConfig))
                return;
            int lv = level;
            foreach (var project in solution.Projects)
            {
                level = lv + 1;
                Foreach(condition, action, project);
            }
            level = lv;
        }
        */
        #endregion
    }
}