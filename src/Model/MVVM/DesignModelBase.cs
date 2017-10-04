using System;
using System.Collections.Generic;
using System.Linq;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    public class DesignModelBase : ModelBase
    {
        #region 操作命令
        
        protected override void DoInitialize()
        {
            Catalog = GetType().Name;
            base.DoInitialize();
        }

        /// <summary>
        ///     分类
        /// </summary>
        public string Catalog { get; set; } 

        /// <summary>
        /// 所有命令
        /// </summary>
        private List<CommandItem> _commands;
        /// <summary>
        /// 所有命令
        /// </summary>
        public List<CommandItem> Commands => _commands ?? (_commands = CreateCommands());

        /// <summary>
        /// 所有按钮
        /// </summary>
        public IEnumerable<CommandItem> Buttons => Commands.Where(p => !p.NoButton);

        /// <summary>
        /// 所有按钮
        /// </summary>
        public IEnumerable<CommandItem> Menus => Commands.Where(p => p.NoButton);

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <returns></returns>
        protected virtual List<CommandItem> CreateCommands()
        {
            var commands = new List<CommandItem>();
            CreateCommands(commands);

            var extends = CommandCoefficient.Coefficient(typeof(EntityConfig), Catalog);
            if (extends.Count > 0)
                commands.AddRange(extends);
            return commands;
        }

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <param name="commands"></param>
        protected virtual void CreateCommands(List<CommandItem> commands)
        {
        }

        #endregion

        #region 基本设置

        /// <summary>
        /// 上下文
        /// </summary>
        private DesignContext _context;
        /// <summary>
        /// 基本模型
        /// </summary>
        public DataModelDesignModel Model { get; set; }
        /// <summary>
        /// 上下文
        /// </summary>
        public DesignContext Context
        {
            get { return _context; }
            set
            {
                if (_context == value)
                    return;
                if (_context != null)
                    _context.PropertyChanged -= Context_PropertyChanged;
                _context = value;
                if (_context != null)
                    _context.PropertyChanged += Context_PropertyChanged;

            }
        }

        /// <summary>
        /// 上下文属性变化
        /// </summary>
        private void Context_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnContextPropertyChanged(e.PropertyName);
        }

        /// <summary>
        /// 上下文属性变化
        /// </summary>
        protected virtual void OnContextPropertyChanged(string name)
        {
        }

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