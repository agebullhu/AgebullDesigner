using System.Collections.Generic;
using System.Linq;
using Agebull.Common.Mvvm;
using Agebull.EntityModel.Config;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ��չViewModel����
    /// </summary>
    public abstract class EditorViewModelBase : ViewModelBase
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
            get;
            set;
        }


        /// <summary>
        /// ����ģ��
        /// </summary>
        public DataModelDesignModel DesignModel
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
            EditorName = baseModel.Editor.EditorName;
            Editor = baseModel.Editor;
            Dispatcher = baseModel.Dispatcher;
            DesignModel = baseModel;
            RaisePropertyChanged(nameof(DesignModel));
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
    public abstract class EditorViewModelBase<TModel> : EditorViewModelBase
        where TModel : DesignModelBase, new()
    {
        public TModel Model { get; }


        /// <summary>
        /// ����
        /// </summary>
        protected EditorViewModelBase()
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
            DesignModel = baseModel;
            Model.DesignModel = cfg;
            Model.Model = DesignModel;
            Model.Context = DesignModel.Context;
            Model.Dispatcher = DesignModel.Dispatcher;
            Model.EditorName = EditorName;

            Model.Initialize();
            RaisePropertyChanged(nameof(DesignModel));
            RaisePropertyChanged(nameof(Commands));
            RaisePropertyChanged(nameof(Menus));
            RaisePropertyChanged(nameof(Buttons));
        }

    }
}