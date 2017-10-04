using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using Agebull.Common.DataModel;
using Agebull.Common.SimpleDesign;
using Gboxt.Common.DataAccess.Schemas;
using Gboxt.Common.WpfMvvmBase.Commands;
using WpfMvvmBase.Coefficient;

namespace Agebull.CodeRefactor.SolutionManager
{
    public abstract class DesignCommondBase<TConfig> : MvvmBase, IAutoRegister
    {
        /// <summary>
        /// 注册代码
        /// </summary>
        void IAutoRegister.AutoRegist()
        {
            var commands = CreateCommands();
            foreach (var command in commands)
            {
                if (command.Catalog == null)
                    command.Catalog = Catalog;
                if (command.SourceType == null)
                    command.SourceType = SourceType;
                CommandCoefficient.RegisterCommand<TConfig>(command);
            }
        }

        /// <summary>
        ///     分类
        /// </summary>
        public virtual string Catalog { get; set; }
        /// <summary>
        ///     目标类型
        /// </summary>
        public virtual string SourceType => typeof(TConfig).Name;

        #region 操作命令

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        private List<ICommandItemBuilder> CreateCommands()
        {
            var commands = new List<ICommandItemBuilder>();
            CreateCommands(commands);
            return commands;
        }

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <param name="commands"></param>
        protected abstract void CreateCommands(List<ICommandItemBuilder> commands);


        #endregion

        #region 基本设置

        /// <summary>
        /// 同步上下文
        /// </summary>
        public override ISynchronousContext Synchronous => DataModelDesignModel.Current.Synchronous;
        /// <summary>
        /// 线程调度器
        /// </summary>
        public override Dispatcher Dispatcher => DataModelDesignModel.Current.Dispatcher;

        /// <summary>
        /// 基本模型
        /// </summary>
        public DataModelDesignModel Model => DataModelDesignModel.Current;
        /// <summary>
        /// 上下文
        /// </summary>
        public DesignContext Context => DataModelDesignModel.Current.Context;

        #endregion

        #region 快捷遍历

        #region Property


        private void Foreach(Action<PropertyConfig> action, PropertyConfig property)
        {
            if (property != null)
                action(property);
        }
        private void Foreach(Action<PropertyConfig> action, EntityConfig entity)
        {
            foreach (var property in entity.Properties)
                Foreach(action, property);
        }
        private void Foreach(Action<PropertyConfig> action, ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                Foreach(action, entity);
        }
        private void Foreach(Action<PropertyConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                Foreach(action, project);
        }

        protected void Foreach(Action<PropertyConfig> action)
        {
            var property = Context.SelectConfig as PropertyConfig;
            if (property != null)
            {
                Foreach(action, property);
                return;
            }
            var entity = Context.SelectConfig as EntityConfig;
            if (entity != null)
            {
                Foreach(action, entity);
                return;
            }
            var project = Context.SelectConfig as ProjectConfig;
            if (project != null)
            {
                Foreach(action, project);
                return;
            }

            Foreach(action, Context.Solution);
        }

        private void Foreach(Func<PropertyConfig, bool> condition, Action<PropertyConfig> action, PropertyConfig property)
        {
            if (property != null && condition(property))
                action(property);
        }
        private void Foreach(Func<PropertyConfig, bool> condition, Action<PropertyConfig> action, EntityConfig entity)
        {
            foreach (var property in entity.Properties)
                Foreach(condition, action, property);
        }
        private void Foreach(Func<PropertyConfig, bool> condition, Action<PropertyConfig> action, ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                Foreach(condition, action, entity);
        }
        private void Foreach(Func<PropertyConfig, bool> condition, Action<PropertyConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                Foreach(condition, action, project);
        }

        protected void Foreach(Func<PropertyConfig, bool> condition, Action<PropertyConfig> action)
        {
            var property = Context.SelectConfig as PropertyConfig;
            if (property != null)
            {
                Foreach(condition, action, property);
                return;
            }
            var entity = Context.SelectConfig as EntityConfig;
            if (entity != null)
            {
                Foreach(condition, action, entity);
                return;
            }
            var project = Context.SelectConfig as ProjectConfig;
            if (project != null)
            {
                Foreach(condition, action, project);
                return;
            }

            Foreach(condition, action, Context.Solution);
        }
        #endregion

        #region Enum



        private void Foreach(Action<EnumConfig> action, EnumConfig enumConfig)
        {
            if (enumConfig != null)
                action(enumConfig);
        }

        private void Foreach(Action<EnumConfig> action, EntityConfig entity)
        {
            foreach (var property in entity.Properties.Where(p => p.EnumConfig != null))
                action(property.EnumConfig);
        }
        private void Foreach(Action<EnumConfig> action, ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                Foreach(action, entity);
        }
        private void Foreach(Action<EnumConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Enums)
                Foreach(action, project);
        }

        protected void Foreach(Action<EnumConfig> action)
        {
            var col = Context.SelectConfig as PropertyConfig;
            if (col != null)
            {
                action(col.EnumConfig);
                return;
            }
            var entity = Context.SelectConfig as EntityConfig;
            if (entity != null)
            {
                Foreach(action, entity);
                return;
            }
            var project = Context.SelectConfig as ProjectConfig;
            if (project != null)
            {
                Foreach(action, project);
                return;
            }

            Foreach(action, Context.Solution);
        }


        private void Foreach(Func<EnumConfig, bool> condition, Action<EnumConfig> action, EnumConfig enumConfig)
        {
            if (enumConfig != null && condition(enumConfig))
                action(enumConfig);
        }

        private void Foreach(Func<EnumConfig, bool> condition, Action<EnumConfig> action, EntityConfig entity)
        {
            foreach (var property in entity.Properties.Where(p => p.EnumConfig != null))
                Foreach(condition, action, property.EnumConfig);
        }
        private void Foreach(Func<EnumConfig, bool> condition, Action<EnumConfig> action, ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                Foreach(condition, action, entity);
        }
        private void Foreach(Func<EnumConfig, bool> condition, Action<EnumConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Enums)
                Foreach(condition, action, project);
        }

        protected void Foreach(Func<EnumConfig, bool> condition, Action<EnumConfig> action)
        {
            var col = Context.SelectConfig as PropertyConfig;
            if (col != null)
            {
                Foreach(condition, action, col.EnumConfig);
                return;
            }
            var entity = Context.SelectConfig as EntityConfig;
            if (entity != null)
            {
                Foreach(condition, action, entity);
                return;
            }
            var project = Context.SelectConfig as ProjectConfig;
            if (project != null)
            {
                Foreach(condition, action, project);
                return;
            }

            Foreach(condition, action, Context.Solution);
        }
        #endregion

        #region Entity


        protected void Foreach(Action<EntityConfig> action, EntityConfig entity)
        {
            if (entity != null)
                action(entity);
        }
        protected void Foreach(Action<EntityConfig> action, ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                Foreach(action, entity);
        }
        protected void Foreach(Action<EntityConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                Foreach(action, project);
        }

        protected void Foreach(Action<EntityConfig> action)
        {
            var col = Context.SelectConfig as PropertyConfig;
            if (col != null)
            {
                action(col.Parent);
                return;
            }
            var entity = Context.SelectConfig as EntityConfig;
            if (entity != null)
            {
                Foreach(action, entity);
                return;
            }
            var project = Context.SelectConfig as ProjectConfig;
            if (project != null)
            {
                Foreach(action, project);
                return;
            }
            Foreach(action, Context.Solution);
        }


        protected void Foreach(Func<EntityConfig, bool> condition, Action<EntityConfig> action, EntityConfig entity)
        {
            if (entity != null && condition(entity))
                action(entity);
        }
        protected void Foreach(Func<EntityConfig, bool> condition, Action<EntityConfig> action, ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                Foreach(condition, action, entity);
        }
        protected void Foreach(Func<EntityConfig, bool> condition, Action<EntityConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                Foreach(condition, action, project);
        }

        protected void Foreach(Func<EntityConfig, bool> condition, Action<EntityConfig> action)
        {
            var col = Context.SelectConfig as PropertyConfig;
            if (col != null)
            {
                Foreach(condition, action, col.Parent);
                return;
            }
            var entity = Context.SelectConfig as EntityConfig;
            if (entity != null)
            {
                Foreach(condition, action, entity);
                return;
            }
            var project = Context.SelectConfig as ProjectConfig;
            if (project != null)
            {
                Foreach(condition, action, project);
                return;
            }
            Foreach(condition, action, Context.Solution);
        }

        #endregion

        #region Project


        protected void Foreach(Action<ProjectConfig> action, EntityConfig entity)
        {
            Foreach(action, entity?.Parent);
        }
        protected void Foreach(Action<ProjectConfig> action, ProjectConfig project)
        {
            if (project != null)
                action(project);
        }
        protected void Foreach(Action<ProjectConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                Foreach(action, project);
        }

        protected void Foreach(Action<ProjectConfig> action)
        {
            var col = Context.SelectConfig as PropertyConfig;
            if (col != null)
            {
                Foreach(action, col.Parent);
                return;
            }
            var entity = Context.SelectConfig as ProjectConfig;
            if (entity != null)
            {
                Foreach(action, entity);
                return;
            }
            var project = Context.SelectConfig as ProjectConfig;
            if (project != null)
            {
                Foreach(action, project);
                return;
            }
            Foreach(action, Context.Solution);
        }


        protected void Foreach(Func<ProjectConfig, bool> condition, Action<ProjectConfig> action, EntityConfig entity)
        {
            Foreach(action, entity?.Parent);
        }
        protected void Foreach(Func<ProjectConfig, bool> condition, Action<ProjectConfig> action, ProjectConfig project)
        {
            if (project != null && condition(project))
                action(project);
        }
        protected void Foreach(Func<ProjectConfig, bool> condition, Action<ProjectConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                Foreach(condition, action, project);
        }

        protected void Foreach(Func<ProjectConfig, bool> condition, Action<ProjectConfig> action)
        {
            var col = Context.SelectConfig as PropertyConfig;
            if (col != null)
            {
                Foreach(condition, action, col.Parent);
                return;
            }
            var entity = Context.SelectConfig as ProjectConfig;
            if (entity != null)
            {
                Foreach(condition, action, entity);
                return;
            }
            var project = Context.SelectConfig as ProjectConfig;
            if (project != null)
            {
                Foreach(condition, action, project);
                return;
            }
            Foreach(condition, action, Context.Solution);
        }

        #endregion
        #endregion
    }
}