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
        #region ��������

        /// <summary>
        ///     ��Ӧ�������
        /// </summary>
        public IEnumerable<CommandItem> Buttons => Commands.Where(p => !p.NoButton);

        /// <summary>
        ///     ��Ӧ�������
        /// </summary>
        public CommandItem Menus =>
            new CommandItem
            {
                IsRoot = true,
                Caption = "��չ����",
                Items = Commands.Where(p => p.NoButton).ToObservableCollection<CommandItem>()
            };
        /// <summary>
        ///     ����
        /// </summary>
        public string EditorName { get; set; }


        /// <summary>
        /// ����ģ��
        /// </summary>
        public DataModelDesignModel BaseModel { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public DesignContext Context => BaseModel?.Context;

        /// <summary>
        /// �����
        /// </summary>
        public EditorModel Editor => BaseModel?.Editor;

        #endregion

        #region �����

        /// <summary>
        /// �����ע�붯��
        /// </summary>
        public DependencyAction ContentBehavior => new DependencyAction
        {
            AttachAction = obj =>
            {
                var border = (Border)obj;
                var body = Body;
                OnBodyCreating(body);
                border.Child = Body;
            }
        };
        /// <summary>
        /// �����
        /// </summary>
        public abstract FrameworkElement Body { get; }

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
        /// <summary>
        /// ����
        /// </summary>
        protected ExtendViewModelBase()
        {
            Model = new TModel
            {
                ViewModel = this
            };
            ModelFunction = new ModelFunctionDictionary<TModel>
            {
                Model = Model
            };
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
        protected override ObservableCollection<CommandItem> CreateCommands()
        {
            return Model.CreateCommands();
        }
        /// <summary>
        ///     ģ��
        /// </summary>
        public TModel Model
        {
            get;
        }

        private DesignModelBase _designModel;
        /// <summary>
        ///     ģ��
        /// </summary>
        public sealed override DesignModelBase DesignModel
        {
            get
            {
                if (_designModel != null)
                    return _designModel;
                Model.EditorName = EditorName;
                Model.Context = Context;
                Model.Dispatcher = Dispatcher;
                return _designModel = Model;
            }
        }


        /// <summary>
        ///     ���������ֵ�
        /// </summary>
        [IgnoreDataMember]
        public ModelFunctionDictionary<TModel> ModelFunction { get; }

    }
}