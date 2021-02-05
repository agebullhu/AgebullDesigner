using System.Collections.Generic;
using System.Linq;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// 扩展ViewModel基类
    /// </summary>
    public abstract class ExtendViewModelBase : ViewModelBase
    {
        private DesignContext _context;
        private EditorModel _editor;


        #region 基本设置

        /// <summary>
        ///     对应的命令集合
        /// </summary>
        public IEnumerable<CommandItemBase> Buttons => Commands.Where(p => !p.NoButton);

        /// <summary>
        ///     对应的命令集合
        /// </summary>
        public CommandItemBase Menus => new CommandItem
        {
            IsRoot = true,
            Caption = "〓",
            Items = Commands.Where(p => p.NoButton).ToNotificationList<CommandItemBase>()
        };

        /// <summary>
        ///     分类
        /// </summary>
        public string EditorName
        {
            get => DesignModel.EditorName;
            set => DesignModel.EditorName = value;
        }


        /// <summary>
        /// 基本模型
        /// </summary>
        public DataModelDesignModel BaseModel
        {
            get;
            set;
        }

        /// <summary>
        /// 上下文
        /// </summary>
        public DesignContext Context
        {
            set
            {
                _context = value;
                RaisePropertyChanged(nameof(Context));
            }

            get => _context;
        }


        /// <summary>
        /// 设计器
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

        #endregion

        #region 主面板

        /// <summary>
        ///     设置当前编辑的对象
        /// </summary>
        public virtual void SetContextConfig(DataModelDesignModel baseModel, ConfigBase cfg)
        {
            Context = baseModel.Context;
            Editor = baseModel.Editor;
            Dispatcher = baseModel.Dispatcher;
            BaseModel = baseModel;
            RaisePropertyChanged(nameof(BaseModel));
        }


        /// <summary>
        ///     模型
        /// </summary>
        public abstract DesignModelBase DesignModel
        {
            get;
        }

        /// <summary>
        /// 构造命令列表
        /// </summary>
        /// <returns></returns>
        public virtual void CreateCommands(IList<CommandItemBase> commands)
        {
        }
        /// <summary>
        /// 命令ICommandModel
        /// </summary>
        public virtual NotificationList<CommandItemBase> Commands
        {
            get
            {
                var cmds = new NotificationList<CommandItemBase>();
                CreateCommands(cmds);
                return cmds;
            }
        }

        #endregion
    }

    /// <summary>
    /// 扩展ViewModel基类
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class ExtendViewModelBase<TModel> : ExtendViewModelBase
        where TModel : DesignModelBase, new()
    {
        public TModel Model { get; }


        /// <summary>
        /// 构造
        /// </summary>
        protected ExtendViewModelBase()
        {
            Model = new TModel
            {
                ViewModel = this
            };
        }

        /// <summary>
        /// 命令ICommandModel
        /// </summary>
        public override NotificationList<CommandItemBase> Commands
        {
            get
            {
                var cmds = new NotificationList<CommandItemBase>();
                CreateCommands(cmds);
                cmds.AddRange(Model.CreateCommands());
                return cmds;
            }
        }

        /// <summary>
        ///     设置当前编辑的对象
        /// </summary>
        public override void SetContextConfig(DataModelDesignModel baseModel, ConfigBase cfg)
        {
            Context = baseModel.Context;
            Editor = baseModel.Editor;
            Dispatcher = baseModel.Dispatcher;
            BaseModel = baseModel;
            Model.DesignModel = cfg;
            Model.Model = BaseModel;
            Model.Context = BaseModel.Context;
            Model.Dispatcher = BaseModel.Dispatcher;
            Model.EditorName = EditorName;
            Model.Initialize();
            RaisePropertyChanged(nameof(BaseModel));
            RaisePropertyChanged(nameof(Commands));
            RaisePropertyChanged(nameof(Menus));
            RaisePropertyChanged(nameof(Buttons));
        }

        /// <summary>
        /// 构造命令列表
        /// </summary>
        /// <returns></returns>
        public virtual void CreateCommands(IList<CommandItemBase> commands)
        {
        }
        /// <summary>
        ///     模型
        /// </summary>
        public sealed override DesignModelBase DesignModel => Model;

    }
}