using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows;
using System.Windows.Controls;
using Agebull.Common.Mvvm;

namespace Agebull.EntityModel.Designer
{
    /// <summary>
    /// ��չViewModel����
    /// </summary>
    public abstract class ExtendViewModelBase : ViewModelBase
    {
        private DataModelDesignModel _baseModel;
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
        public CommandItem Menus =>
            new CommandItem
            {
                IsRoot = true,
                Caption = "��չ����",
                Items = Commands.Where(p => p.NoButton).ToNotificationList<CommandItemBase>()
            };
        /// <summary>
        ///     ����
        /// </summary>
        public string EditorName { get; set; }


        /// <summary>
        /// ����ģ��
        /// </summary>
        public DataModelDesignModel BaseModel
        {
            get => _baseModel;
            set
            {
                _baseModel = value;
                Context = value.Context;
                Editor = value.Editor;
                Dispatcher = value.Dispatcher;
                OnBaseModelBinding();
            }
        }


        /// <summary>
        ///     ģ��
        /// </summary>
        public abstract void OnBaseModelBinding();

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
        /// ����幹�����
        /// </summary>
        /// <param name="body"></param>
        protected abstract void OnBodyCreating(FrameworkElement body);


        /// <summary>
        ///     ģ��
        /// </summary>
        public abstract DesignModelBase DesignModel
        {
            get;
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
        ///     ģ��
        /// </summary>
        public override void OnBaseModelBinding()
        {
            Model.Model = BaseModel;
            Model.Context = BaseModel.Context;
            Model.Dispatcher = BaseModel.Dispatcher;
            Model.EditorName = EditorName;
            Model.Initialize();
        }
        /// <summary>
        /// ����幹�����
        /// </summary>
        /// <param name="body"></param>
        protected override void OnBodyCreating(FrameworkElement body)
        {
            //body.DataContext = Model;
            //RaisePropertyChanged(nameof(Context));
            //RaisePropertyChanged(nameof(DesignModel));
        }
        protected override NotificationList<CommandItemBase> CreateCommands()
        {
            return Model.CreateCommands();
        }

        /// <summary>
        ///     ģ��
        /// </summary>
        public sealed override DesignModelBase DesignModel => Model;


        /// <summary>
        ///     ���������ֵ�
        /// </summary>
        [IgnoreDataMember]
        public ModelFunctionDictionary<TModel> ModelFunction { get; }

    }
}