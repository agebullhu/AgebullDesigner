using System;
using System.Diagnostics;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     配置遍历器
    /// </summary>
    public static class ConfigIterator
    {
        #region Temlpate

        [ThreadStatic] private static int level;
        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="starting"></param>
        /// <param name="action"></param>
        public static void Foreach<T>(this ConfigBase starting, Action<T> action)
            where T : ConfigBase
        {
            if (starting.IsDiscard || starting.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace,$"{starting.Caption}({starting.Name})","配置遍历");
            level = 0;
            switch (starting)
            {
                case FieldConfig field:
                    if (typeof(T) == typeof(EntityConfig))
                    {
                        DoAction(action, field.Entity);
                    }
                    if (typeof(T) == typeof(ProjectConfig))
                    {
                        DoAction(action, field?.Entity?.Parent);
                    }
                    else if (typeof(T) == typeof(FieldConfig))
                    {
                        DoAction(action, field.Entity?.Parent);
                    }
                    else
                    {
                        DoAction(action, field);
                    }
                    return;
                case PropertyConfig property:
                    if (typeof(T) == typeof(EntityConfig))
                    {
                        DoAction(action, property.Entity);
                    }
                    else if (typeof(T) == typeof(ProjectConfig))
                    {
                        DoAction(action, property.Model?.Parent);
                    }
                    else
                    {
                        DoAction(action, property);
                    }
                    return;
                case UserCommandConfig cmd:
                    if (typeof(T) == typeof(EntityConfig))
                    {
                        DoAction(action, cmd.Parent);
                    }
                    else if (typeof(T) == typeof(ProjectConfig))
                    {
                        DoAction(action, cmd.Parent?.Parent);
                    }
                    else
                    {
                        DoAction(action, cmd);
                    }
                    return;
                case EntityConfig item:
                    if (typeof(T) == typeof(EntityConfig))
                    {
                        DoAction(action, item);
                    }
                    else if (typeof(T) == typeof(ProjectConfig))
                    {
                        DoAction(action, item.Parent);
                    }
                    else
                    {
                        Foreach(action, item);
                    }
                    return;
                case EnumConfig enumConfig:
                    if (typeof(T) == typeof(EnumConfig))
                    {
                        DoAction(action, enumConfig);
                    }
                    else if (typeof(T) == typeof(ProjectConfig))
                    {
                        DoAction(action, enumConfig.Parent);
                    }
                    else
                    {
                        Foreach(action, enumConfig);
                    }
                    return;
                case ProjectConfig project:
                    if (typeof(T) == typeof(ProjectConfig))
                    {
                        DoAction(action, project);
                    }
                    else
                    {
                        Foreach(action, project);
                    }
                    return;
                case EntityClassify classify:
                    if (typeof(T) == typeof(EntityClassify))
                    {
                        DoAction(action, classify);
                    }
                    else
                    {
                        Foreach(action, classify);
                    }
                    return;
                case SolutionConfig solution:
                    Foreach(action, solution);
                    return;
            }
        }
        /// <summary>
        /// 遍历
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="starting"></param>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        public static void Foreach<T>(this ConfigBase starting, Func<T, bool> condition, Action<T> action)
            where T : ConfigBase
        {
            if (starting.IsDiscard || starting.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{starting.Caption}({starting.Name})", "配置遍历");
            level = 0;
            switch (starting)
            {
                case PropertyConfig property:
                    if (typeof(T) == typeof(ModelConfig))
                    {
                        DoAction(condition, action, property.Model);
                    }
                    if (typeof(T) == typeof(EntityConfig))
                    {
                        DoAction(condition, action, property.Entity);
                    }
                    else if (typeof(T) == typeof(ProjectConfig))
                    {
                        DoAction(condition, action, property.Model?.Parent);
                    }
                    else
                    {
                        DoAction(condition, action, property);
                    }
                    return;
                case FieldConfig field:
                    if (typeof(T) == typeof(EntityConfig))
                    {
                        DoAction(condition, action, field.Entity);
                    }
                    else if (typeof(T) == typeof(ProjectConfig))
                    {
                        DoAction(condition, action, field.Entity?.Parent);
                    }
                    else
                    {
                        DoAction(condition, action, field);
                    }
                    return;
                case UserCommandConfig cmd:
                    if (typeof(T) == typeof(ModelConfig))
                    {
                        DoAction(condition, action, cmd.Parent);
                    }
                    else if (typeof(T) == typeof(ProjectConfig))
                    {
                        DoAction(condition, action, cmd.Parent?.Parent);
                    }
                    else
                    {
                        DoAction(condition, action, cmd);
                    }
                    return;
                case EntityConfig item:
                    if (typeof(T) == typeof(EntityConfig))
                    {
                        DoAction(condition, action, item);
                    }
                    else if (typeof(T) == typeof(ProjectConfig))
                    {
                        DoAction(condition, action, item.Parent);
                    }
                    else
                    {
                        Foreach(condition, action, item);
                    }
                    return;
                case ProjectConfig project:
                    if (typeof(T) == typeof(ProjectConfig))
                    {
                        DoAction(condition, action, project);
                    }
                    else
                    {
                        Foreach(condition, action, project);
                    }
                    return;
                case SolutionConfig solution:
                    Foreach(condition, action, solution);
                    return;
            }
        }

        private static void DoAction<T>(Action<T> action, ConfigBase config)
            where T : ConfigBase
        {
            if (config.IsDiscard || config.IsDelete)
                return;
            if (config is T t)
            {
                action(t);
            }
        }
        private static void Foreach<T>(Action<T> action, EntityConfig item)
            where T : ConfigBase
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
            where T : ConfigBase
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
            where T : ConfigBase
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
            where T : ConfigBase
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
            where T : ConfigBase
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
            where T : ConfigBase
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


        private static void DoAction<T>(Func<T, bool> condition, Action<T> action, ConfigBase config)
            where T : ConfigBase
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


        private static void Foreach<T>(Func<T, bool> condition, Action<T> action, EntityConfig item)
            where T : ConfigBase
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
            where T : ConfigBase
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
            where T : ConfigBase
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
            where T : ConfigBase
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

        #endregion

    }
}