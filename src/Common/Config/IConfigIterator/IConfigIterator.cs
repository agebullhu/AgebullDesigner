using System;

namespace Agebull.EntityModel.Config
{
    /// <summary>
    ///     配置遍历器
    /// </summary>
    public interface IConfigIterator
    {
        void Foreach<T>(Action<T> action) where T : class;
        void Foreach<T>(Func<T, bool> condition, Action<T> action) where T : class;
    }

    partial class EntityConfig: IConfigIterator
    {
        void IConfigIterator.Foreach<T>(Action<T> action)
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

}