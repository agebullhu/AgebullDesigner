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
                        DoAction(action, property.Field.Entity);
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
                case EntityConfig entity:
                    if (typeof(T) == typeof(EntityConfig))
                    {
                        DoAction(action, entity);
                    }
                    else if (typeof(T) == typeof(ProjectConfig))
                    {
                        DoAction(action, entity.Parent);
                    }
                    else
                    {
                        Foreach(action, entity);
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
                        DoAction(condition, action, property.Field.Entity);
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
                case EntityConfig entity:
                    if (typeof(T) == typeof(EntityConfig))
                    {
                        DoAction(condition, action, entity);
                    }
                    else if (typeof(T) == typeof(ProjectConfig))
                    {
                        DoAction(condition, action, entity.Parent);
                    }
                    else
                    {
                        Foreach(condition, action, entity);
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
                //Trace.WriteLine($" <=====>\t{config.Caption ?? config.Name}", $"{ Space()}{ config.GetTypeName()}");
            }
            //else
            //{
            //    Trace.WriteLine($"        \t{config.Caption ?? config.Name}", $"{Space()}{config.GetTypeName()}");
            //}
        }
        /*
        static string Space()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append( ' ', level * 3);
            return builder.ToString();
        }*/
        private static void Foreach<T>(Action<T> action, EntityConfig entity)
            where T : ConfigBase
        {
            if (entity.IsDiscard || entity.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{entity.Caption}({entity.Name})", "配置遍历");
            int lv = level;
            DoAction(action, entity);
            foreach (var property in entity.Properties)
            {
                level = lv + 1;
                DoAction(action, property);
            }
            level = lv;
        }
        private static void Foreach<T>(Action<T> action, ModelConfig entity)
            where T : ConfigBase
        {
            if (entity.IsDiscard || entity.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{entity.Caption}({entity.Name})", "配置遍历");
            int lv = level;
            DoAction(action, entity);
            foreach (var property in entity.Properties)
            {
                level = lv + 1;
                DoAction(action, property);
            }

            foreach (var cmd in entity.Commands)
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
            foreach (var entity in classify.Items)
            {
                level = lv + 1;
                Foreach(action, entity);
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
            foreach (var entity in project.Entities)
            {
                level = lv + 1;
                Foreach(action, entity);
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

        private static void Foreach<T>(Func<T, bool> condition, Action<T> action, ModelConfig entity)
            where T : ConfigBase
        {
            if (entity.IsDiscard || entity.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{entity.Caption}({entity.Name})", "配置遍历");
            DoAction(condition, action, entity);
            if (typeof(T) == typeof(ModelConfig))
                return;
            int lv = level;
            foreach (var property in entity.Properties)
            {
                level = lv + 1;
                DoAction(condition, action, property);
            }
            foreach (var cmd in entity.Commands)
            {
                level = lv + 1;
                DoAction(action, cmd);
            }
            level = lv;
        }
        private static void Foreach<T>(Func<T, bool> condition, Action<T> action, EntityConfig entity)
            where T : ConfigBase
        {
            if (entity.IsDiscard || entity.IsDelete)
                return;
            Trace.WriteLineIf(SolutionConfig.Current.DetailTrace, $"{entity.Caption}({entity.Name})", "配置遍历");
            DoAction(condition, action, entity);
            if (typeof(T) == typeof(EntityConfig))
                return;
            int lv = level;
            foreach (var property in entity.Properties)
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
            foreach (var entity in project.Entities)
            {
                level = lv + 1;
                Foreach(condition, action, entity);
            }

            foreach (var entity in project.Enums)
            {
                level = lv + 1;
                Foreach(condition, action, entity);
            }

            foreach (var entity in project.ApiItems)
            {
                level = lv + 1;
                DoAction(condition, action, entity);
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

        #region Config
        /*
        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="starting"></param>
        /// <param name="action"></param>
        public static void Foreach(this ConfigBase starting, Action<ConfigBase> action)
        {
            switch (starting)
            {
                case PropertyConfig property:
                    Foreach(action, property);
                    return;
                case EntityConfig entity:
                    Foreach(action, entity);
                    return;
                case ProjectConfig project:
                    Foreach(action, project);
                    return;
                case SolutionConfig solution:
                    Foreach(action, solution);
                    return;
            }
        }
        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="starting"></param>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        public static void Foreach(this ConfigBase starting, Func<ConfigBase, bool> condition, Action<ConfigBase> action)
        {
            switch (starting)
            {
                case PropertyConfig property:
                    Foreach(condition, action, property);
                    return;
                case EntityConfig entity:
                    Foreach(condition, action, entity);
                    return;
                case ProjectConfig project:
                    Foreach(condition, action, project);
                    return;
                case SolutionConfig solution:
                    Foreach(condition, action, solution);
                    return;
            }
        }

        private static void DoAction(Action<ConfigBase> action, ConfigBase config)
        {
            if (config != null)
                action(config);
        }

        private static void Foreach(Action<ConfigBase> action, EntityConfig entity)
        {
            DoAction(action, entity);
            foreach (var property in entity.Properties)
                DoAction(action, property);
            foreach (var cmd in entity.Commands)
                DoAction(action, cmd);
        }

        private static void Foreach(Action<ConfigBase> action, ProjectConfig project)
        {
            DoAction(action, project);
            foreach (var entity in project.Entities)
                Foreach(action, entity);
            foreach (var e in project.Enums)
                Foreach(action, e);
            foreach (var a in project.ApiItems)
                Foreach(action, a);
        }

        private static void Foreach(Action<ConfigBase> action, SolutionConfig solution)
        {
            DoAction(action, solution);
            foreach (var project in solution.Projects)
                Foreach(action, project);
        }


        private static void DoAction(Func<ConfigBase, bool> condition, Action<ConfigBase> action, ConfigBase config)
        {
            if (config != null && condition(config))
                action(config);
        }

        private static void Foreach(Func<ConfigBase, bool> condition, Action<ConfigBase> action, EntityConfig entity)
        {
            DoAction(condition, action, entity);
            foreach (var property in entity.Properties)
                DoAction(condition, action, property);
            foreach (var cmd in entity.Commands)
                DoAction(condition, action, cmd);
        }

        private static void Foreach(Func<ConfigBase, bool> condition, Action<ConfigBase> action, ProjectConfig project)
        {
            DoAction(condition, action, project);
            foreach (var entity in project.Entities)
                Foreach(condition, action, entity);
        }

        private static void Foreach(Func<ConfigBase, bool> condition, Action<ConfigBase> action,
            SolutionConfig solution)
        {
            DoAction(condition, action, solution);
            foreach (var project in solution.Projects)
                Foreach(condition, action, project);
        }

        #endregion

        #region Property
        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="starting"></param>
        /// <param name="action"></param>
        public static void Foreach(this ConfigBase starting, Action<PropertyConfig> action)
        {
            switch (starting)
            {
                case PropertyConfig property:
                    Foreach(action, property);
                    return;
                case EntityConfig entity:
                    Foreach(action, entity);
                    return;
                case ProjectConfig project:
                    Foreach(action, project);
                    return;
                case SolutionConfig solution:
                    Foreach(action, solution);
                    return;
            }
        }
        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="starting"></param>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        public static void Foreach(this ConfigBase starting, Func<PropertyConfig, bool> condition,
            Action<PropertyConfig> action)
        {
            switch (starting)
            {
                case PropertyConfig property:
                    Foreach(condition, action, property);
                    return;
                case EntityConfig entity:
                    Foreach(condition, action, entity);
                    return;
                case ProjectConfig project:
                    Foreach(condition, action, project);
                    return;
                case SolutionConfig solution:
                    Foreach(condition, action, solution);
                    return;
            }
        }

        private static void Foreach(Action<PropertyConfig> action, PropertyConfig property)
        {
            if (property != null)
                action(property);
        }

        private static void Foreach(Action<PropertyConfig> action, EntityConfig entity)
        {
            foreach (var property in entity.Properties)
                Foreach(action, property);
        }

        private static void Foreach(Action<PropertyConfig> action, ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                Foreach(action, entity);
        }

        private static void Foreach(Action<PropertyConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                Foreach(action, project);
        }


        private static void Foreach(Func<PropertyConfig, bool> condition, Action<PropertyConfig> action,
            PropertyConfig property)
        {
            if (property != null && condition(property))
                action(property);
        }

        private static void Foreach(Func<PropertyConfig, bool> condition, Action<PropertyConfig> action,
            EntityConfig entity)
        {
            foreach (var property in entity.Properties)
                Foreach(condition, action, property);
        }

        private static void Foreach(Func<PropertyConfig, bool> condition, Action<PropertyConfig> action,
            ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                Foreach(condition, action, entity);
        }

        private static void Foreach(Func<PropertyConfig, bool> condition, Action<PropertyConfig> action,
            SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                Foreach(condition, action, project);
        }

        #endregion

        #region Enum
        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="starting"></param>
        /// <param name="action"></param>
        public static void Foreach(this ConfigBase starting, Action<EnumConfig> action)
        {
            switch (starting)
            {
                case EnumConfig @enum:
                    Foreach(action, @enum);
                    return;
                case ProjectConfig project:
                    Foreach(action, project);
                    return;
                case SolutionConfig solution:
                    Foreach(action, solution);
                    return;
            }
        }
        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="starting"></param>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        public static void Foreach(this ConfigBase starting, Func<EnumConfig, bool> condition, Action<EnumConfig> action)
        {
            switch (starting)
            {
                case EnumConfig @enum:
                    Foreach(condition, action, @enum);
                    return;
                case ProjectConfig project:
                    Foreach(condition, action, project);
                    return;
                case SolutionConfig solution:
                    Foreach(condition, action, solution);
                    return;
            }
        }

        private static void Foreach(Action<EnumConfig> action, EnumConfig config)
        {
            if (config != null)
                action(config);
        }

        private static void Foreach(Action<EnumConfig> action, ProjectConfig project)
        {
            foreach (var e in project.Enums)
                Foreach(action, e);
        }

        private static void Foreach(Action<EnumConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Enums)
                Foreach(action, project);
        }


        private static void Foreach(Func<EnumConfig, bool> condition, Action<EnumConfig> action, EnumConfig config)
        {
            if (config != null && condition(config))
                action(config);
        }

        private static void Foreach(Func<EnumConfig, bool> condition, Action<EnumConfig> action, EntityConfig entity)
        {
            foreach (var property in entity.Properties.Where(p => p.EnumConfig != null))
                Foreach(condition, action, property.EnumConfig);
        }

        private static void Foreach(Func<EnumConfig, bool> condition, Action<EnumConfig> action, ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                Foreach(condition, action, entity);
        }

        private static void Foreach(Func<EnumConfig, bool> condition, Action<EnumConfig> action,
            SolutionConfig solution)
        {
            foreach (var project in solution.Enums)
                Foreach(condition, action, project);
        }

        #endregion

        #region Api
        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="starting"></param>
        /// <param name="action"></param>
        public static void Foreach(this ConfigBase starting, Action<ApiItem> action)
        {
            switch (starting)
            {
                case ApiItem api:
                    Foreach(action, api);
                    return;
                case ProjectConfig project:
                    Foreach(action, project);
                    return;
                case SolutionConfig solution:
                    Foreach(action, solution);
                    return;
            }
        }
        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="starting"></param>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        public static void Foreach(this ConfigBase starting, Func<ApiItem, bool> condition, Action<ApiItem> action)
        {
            switch (starting)
            {
                case ApiItem api:
                    Foreach(condition, action, api);
                    return;
                case ProjectConfig project:
                    Foreach(condition, action, project);
                    return;
                case SolutionConfig solution:
                    Foreach(condition, action, solution);
                    return;
            }
        }

        private static void Foreach(Action<ApiItem> action, ApiItem config)
        {
            if (config != null)
                action(config);
        }

        private static void Foreach(Action<ApiItem> action, ProjectConfig project)
        {
            foreach (var api in project.ApiItems)
                Foreach(action, api);
        }

        private static void Foreach(Action<ApiItem> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                Foreach(action, project);
        }

        private static void Foreach(Func<ApiItem, bool> condition, Action<ApiItem> action, ApiItem config)
        {
            if (config != null && condition(config))
                action(config);
        }

        private static void Foreach(Func<ApiItem, bool> condition, Action<ApiItem> action, ProjectConfig project)
        {
            foreach (var entity in project.ApiItems)
                Foreach(condition, action, entity);
        }

        private static void Foreach(Func<ApiItem, bool> condition, Action<ApiItem> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                Foreach(condition, action, project);
        }

        #endregion

        #region Entity
        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="starting"></param>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        public static void Foreach(this ConfigBase starting, Func<EntityConfig, bool> condition, Action<EntityConfig> action)
        {
            switch (starting)
            {
                case PropertyConfig col:
                    Foreach(condition, action, col.Parent);
                    return;
                case EntityConfig entity:
                    Foreach(condition, action, entity);
                    return;
                case ProjectConfig project:
                    Foreach(condition, action, project);
                    return;
                case SolutionConfig solution:
                    Foreach(condition, action, solution);
                    return;
            }
        }
        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="starting"></param>
        /// <param name="action"></param>
        public static void Foreach(this ConfigBase starting, Action<EntityConfig> action)
        {
            switch (starting)
            {
                case PropertyConfig col:
                    action(col.Parent);
                    return;
                case EntityConfig entity:
                    Foreach(action, entity);
                    return;
                case ProjectConfig project:
                    Foreach(action, project);
                    return;
                case SolutionConfig solution:
                    Foreach(action, solution);
                    return;
            }
        }


        private static void Foreach(Action<EntityConfig> action, EntityConfig entity)
        {
            if (entity != null)
                action(entity);
        }

        private static void Foreach(Action<EntityConfig> action, ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                Foreach(action, entity);
        }

        private static void Foreach(Action<EntityConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                Foreach(action, project);
        }


        private static void Foreach(Func<EntityConfig, bool> condition, Action<EntityConfig> action,
            EntityConfig entity)
        {
            if (entity != null && condition(entity))
                action(entity);
        }

        private static void Foreach(Func<EntityConfig, bool> condition, Action<EntityConfig> action,
            ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                Foreach(condition, action, entity);
        }

        private static void Foreach(Func<EntityConfig, bool> condition, Action<EntityConfig> action,
            SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                Foreach(condition, action, project);
        }

        #endregion

        #region Project
        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="starting"></param>
        /// <param name="action"></param>
        public static void Foreach(this ConfigBase starting, Action<ProjectConfig> action)
        {
            switch (starting)
            {
                case PropertyConfig col:
                    Foreach(action, col.Parent.Parent);
                    return;
                case EntityConfig entity:
                    Foreach(action, entity.Parent);
                    return;
                case ProjectConfig project:
                    Foreach(action, project);
                    return;
                case SolutionConfig solution:
                    Foreach(action, solution);
                    return;
            }
        }
        /// <summary>
        /// 遍历
        /// </summary>
        /// <param name="starting"></param>
        /// <param name="condition"></param>
        /// <param name="action"></param>
        public static void Foreach(this ConfigBase starting, Func<ProjectConfig, bool> condition,
            Action<ProjectConfig> action)
        {
            switch (starting)
            {
                case PropertyConfig col:
                    Foreach(condition, action, col.Parent.Parent);
                    return;
                case EntityConfig entity:
                    Foreach(condition, action, entity.Parent);
                    return;
                case ProjectConfig project:
                    Foreach(condition, action, project);
                    return;
                case SolutionConfig solution:
                    Foreach(condition, action, solution);
                    return;
            }
        }

        private static void Foreach(Action<ProjectConfig> action, ProjectConfig project)
        {
            if (project != null)
                action(project);
        }

        private static void Foreach(Action<ProjectConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                Foreach(action, project);
        }

        private static void Foreach(Func<ProjectConfig, bool> condition, Action<ProjectConfig> action,
            ProjectConfig project)
        {
            if (project != null && condition(project))
                action(project);
        }

        private static void Foreach(Func<ProjectConfig, bool> condition, Action<ProjectConfig> action,
            SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                Foreach(condition, action, project);
        }
        */
        #endregion
    }
}