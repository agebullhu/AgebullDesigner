using System.Collections.Generic;
using System.Linq;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ��չViewModel����
    /// </summary>
    public abstract class ExtendViewModelBase : ViewModelBase
    {
        private DesignContext _context;
        private EditorModel _editor;


        #region ��������

        /// <summary>
        ///     ��Ӧ�������
        /// </summary>
        public IEnumerable<CommandItemBase> Buttons => Commands.Where(p => !p.NoButton);

        /// <summary>
        ///     ��Ӧ�������
        /// </summary>
        public CommandItemBase Menus => new CommandItem
        {
            IsRoot = true,
            Caption = "��",
            Items = Commands.Where(p => p.NoButton).ToNotificationList<CommandItemBase>()
        };

        /// <summary>
        ///     ����
        /// </summary>
        public string EditorName
        {
            get => DesignModel.EditorName;
            set => DesignModel.EditorName = value;
        }


        /// <summary>
        /// ����ģ��
        /// </summary>
        public DataModelDesignModel BaseModel
        {
            get;
            set;
        }

        /// <summary>
        /// ������
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
        /// �����
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

        #region �����

        /// <summary>
        ///     ���õ�ǰ�༭�Ķ���
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
        ///     ģ��
        /// </summary>
        public abstract DesignModelBase DesignModel
        {
            get;
        }

        /// <summary>
        /// ���������б�
        /// </summary>
        /// <returns></returns>
        public virtual void CreateCommands(IList<CommandItemBase> commands)
        {
        }
        /// <summary>
        /// ����ICommandModel
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
    /// ��չViewModel����
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public abstract class ExtendViewModelBase<TModel> : ExtendViewModelBase
        where TModel : DesignModelBase, new()
    {
        public TModel Model { get; }


        /// <summary>
        /// ����
        /// </summary>
        protected ExtendViewModelBase()
        {
            Model = new TModel
            {
                ViewModel = this
            };
        }

        /// <summary>
        /// ����ICommandModel
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
        ///     ���õ�ǰ�༭�Ķ���
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
        /// ���������б�
        /// </summary>
        /// <returns></returns>
        public virtual void CreateCommands(IList<CommandItemBase> commands)
        {
        }
        /// <summary>
        ///     ģ��
        /// </summary>
        public sealed override DesignModelBase DesignModel => Model;

    }
}