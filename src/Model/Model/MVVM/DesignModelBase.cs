using System;
using System.Linq;
using Agebull.EntityModel.Config;
using Agebull.Common.Mvvm;
using System.Collections.Generic;

namespace Agebull.EntityModel.Designer
{
    public class DesignModelBase : ModelBase
    {
        #region 操作命令

        public DesignModelBase()
        {
            CommondFilter = cmd =>
            {
                if (cmd.Editor.IsEmpty() || !cmd.Editor.Contains(EditorName, StringComparison.OrdinalIgnoreCase))
                    return false;
                return true;
            };
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected override void DoInitialize()
        {
            DesignModelType = DesignModel?.GetType();
        }

        /// <summary>
        /// 当前编辑的对象
        /// </summary>
        public ConfigBase DesignModel
        {
            get => designModel;
            set
            {
                designModel = value;
                RaisePropertyChanged(nameof(DesignModel));
            }
        }

        /// <summary>
        /// 当前的命令过滤器
        /// </summary>
        public Func<ICommandItemBuilder, bool> CommondFilter { get; set; }

        /// <summary>
        ///     分类
        /// </summary>
        public string EditorName { get; set; }

        /// <summary>
        ///     分类
        /// </summary>
        public Type DesignModelType { get; set; }

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <param name="commands"></param>
        public NotificationList<CommandItemBase> CreateCommands()
        {
            var commands = new List<CommandItemBase>();
            CreateCommands(commands);

            if (DesignModelType != null && CommondFilter != null)
                CommandCoefficient.EditorToolbar(commands, CommondFilter, DesignModelType);
            return commands.Where(FilterCommand).ToNotificationList<CommandItemBase>();
        }

        /// <summary>
        /// 命令ICommandModel
        /// </summary>
        public NotificationList<CommandItemBase> Commands => CreateCommands();

        /// <summary>
        /// 生成命令对象
        /// </summary>
        /// <param name="commands"></param>
        public virtual void CreateCommands(IList<CommandItemBase> commands)
        {
        }

        /// <summary>
        /// 初始化
        /// </summary>
        protected virtual bool FilterCommand(CommandItemBase command)
        {
            if (command.CanButton)
                command.IsButton = true;
            command.Source = DesignModel;
            command.SignleSoruce = true;
            return true;
        }
        #endregion

        #region 基本设置

        /// <summary>
        /// 同步解决方案变更
        /// </summary>
        public virtual void OnSolutionChanged()
        {
        }

        /// <summary>
        /// 上下文
        /// </summary>
        private DesignContext _context;

        private DataModelDesignModel _model;
        private EditorModel _editor;
        private ConfigBase designModel;

        /// <summary>
        /// 基本模型
        /// </summary>
        public DataModelDesignModel Model
        {
            get => _model;
            set
            {
                _model = value;
                RaisePropertyChanged(nameof(Model));
            }
        }

        /// <summary>
        /// 编辑器模型
        /// </summary>
        public EditorModel Editor
        {
            get => _editor;
            set
            {
                _editor = value;
                RaisePropertyChanged(nameof(Editor));
            }
        }


        /// <summary>
        /// 上下文
        /// </summary>
        public DesignContext Context
        {
            get => _context;
            set
            {
                if (_context == value)
                    return;
                if (_context != null)
                    _context.PropertyChanged -= Context_PropertyChanged;
                _context = value;
                if (_context != null)
                    _context.PropertyChanged += Context_PropertyChanged;
                RaisePropertyChanged(nameof(Context));

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


        private void Foreach(Action<FieldConfig> action, FieldConfig property)
        {
            if (property != null)
                action(property);
        }
        private void Foreach(Action<FieldConfig> action, EntityConfig entity)
        {
            foreach (var property in entity.Properties)
                Foreach(action, property);
        }
        private void Foreach(Action<FieldConfig> action, ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                Foreach(action, entity);
        }
        private void Foreach(Action<FieldConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                Foreach(action, project);
        }

        protected void Foreach(Action<FieldConfig> action)
        {
            if (Context.SelectConfig is FieldConfig property)
            {
                Foreach(action, property);
                return;
            }
            if (Context.SelectConfig is EntityConfig entity)
            {
                Foreach(action, entity);
                return;
            }
            if (Context.SelectConfig is ProjectConfig project)
            {
                Foreach(action, project);
                return;
            }

            Foreach(action, Context.Solution);
        }

        private void Foreach(Func<FieldConfig, bool> condition, Action<FieldConfig> action, FieldConfig property)
        {
            if (property != null && condition(property))
                action(property);
        }
        private void Foreach(Func<FieldConfig, bool> condition, Action<FieldConfig> action, EntityConfig entity)
        {
            foreach (var property in entity.Properties)
                Foreach(condition, action, property);
        }
        private void Foreach(Func<FieldConfig, bool> condition, Action<FieldConfig> action, ProjectConfig project)
        {
            foreach (var entity in project.Entities)
                Foreach(condition, action, entity);
        }
        private void Foreach(Func<FieldConfig, bool> condition, Action<FieldConfig> action, SolutionConfig solution)
        {
            foreach (var project in solution.Projects)
                Foreach(condition, action, project);
        }

        protected void Foreach(Func<FieldConfig, bool> condition, Action<FieldConfig> action)
        {
            if (Context.SelectConfig is FieldConfig property)
            {
                Foreach(condition, action, property);
                return;
            }
            if (Context.SelectConfig is EntityConfig entity)
            {
                Foreach(condition, action, entity);
                return;
            }
            if (Context.SelectConfig is ProjectConfig project)
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
            if (Context.SelectConfig is FieldConfig col)
            {
                action(col.EnumConfig);
                return;
            }
            if (Context.SelectConfig is EntityConfig entity)
            {
                Foreach(action, entity);
                return;
            }
            if (Context.SelectConfig is ProjectConfig project)
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
            if (Context.SelectConfig is FieldConfig col)
            {
                Foreach(condition, action, col.EnumConfig);
                return;
            }
            if (Context.SelectConfig is EntityConfig entity)
            {
                Foreach(condition, action, entity);
                return;
            }
            if (Context.SelectConfig is ProjectConfig project)
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
            if (Context.SelectConfig is FieldConfig col)
            {
                action(col.Entity);
                return;
            }
            if (Context.SelectConfig is EntityConfig entity)
            {
                Foreach(action, entity);
                return;
            }
            if (Context.SelectConfig is ProjectConfig project)
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
            if (Context.SelectConfig is FieldConfig col)
            {
                Foreach(condition, action, col.Entity);
                return;
            }
            if (Context.SelectConfig is EntityConfig entity)
            {
                Foreach(condition, action, entity);
                return;
            }
            if (Context.SelectConfig is ProjectConfig project)
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
            if (Context.SelectConfig is FieldConfig col)
            {
                Foreach(action, col.Entity);
                return;
            }
            if (Context.SelectConfig is ProjectConfig entity)
            {
                Foreach(action, entity);
                return;
            }
            if (Context.SelectConfig is ProjectConfig project)
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
            if (Context.SelectConfig is FieldConfig col)
            {
                Foreach(condition, action, col.Entity);
                return;
            }
            if (Context.SelectConfig is ProjectConfig entity)
            {
                Foreach(condition, action, entity);
                return;
            }
            if (Context.SelectConfig is ProjectConfig project)
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