using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Agebull.EntityModel.Designer
{
    public class DesignModelBase : ModelBase
    {
        #region 操作命令

        public DesignModelBase()
        {
            CommondFilter = cmd =>
            {
                if (cmd.Editor.IsMissing() || EditorName.IsMissing() || !cmd.Editor.Contains(EditorName, StringComparison.OrdinalIgnoreCase))
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
            //command.SignleSoruce = true;
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

    }

}